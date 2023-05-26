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


        public CSharpCodeWriter(List<string> headers, ExcelTableData excelData)
        {
            m_Headers = headers;
            m_ExcelData = excelData;
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
            sw.WriteLine("    {0} partial class {1} : ExcelTableBase", "public",
                JsonClassGenerator.ToTitleCase(config.MainClass));
            sw.WriteLine("    {");
        }

        public void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            #region BuildCSV

            string NameListString = "";
            foreach (var str in m_ExcelData.NameList)
            {
                if (NameListString != "")
                {
                    NameListString += "\",\"";
                }

                NameListString += str;
            }

            string cnNameListString = "";
            foreach (var str in m_ExcelData.CnNameLst)
            {
                if (cnNameListString != "")
                {
                    cnNameListString += "\",\"";
                }

                cnNameListString += str;
            }

            string typeNameListString = "";
            foreach (var str in m_ExcelData.TypeList)
            {
                if (typeNameListString != "")
                {
                    typeNameListString += "\",\"";
                }

                typeNameListString += str;
            }

            sw.WriteLine();
            sw.WriteLine("        /// <summary> 获取表头 </summary>");
            sw.WriteLine("        public override string[] GetHeadNames()");
            sw.WriteLine("        {");
            sw.WriteLine("            return new string[]{0};", "{\"" + NameListString + "\"}");
            sw.WriteLine("        }");
            sw.WriteLine();

            sw.WriteLine();
            sw.WriteLine("        /// <summary> 获取类型名 </summary>");
            sw.WriteLine("        public override string[] GetTypeNames()");
            sw.WriteLine("        {");
            sw.WriteLine("            return new string[]{0};", "{\"" + typeNameListString + "\"}");
            sw.WriteLine("        }");
            sw.WriteLine();

            sw.WriteLine();
            sw.WriteLine("        /// <summary> 获取描述名 </summary>");
            sw.WriteLine("        public override string[] GetDescNames()");
            sw.WriteLine("        {");
            sw.WriteLine("            return new string[]{0};", "{\"" + cnNameListString + "\"}");
            sw.WriteLine("        }");
            sw.WriteLine();
            
            sw.WriteLine();
            sw.WriteLine("        /// <summary> 获取表的每行数据 </summary>");
            sw.WriteLine("        public override List<string[]> GetTableRowDataList()");
            sw.WriteLine("        {");
            sw.WriteLine("            List<string[]> tmpRowDataList = new List<string[]>();");
            sw.WriteLine("            foreach (var item in Rows)");
            sw.WriteLine("            {");
            sw.WriteLine("                string[] texts = new string[item.GetType().GetFields().Length];");
            sw.WriteLine("                for (int i = 0; i < texts.Length; i++)");
            sw.WriteLine("                {");
            sw.WriteLine("                   object valueText = item.GetType().GetFields()[i].GetValue(item);");
            sw.WriteLine(@"                  texts[i] = valueText != null ?valueText.ToString(): "";");
            sw.WriteLine("                }");
            sw.WriteLine("                tmpRowDataList.Add(texts);");
            sw.WriteLine("            }");
            sw.WriteLine("            return tmpRowDataList;");
            sw.WriteLine("        }");
            
            sw.WriteLine("        /// <summary> 从Excel文件重新构建数据 </summary>");
            sw.WriteLine("        public virtual void LoadExcelFile(string excelFilePath)");
            sw.WriteLine("        {");
            sw.WriteLine("            Rows=new ();");
            sw.WriteLine("            var fileInfo = new FileInfo(excelFilePath);");
            sw.WriteLine("            ExcelPackage excelPackage = new ExcelPackage(fileInfo);");
            sw.WriteLine("            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];");
            sw.WriteLine("            for (int i = 3; i < worksheet.Dimension.Rows; i++)");
            sw.WriteLine("            {");
            sw.WriteLine($"                {m_ExcelData.ClassBaseName}Row  eventsRow = new {m_ExcelData.ClassBaseName}Row();");
            sw.WriteLine("                 var eventRowFieldInfos = eventsRow.GetType().GetFields();");
            sw.WriteLine("                 for (int j = 0; j < worksheet.Dimension.Columns; j++)");
            sw.WriteLine("                 {");
            sw.WriteLine("                       var value = StringConvert.ToValue(eventRowFieldInfos[j].FieldType, worksheet.Cells[i+1,j+1].Value.ToString());  //将字符串解析成指定类型");
            sw.WriteLine("                       eventRowFieldInfos[j].SetValue(eventsRow,value);");
            sw.WriteLine("                 }");
            sw.WriteLine("                 Rows.Add(eventsRow);");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
            sw.WriteLine();

            #endregion BuildCSV


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
                        sw.WriteLine(prefix + "[ShowInInspector]");
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