﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pangoo
{
    public interface ICodeWriter
    {
        string FileExtension { get; }
        string GetTypeName(JsonType type, IJsonClassGeneratorConfig config);
        void WriteClass(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type);
        void WriteFileStart(IJsonClassGeneratorConfig config, TextWriter sw);
        void WriteFileEnd(IJsonClassGeneratorConfig config, TextWriter sw);
        void WriteMainClassStart(IJsonClassGeneratorConfig config, TextWriter sw);
        void WriteMainClassEnd(IJsonClassGeneratorConfig config, TextWriter sw);
        void WriteAdditionFunction(IJsonClassGeneratorConfig config, TextWriter sw);
        void WriteClassMembers(IJsonClassGeneratorConfig config, TextWriter sw, JsonType type, string prefix);
    }
}
