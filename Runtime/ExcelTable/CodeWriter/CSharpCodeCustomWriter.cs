﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pangoo
{
    public class CSharpCodeCustomWriter : CsharpCodeWriterBase
    {
        ExcelTableData m_ExcelData;
        
        public CSharpCodeCustomWriter(List<string> headers = null, ExcelTableData excelData = null)
        {
            m_Headers = headers;
            m_ExcelData = excelData;
        }

        public override void WriteFileStart(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            // foreach (var line in JsonClassGenerator.FileHeader)
            // {
            //     sw.WriteLine("// " + line);
            // }
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

        public override void WriteMainClassStart(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("namespace {0}", config.Namespace);
            sw.WriteLine("{");
            sw.WriteLine("    {0} partial class {1} ", "public", JsonClassGenerator.ToTitleCase(config.MainClass));
            sw.WriteLine("    {");
        }

        public override void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("        /// <summary> 用户处理 </summary>");
            sw.WriteLine("        public override void CustomInit()");
            sw.WriteLine("        {");
            sw.WriteLine("        }");
            sw.WriteLine();


            // sw.WriteLine("public override void Merge(ExcelTableBase val){");
            // sw.WriteLine($" var table = val as {config.MainClass};");
            // sw.WriteLine($" Rows.AddRange(table.Rows);");
            // sw.WriteLine("}");
        }



        public override void WriteClassMembers(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type, string prefix)
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

                        sw.WriteLine(prefix + $"[HorizontalGroup(\"{excelData.CnName}\")]");
                        sw.WriteLine(prefix + "[HideLabel]");
                        sw.WriteLine(prefix + "[ShowInInspector]");

                    }
                    else
                    {
                        if (field.Type.Type == JsonTypeEnum.Array)
                        {
                            sw.WriteLine(prefix + "[TableList(IsReadOnly = true, AlwaysExpanded = true)]");
                        }
                    }

                }
                if (config.UsePascalCase)
                {

                    sw.WriteLine(prefix + "[JsonMember(\"{0}\")]", field.JsonMemberName);
                }


                //sw.WriteLine(prefix + "public {0} {1} {{ get; private set; }}", field.Type.GetTypeName(), field.MemberName);

                //使用模板Example值作为类型
                //export_path不作为类型导出
                if (config.ExamplesToType && field.Type.Type == JsonTypeEnum.String && field.JsonMemberName != "@export_path")
                    sw.WriteLine(prefix + "public {0} {1} ;", GetTypeFromExample(field.GetExamplesText()), field.MemberName);
                else
                    sw.WriteLine(prefix + "public {0} {1} ;", field.Type.GetTypeName(), field.MemberName);
            }
        }
    }
}
