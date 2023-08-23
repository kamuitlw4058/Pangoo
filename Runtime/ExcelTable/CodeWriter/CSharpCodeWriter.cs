using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LitJson;
using Newtonsoft.Json;

namespace Pangoo
{
    public class CSharpCodeWriter : ICodeWriter
    {
        public string FileExtension => ".cs";

        private const string NoRenameAttribute = "[Obfuscation(Feature = \"renaming\", Exclude = true)]";
        private const string NoPruneAttribute = "[Obfuscation(Feature = \"trigger\", Exclude = false)]";

        List<string> m_Headers;
        ExcelTableData m_ExcelData;
        bool m_Named;


        public CSharpCodeWriter(List<string> headers, ExcelTableData excelData,bool named)
        {
            m_Headers = headers;
            m_ExcelData = excelData;
            m_Named = named;
        }

        public string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
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
            sw.WriteLine("    [Serializable]");
            sw.WriteLine("    {0} partial class {1} : ExcelTableBase", "public",
                JsonClassGenerator.ToTitleCase(config.MainClass));
            sw.WriteLine("    {");
        }

        public void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
        {

            sw.WriteLine($"        [TableList]");
            sw.WriteLine($"        public List<{m_ExcelData.ClassBaseName}Row> Rows = new();");

            sw.WriteLine();
            sw.WriteLine("        public override List<ExcelRowBase> BaseRows{");
            sw.WriteLine("          get{");
            sw.WriteLine("              List<ExcelRowBase> ret = new List<ExcelRowBase>();");
            sw.WriteLine("              ret.AddRange(Rows);");
            sw.WriteLine("              return ret;");
            sw.WriteLine("          }");
            sw.WriteLine("        }");


            sw.WriteLine();
            if(m_Named){
                sw.WriteLine("        public override List<ExcelNamedRowBase> NamedBaseRows{");
                sw.WriteLine("          get{");
                sw.WriteLine("              List<ExcelNamedRowBase> ret = new List<ExcelNamedRowBase>();");
                sw.WriteLine("              ret.AddRange(Rows);");
                sw.WriteLine("              return ret;");
                sw.WriteLine("          }");
                sw.WriteLine("        }");
            }

            // sw.WriteLine("        public override int RowCount{");
            // sw.WriteLine("          get{");
            // sw.WriteLine("              return Rows.Count;");
            // sw.WriteLine("          }");
            // sw.WriteLine("        }");

            // sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("        [NonSerialized]");
            sw.WriteLine("        [XmlIgnore]");
            sw.WriteLine($"        public Dictionary<int,{m_ExcelData.ClassBaseName}Row> Dict = new ();");

            sw.WriteLine();

            sw.WriteLine("        public override void Init(){");
            sw.WriteLine("          Dict.Clear();");
            sw.WriteLine("          foreach(var row in Rows){");
            sw.WriteLine("              Dict.Add(row.Id,row);");
            sw.WriteLine("          }");
            // sw.WriteLine("          CustomInit();");
            sw.WriteLine("        }");

            sw.WriteLine();

            sw.WriteLine("        public override void Merge(ExcelTableBase val){");
            sw.WriteLine($"          var table = val as {m_ExcelData.ClassBaseName}Table;");
            sw.WriteLine("          Rows.AddRange(table.Rows);");
            // sw.WriteLine("          Init();");
            sw.WriteLine("        }");


            sw.WriteLine();


            sw.WriteLine($"        public {m_ExcelData.ClassBaseName}Row GetRowById(int row_id)" + "{");
            sw.WriteLine($"          {m_ExcelData.ClassBaseName}Row row;");
            sw.WriteLine("          if(Dict.TryGetValue(row_id,out row)){");
            sw.WriteLine("              return row;");
            sw.WriteLine("          }");
            sw.WriteLine("          return null;");
            sw.WriteLine("         }");

            sw.WriteLine();



            
            sw.WriteLine($"#if UNITY_EDITOR");
            sw.WriteLine("        /// <summary> 从Excel文件重新构建数据 </summary>");
            sw.WriteLine("        public virtual void LoadExcelFile(string excelFilePath)");
            sw.WriteLine("        {");
            sw.WriteLine($"          Rows = LoadExcelFile<{m_ExcelData.ClassBaseName}Row>(excelFilePath);");
            sw.WriteLine("        }");
            sw.WriteLine($"#endif");
            sw.WriteLine();



            sw.WriteLine();
            sw.WriteLine("        /// <summary> 反射获取配置文件路径 </summary>");
            sw.WriteLine("        public static string DataFilePath()");
            sw.WriteLine("        {");
            sw.WriteLine("            return \"{0}\";", config.FilePath);
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
                if(m_Named){
                    sw.WriteLine("        {0} partial class {1} : ExcelNamedRowBase", visibility, type.AssignedName);
                }else{
                    sw.WriteLine("        {0} partial class {1} : ExcelRowBase", visibility, type.AssignedName);
                }
                
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
                if (field.Type.Type == JsonTypeEnum.Array)
                {
                    continue;
                }

                if(field.MemberName == "Id"){
                    continue;
                }

                if(m_Named &&  field.MemberName == "Name"){
                    continue;
                }

                if (config.UsePascalCase || config.ExamplesInDocumentation)
                    sw.WriteLine();

                if (config.ExamplesInDocumentation)
                {
                    sw.WriteLine(prefix + "/// <summary>");
                    sw.WriteLine(prefix + "/// Examples: " + field.GetExamplesText());
                    sw.WriteLine(prefix + "/// </summary>");
                }

                if (m_ExcelData != null)
                {
                    var excelData = m_ExcelData.GetColInfo(field.JsonMemberName);
                    if (excelData != null)
                    {
                        sw.WriteLine(prefix + "/// <summary>");
                        var descList = excelData.Desc.Split("\n");
                        bool isFirstLine = true;
                        foreach (var desc in descList)
                        {
                            if (isFirstLine)
                            {
                                sw.WriteLine(prefix + "/// Desc: " + excelData.Desc);
                            }
                            else
                            {
                                sw.WriteLine(prefix + "///      " + excelData.Desc);
                            }
                        }

                        sw.WriteLine(prefix + "/// </summary>");

                        sw.WriteLine(prefix + $"[TableTitleGroup(\"{excelData.CnName}\")]");
                        sw.WriteLine(prefix + "[HideLabel]");
                        // sw.WriteLine(prefix + "[ShowInInspector]");
                    }
                    else
                    {
                        if (field.Type.Type == JsonTypeEnum.Array)
                        {
                            sw.WriteLine(prefix + "[TableList( AlwaysExpanded = true)]");
                        }
                    }
                }

                if (config.UsePascalCase)
                {
                    sw.WriteLine(prefix + "[JsonMember(\"{0}\")]", field.JsonMemberName);
                    var col_index =  m_ExcelData.NameList.IndexOf(field.JsonMemberName);
                    sw.WriteLine(prefix + $"[ExcelTableCol(\"{field.MemberName}\",\"{field.JsonMemberName}\",\"{m_ExcelData.TypeList[col_index]}\", \"{m_ExcelData.CnNameLst[col_index]}\",{col_index +1})]");
                }

                //这边设定Array只有数据所以直接给数据的名字。
                if (field.Type.Type == JsonTypeEnum.Array)
                {
                    var RowsName = "Rows";
                    if (config.ExamplesToType && field.Type.Type == JsonTypeEnum.String &&
                        field.JsonMemberName != "@export_path")
                        sw.WriteLine(prefix + "public {0} {1} ;", GetTypeFromExample(field.GetExamplesText()),
                            RowsName);
                    else
                        sw.WriteLine(prefix + "public {0} {1} ;", field.Type.GetTypeName(), RowsName);
                }
                else
                {
                    //使用模板Example值作为类型
                    //export_path不作为类型导出
                    if (config.ExamplesToType && field.Type.Type == JsonTypeEnum.String &&
                        field.JsonMemberName != "@export_path")
                        sw.WriteLine(prefix + "public {0} {1} ;", GetTypeFromExample(field.GetExamplesText()),
                            field.MemberName);
                    else
                        sw.WriteLine(prefix + "public {0} {1} ;", field.Type.GetTypeName(), field.MemberName);
                }
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
}