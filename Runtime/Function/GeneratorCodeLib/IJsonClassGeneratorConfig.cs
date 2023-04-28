﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pangoo
{
    public interface IJsonClassGeneratorConfig
    {
        string Namespace { get; set; }
        string MainClass { get; set; }
        bool UsePascalCase { get; set; }
        bool ApplyObfuscationAttributes { get; set; }
        ICodeWriter CodeWriter { get; set; }
        bool ExamplesInDocumentation { get; set; }
        bool ExamplesToType { get; set; }
        string FilePath { get; set; }
    }
}
