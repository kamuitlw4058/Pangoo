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
    public class CSharpEventCodeWriter : CsharpCodeWriterBase
    {
        public CSharpEventCodeWriter(List<string> headers)
        {
            m_Headers = headers;
        }
        

        public override void WriteMainClassStart(IJsonClassGeneratorConfig config, TextWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("namespace {0}", config.Namespace);
            sw.WriteLine("{");
            sw.WriteLine("     [Serializable]");
            sw.WriteLine("    {0} partial class {1} : GameEventArgs", "public", JsonClassGenerator.ToTitleCase(config.MainClass));
            sw.WriteLine("    {");
        }

        public override void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw)
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
    }
}
