using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pangoo;
using UnityEngine;

public class CsharpCodeWriterBase : ICodeWriter
{
    public string FileExtension => ".cs";

    protected const string NoRenameAttribute = "[Obfuscation(Feature = \"renaming\", Exclude = true)]";
    protected const string NoPruneAttribute = "[Obfuscation(Feature = \"trigger\", Exclude = false)]";

    protected List<string> m_Headers;
    
    protected bool ShouldApplyNoRenamingAttribute(IJsonClassGeneratorConfig config)
    {
        return config.ApplyObfuscationAttributes && !config.UsePascalCase;
    }
    protected bool ShouldApplyNoPruneAttribute(IJsonClassGeneratorConfig config)
    {
        return config.ApplyObfuscationAttributes;
    }
    public virtual string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
    {
        switch (type.Type)
        {
            case JsonTypeEnum.Anything: return "object";
            case JsonTypeEnum.Array: return "List<" + GetTypeName(type.InternalType, config) + ">";
            case JsonTypeEnum.Dictionary:
                return "Dictionary<string, " + GetTypeName(type.InternalType, config) + ">";
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
            case JsonTypeEnum.NullableVector3: return "vector3?";
            case JsonTypeEnum.NullableSomething: return "object";
            case JsonTypeEnum.Object: return type.AssignedName;
            case JsonTypeEnum.String: return "string";
            case JsonTypeEnum.Vector3 : return "Vector3";
            default: throw new System.NotSupportedException("Unsupported json type");
        }
    }

    public virtual void WriteClass(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type)
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
    public virtual void WriteClassMembers(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type, string prefix)
    {
        
    }

    public virtual void WriteFileStart(IJsonClassGeneratorConfig config, TextWriter sw)
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

    public virtual void WriteFileEnd(IJsonClassGeneratorConfig config, TextWriter sw)
    {
        sw.WriteLine("    }");
        sw.WriteLine("}");
    }

    public virtual void WriteMainClassStart(IJsonClassGeneratorConfig config, TextWriter sw)
    {
        
    }

    public virtual void WriteMainClassEnd(IJsonClassGeneratorConfig config, TextWriter sw)
    {
        
    }

    public virtual void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
    {
        
    }
    
    protected virtual string GetTypeFromExample(string example)
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
            case "float":
            case "object":
            case "Vector3":
                break;
            default:
                throw new NotSupportedException("not support type from example: " + result);
        }

        if (isList)
            result = $"List<{result}>";

        return result;
    }

}
