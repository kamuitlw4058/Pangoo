﻿// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ClassGenerator
{
    public class JsonClassGenerator : IJsonClassGeneratorConfig
    {
        public string Example { get; set; }
        public string TargetFolder { get; set; }
        public string Namespace { get; set; }
        public string MainClass { get; set; }

        public string BaseClass { get; set; }
        public string[] BaseFields { get; set; }

        public bool IsUseUnityEditor { get; set; }
        public bool IsAddCreateAssetMenu { get; set; }

        public bool IsSerializable { get; set; }

        public bool IsWriteFileHeader { get; set; }


        public string AssetMenuPrefix { get; set; }

        public bool UseJsonMember { get; set; }

        public bool UsePascalCase { get; set; }
        public bool ApplyObfuscationAttributes { get; set; }
        public ICodeWriter CodeWriter { get; set; }
        public TextWriter OutputStream { get; set; }
        public bool ExamplesInDocumentation { get; set; }
        public bool ExamplesToType { get; set; }
        public string FilePath { get; set; }

        // private PluralizationService pluralizationService = PluralizationService.CreateService(new CultureInfo("en-us"));

        public void GenerateClasses()
        {
            var writeToDisk = TargetFolder != null;
            if (writeToDisk && !Directory.Exists(TargetFolder))
                Directory.CreateDirectory(TargetFolder);

            JObject[] examples;
            using (var sr = new StringReader(Example))
            using (var reader = new JsonTextReader(sr))
            {
                var json = JToken.ReadFrom(reader);
                if (json is JArray array)
                {
                    examples = array.Cast<JObject>().ToArray();
                }
                else if (json is JObject)
                {
                    examples = new[] { (JObject)json };
                }
                else
                {
                    throw new Exception("Sample JSON must be either a JSON array, or a JSON object.");
                }
            }


            Types = new List<JsonType>();
            Names = new HashSet<string>();

            Names.Add(MainClass);
            var rootType = new JsonType(this, examples[0]);
            rootType.IsRoot = true;
            rootType.AssignName(MainClass);
            GenerateClass(examples, rootType);

            if (writeToDisk)
            {
                WriteClassesToFile(Path.Combine(TargetFolder, MainClass + CodeWriter.FileExtension), Types);
            }
            else if (OutputStream != null)
            {
                WriteClassesToStream(OutputStream, Types);
            }
        }

        private void WriteClassesToFile(string path, IEnumerable<JsonType> types)
        {
            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                WriteClassesToStream(sw, types);
            }
        }

        private void WriteClassesToStream(TextWriter sw, IEnumerable<JsonType> types)
        {
            var inMainClass = false;

            CodeWriter.WriteFileStart(this, sw);

            foreach (var type in types)
            {
                if (!inMainClass)
                {
                    CodeWriter.WriteMainClassStart(this, sw);
                    inMainClass = true;
                }

                CodeWriter.WriteClass(this, sw, type);
            }

            CodeWriter.WriteAdditionFunction(this, sw);

            CodeWriter.WriteMainClassEnd(this, sw);

            CodeWriter.WriteFileEnd(this, sw);
        }


        private void GenerateClass(JObject[] examples, JsonType type)
        {
            var jsonFields = new Dictionary<string, JsonType>();
            var fieldExamples = new Dictionary<string, IList<object>>();

            var first = true;

            foreach (var obj in examples)
            {
                foreach (var prop in obj.Properties())
                {
                    JsonType fieldType;
                    var currentType = new JsonType(this, prop.Value);
                    var propName = prop.Name;
                    if (jsonFields.TryGetValue(propName, out fieldType))
                    {
                        var commonType = fieldType.GetCommonType(currentType);
                        jsonFields[propName] = commonType;
                    }
                    else
                    {
                        var commonType = currentType;
                        if (!first)
                            commonType = commonType.GetCommonType(JsonType.GetNull(this));
                        jsonFields.Add(propName, commonType);
                        fieldExamples[propName] = new List<object>();
                    }
                    var fe = fieldExamples[propName];
                    var val = prop.Value;
                    if (val.Type == JTokenType.Null || val.Type == JTokenType.Undefined)
                    {
                        if (!fe.Contains(null))
                        {
                            fe.Insert(0, null);
                        }
                    }
                    else
                    {
                        var v = val.Type == JTokenType.Array || val.Type == JTokenType.Object ? val : val.Value<object>();
                        if (!fe.Any(x => v.Equals(x)))
                            fe.Add(v);
                    }
                }
                first = false;
            }

            foreach (var field in jsonFields)
            {
                Names.Add(ToTitleCase(field.Key));
            }

            foreach (var field in jsonFields)
            {
                var fieldType = field.Value;
                if (fieldType.Type == JsonTypeEnum.Object)
                {
                    var subexamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Object)
                            {
                                subexamples.Add((JObject)value);
                            }
                        }
                    }

                    fieldType.AssignName(CreateUniqueClassName(field.Key));
                    GenerateClass(subexamples.ToArray(), fieldType);
                }

                if (fieldType.InternalType != null && fieldType.InternalType.Type == JsonTypeEnum.Object)
                {
                    var subexamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Array)
                            {
                                foreach (var item in (JArray)value)
                                {
                                    if (!(item is JObject)) throw new NotSupportedException("Arrays of non-objects are not supported yet.");
                                    subexamples.Add((JObject)item);
                                }

                            }
                            else if (value.Type == JTokenType.Object)
                            {
                                foreach (var item in (JObject)value)
                                {
                                    if (!(item.Value is JObject)) throw new NotSupportedException("Arrays of non-objects are not supported yet.");

                                    subexamples.Add((JObject)item.Value);
                                }
                            }
                        }
                    }

                    field.Value.InternalType.AssignName(CreateUniqueClassNameFromPlural(field.Key));
                    GenerateClass(subexamples.ToArray(), field.Value.InternalType);
                }
            }

            type.Fields = jsonFields.Select(x => new JsonClassGeneratorFieldInfo(this, x.Key, x.Value, UsePascalCase, fieldExamples[x.Key])).ToArray();

            //检查是否已有同名类，有则合并成员字段
            var exist = false;
            foreach (var existType in Types)
            {
                if (existType.AssignedName == type.AssignedName)
                {
                    var comparer = new FieldInfoComparer();
                    existType.Fields = existType.Fields.Union(type.Fields, comparer).ToList();

                    exist = true;
                    break;
                }
            }
            if (!exist)
                Types.Add(type);

        }

        public IList<JsonType> Types { get; private set; }
        public HashSet<string> Names { get; private set; }

        private string CreateUniqueClassName(string name)
        {
            name = ToTitleCase(name);

            var finalName = name;
            if (Names.Any(x => x.Equals(finalName, StringComparison.OrdinalIgnoreCase)))
            {
                finalName = name + "Row";
            }
            // var i = 2;
            // while (Names.Any(x => x.Equals(finalName, StringComparison.OrdinalIgnoreCase)))
            // {
            //     finalName = name + i.ToString();
            //     i++;
            // }

            Names.Add(finalName);
            return finalName;
        }

        private string CreateUniqueClassNameFromPlural(string plural)
        {
            plural = ToTitleCase(plural);
            // return CreateUniqueClassName(pluralizationService.Singularize(plural));
            return CreateUniqueClassName(plural);//不将类名单数化
        }



        public static string ToTitleCase(string str)
        {
            var sb = new StringBuilder(str.Length);
            var flag = true;

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(flag ? char.ToUpper(c) : c);
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return sb.ToString();
        }

        public static readonly string[] FileHeader = new[] {
            "本文件使用工具自动生成，请勿进行手动修改！",
        };

        public static string GeneratorCodeString(string codeJson,
                                                string nameSpace,
                                                ICodeWriter codeWriter,
                                                string className,
                                                string path,
                                                string jsonPath = null,
                                                string baseClass = null,
                                                string[] baseFields = null,
                                                bool useJsonMember = false,
                                                bool isAddCreateAssetMenu = false,
                                                string assetMenuPrefix = null,
                                                bool isSerializable = true,
                                                bool isWriteFileHeader = true
                                                )
        {
            string OutputCode;
            var gen = new JsonClassGenerator
            {
                Example = codeJson,
                CodeWriter = codeWriter,
                Namespace = nameSpace,
                TargetFolder = null,
                MainClass = className,
                BaseClass = baseClass,
                BaseFields = baseFields,
                UseJsonMember = useJsonMember,
                IsAddCreateAssetMenu = isAddCreateAssetMenu,
                AssetMenuPrefix = assetMenuPrefix,
                IsSerializable = isSerializable,
                IsWriteFileHeader = isWriteFileHeader,
                UsePascalCase = true,
                ApplyObfuscationAttributes = false,
                ExamplesInDocumentation = false,
                ExamplesToType = true,
                FilePath = jsonPath, //用于反射获取文件路径进行读取
            };

            using (var stringWriter = new StringWriter())
            {
                gen.OutputStream = stringWriter;
                gen.GenerateClasses();
                stringWriter.Flush();
                stringWriter.ToString();
                OutputCode = stringWriter.ToString();
            }

            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(OutputCode);
            }

            return OutputCode;
        }
    }
}
