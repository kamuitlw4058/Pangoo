﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LitJson;
using Newtonsoft.Json;

namespace Pangoo.Editor
{
    public class CSharpEventCodeWriter : ICodeWriter
    {
        public string FileExtension => ".cs";

        private const string NoRenameAttribute = "[Obfuscation(Feature = \"renaming\", Exclude = true)]";
        private const string NoPruneAttribute = "[Obfuscation(Feature = \"trigger\", Exclude = false)]";

        List<string> m_Headers;

        public CSharpEventCodeWriter(List<string> headers)
        {
            m_Headers = headers;
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
                case JsonTypeEnum.Vector3 : return "Vector3";
                case JsonTypeEnum.NonConstrained: return "object";
                case JsonTypeEnum.NullableBoolean: return "bool?";
                case JsonTypeEnum.NullableFloat: return "double?";
                case JsonTypeEnum.NullableInteger: return "int?";
                case JsonTypeEnum.NullableLong: return "long?";
                case JsonTypeEnum.NullableDate: return "DateTime?";
                case JsonTypeEnum.NullableVector3: return "Vector3?";
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
            sw.WriteLine("     [Serializable]");
            sw.WriteLine("    {0} partial class {1} : GameEventArgs", "public", JsonClassGenerator.ToTitleCase(config.MainClass));
            sw.WriteLine("    {");
        }

        public void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            var className = JsonClassGenerator.ToTitleCase(config.MainClass);
            sw.WriteLine();
            sw.WriteLine($"        public static readonly int EventId = typeof({className}).GetHashCode();");
            sw.WriteLine();

            sw.WriteLine();
            sw.WriteLine($"        public override int Id => EventId;");
            sw.WriteLine();

            sw.WriteLine();
            sw.WriteLine($"        public static {className} Create()");
            sw.WriteLine("        {");
            sw.WriteLine($"            var args = ReferencePool.Acquire<{className}>();");
            sw.WriteLine("              return args;");
            sw.WriteLine("        }");
            sw.WriteLine();


            sw.WriteLine();
            sw.WriteLine("        public override void Clear(){");
            sw.WriteLine("        }");
            sw.WriteLine();

            sw.WriteLine();
            sw.WriteLine($"        public {className}()");
            sw.WriteLine("        {");
            sw.WriteLine("              Clear();");
            sw.WriteLine("        }");
            sw.WriteLine();

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

                sw.WriteLine("        [Serializable]");
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
