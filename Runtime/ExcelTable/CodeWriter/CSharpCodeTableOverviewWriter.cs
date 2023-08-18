﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pangoo
{
    public class CSharpCodeTableOverviewWriter : ICodeWriter
    {
        public string FileExtension => ".cs";

        private const string NoRenameAttribute = "[Obfuscation(Feature = \"renaming\", Exclude = true)]";
        private const string NoPruneAttribute = "[Obfuscation(Feature = \"trigger\", Exclude = false)]";

        List<string> m_Headers;

        ExcelTableData m_ExcelData;

        // string m_JsonDir;
        // string m_PackageDir;
        

        public CSharpCodeTableOverviewWriter(List<string> headers, ExcelTableData excelData)
        {
            m_Headers = headers;
            m_ExcelData = excelData;
            // m_PackageDir = packageDir;
            // m_JsonDir = jsonDir;
        }

        public string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
        {
            switch (type.Type)
            {
                case JsonTypeEnum.Anything: return "object";
                case JsonTypeEnum.Array: return "List<" + GetTypeName(type.InternalType, config) + ">";
                case JsonTypeEnum.Dictionary: return "Dictionary<string, " + GetTypeName(type.InternalType, config) + ">";
                case JsonTypeEnum.Boolean: return "bool";
                case JsonTypeEnum.Float: return "double";
                case JsonTypeEnum.Integer: return "int";
                case JsonTypeEnum.Long: return "long";
                case JsonTypeEnum.Date: return "DateTime";
                case JsonTypeEnum.NonConstrained: return "object";
                case JsonTypeEnum.NullableBoolean: return "bool?";
                case JsonTypeEnum.NullableFloat: return "double?";
                case JsonTypeEnum.NullableInteger: return "int?";
                case JsonTypeEnum.NullableLong: return "long?";
                case JsonTypeEnum.NullableDate: return "DateTime?";
                case JsonTypeEnum.NullableSomething: return "object";
                case JsonTypeEnum.Object: return type.AssignedName;
                case JsonTypeEnum.String: return "string";
                default: throw new System.NotSupportedException("Unsupported json type");
            }
        }


        private bool ShouldApplyNoRenamingAttribute(IJsonClassGeneratorConfig config)
        {
            return config.ApplyObfuscationAttributes && !config.UsePascalCase;
        }
        private bool ShouldApplyNoPruneAttribute(IJsonClassGeneratorConfig config)
        {
            return config.ApplyObfuscationAttributes;
        }

        public void WriteFileStart(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            foreach (var line in JsonClassGenerator.FileHeader)
            {
                sw.WriteLine("// " + line);
            }
            sw.WriteLine();
            if (m_Headers != null)
            {
                foreach (var header in m_Headers)
                {
                    sw.WriteLine($"using {header};");
                }
            }
            sw.WriteLine($"#if UNITY_EDITOR");
            sw.WriteLine($"using UnityEditor;");
            sw.WriteLine($"#endif");


            if (ShouldApplyNoPruneAttribute(config) || ShouldApplyNoRenamingAttribute(config))
                sw.WriteLine("using System.Reflection;");
        }

        public void WriteFileEnd(IJsonClassGeneratorConfig config, TextWriter sw)
        {

        }


        public void WriteMainClassStart(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("namespace {0}", config.Namespace);
            sw.WriteLine("{");
            // sw.WriteLine($"   [CreateAssetMenu(fileName = \"{config.MainClass}Overview\", menuName = \"Pangoo/ExcelTable/{config.MainClass}Overview\", order = 0)]");
            sw.WriteLine($"    public partial class {JsonClassGenerator.ToTitleCase(config.MainClass)}Overview : ExcelTableOverview");
            sw.WriteLine("    {");
        }

        public void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            sw.WriteLine();
            // sw.WriteLine("         [TableList(IsReadOnly = true, AlwaysExpanded = true)]");
            if (m_ExcelData.ClassName != null)
            {
                sw.WriteLine($"       public {m_ExcelData.ClassName} Data;");


                sw.WriteLine();
                sw.WriteLine("       public override ExcelTableBase GetExcelTableBase()");
                sw.WriteLine("       {");
                sw.WriteLine($"           return CopyUtility.Clone<{m_ExcelData.ClassName}>(Data);");
                sw.WriteLine("       }");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("       public override Type GetDataType()");
                sw.WriteLine("       {");
                sw.WriteLine("           return Data.GetType();");
                sw.WriteLine("       }");


                sw.WriteLine();
                sw.WriteLine("       public override ExcelTableBase Table{");
                sw.WriteLine("          get{");
                sw.WriteLine("           return Data;");
                sw.WriteLine("          }");
                sw.WriteLine("       }");




                // sw.WriteLine();
                // sw.WriteLine("       public override int GetRowCount()");
                // sw.WriteLine("       {");
                // sw.WriteLine("           return Data != null ? Data.Rows.Count : 0;");
                // sw.WriteLine("       }");
                // sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("       public override string GetJsonPath()");
                sw.WriteLine("       {");
                sw.WriteLine($"           return \"{m_ExcelData.ClassName}\";");
                sw.WriteLine("       }");
                sw.WriteLine();


                sw.WriteLine();
                sw.WriteLine("       public override string GetName()");
                sw.WriteLine("       {");
                sw.WriteLine($"           return \"{m_ExcelData.ClassBaseName}\";");
                sw.WriteLine("       }");
                sw.WriteLine();




                sw.WriteLine($"#if UNITY_EDITOR");

                // sw.WriteLine();
                // sw.WriteLine("       [Button(\"从Json重构\",30)]");
                // sw.WriteLine("       public override void LoadFromJson()");
                // sw.WriteLine("       {");
                // sw.Write(    "          var path = $\"{PackageDir}/" + $"{m_JsonDir}");
                // sw.WriteLine("/{GetJsonPath()}.json\";");
                // sw.WriteLine($"          string json = File.ReadAllText(path);");
                // sw.WriteLine($"          Data =  JsonMapper.ToObject<{m_ExcelData.ClassName}>(json);");
                // sw.WriteLine("       }");
                // sw.WriteLine();
                //
                // sw.WriteLine();
                // sw.WriteLine("       [Button(\"生成Json\",30)]");
                // sw.WriteLine("       public override void SaveJson()");
                // sw.WriteLine("       {");
                // sw.Write(    "          var path = $\"{PackageDir}/" + $"{m_JsonDir}");
                // sw.WriteLine(           "/{GetJsonPath()}.json\";");
                // sw.WriteLine("          var json = JsonMapper.ToJson(Data);");
                // sw.WriteLine("          using (var sw = new StreamWriter(path))");
                // sw.WriteLine("           {");
                // sw.WriteLine("              sw.WriteLine(json);");
                // sw.WriteLine("           }");
                // sw.WriteLine("           SaveConfig();");
                // sw.WriteLine("       }");
                // sw.WriteLine();
                
                sw.WriteLine();
                // sw.WriteLine("       [Button(\"从Excel文件重构数据\",30)]");
                sw.WriteLine("        /// <summary> 加载Excel文件</summary>");
                sw.WriteLine("        public override void LoadExcelFile(bool save = true)");
                sw.WriteLine("        {");
                sw.WriteLine("           if(Data == null){");
                sw.WriteLine("              Data=new();");
                sw.WriteLine("           }");
                sw.WriteLine("           Data.LoadExcelFile(ExcelPath);");
                sw.WriteLine("           if(save){");
                sw.WriteLine("              SaveConfig();");
                sw.WriteLine("           } else {");
                sw.WriteLine("               EditorUtility.SetDirty(this);");
                sw.WriteLine("           }");
                sw.WriteLine("        }");
                sw.WriteLine();
                
        //                   if(Data == null){
        //     Data=new();
        //   }
        //   Data.LoadExcelFile(ExcelPath);
        //   SaveConfig();
                // sw.WriteLine();
                // sw.WriteLine("       [Button(\"生成Excel文件\",30)]");
                // sw.WriteLine("        /// <summary> 生成Excel文件</summary>");
                // sw.WriteLine("        public override void BuildExcelFile()");
                // sw.WriteLine("        {");
                // sw.WriteLine("          BuildExcelFile(Data);");
                // sw.WriteLine("        }");
                // sw.WriteLine();


                sw.WriteLine($"#endif");

            }



        }

        public void WriteMainClassEnd(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            sw.WriteLine("    }");
            sw.WriteLine("}");
        }

        public void WriteClass(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type)
        {
            var visibility = "public";

            if (!type.IsRoot)
            {
                if (ShouldApplyNoRenamingAttribute(config))
                    sw.WriteLine("        " + NoRenameAttribute);
                if (ShouldApplyNoPruneAttribute(config))
                    sw.WriteLine("        " + NoPruneAttribute);
                sw.WriteLine("        {0} partial class {1}", visibility, type.AssignedName);
                sw.WriteLine("        {");
            }

            var prefix = !type.IsRoot ? "            " : "        ";

            WriteClassMembers(config, sw, type, prefix);

            if (!type.IsRoot)
                sw.WriteLine("        }");

            sw.WriteLine();
        }



        private void WriteClassMembers(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type, string prefix)
        {
            foreach (var field in type.Fields)
            {
                if (config.UsePascalCase || config.ExamplesInDocumentation)
                    sw.WriteLine();

                if (config.ExamplesInDocumentation)
                {
                    sw.WriteLine(prefix + "/// <summary>");
                    sw.WriteLine(prefix + "/// Examples: " + field.GetExamplesText());
                    sw.WriteLine(prefix + "/// </summary>");
                }


                if (config.UsePascalCase)
                {

                    sw.WriteLine(prefix + "[JsonMember(\"{0}\")]", field.JsonMemberName);
                }


                //sw.WriteLine(prefix + "public {0} {1} {{ get; private set; }}", field.Type.GetTypeName(), field.MemberName);

                //使用模板Example值作为类型
                //export_path不作为类型导出
                if (config.ExamplesToType && field.Type.Type == JsonTypeEnum.String && field.JsonMemberName != "@export_path")
                    sw.WriteLine(prefix + "public {0} {1} {{ get; private set; }}", GetTypeFromExample(field.GetExamplesText()), field.MemberName);
                else
                    sw.WriteLine(prefix + "public {0} {1} {{ get; private set; }}", field.Type.GetTypeName(), field.MemberName);
            }
        }

        private string GetTypeFromExample(string example)
        {
            var result = example;
            result = result.Replace("\"", string.Empty);

            var isList = result.StartsWith("list|");
            if (isList)
                result = result.Substring(5);

            switch (result)
            {
                case "string":
                case "int":
                case "bool":
                case "double":
                case "DateTime":
                case "LFloat":
                    break;
                default:
                    throw new NotSupportedException("not support type from example: " + result);
            }

            if (isList)
                result = $"List<{result}>";

            return result;
        }
    }
}
