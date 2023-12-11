﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassGenerator
{
    public interface IJsonClassGeneratorConfig
    {
        string Namespace { get; set; }
        string MainClass { get; set; }

        string BaseClass { get; set; }

        string[] BaseFields { get; set; }

        bool IsUseUnityEditor { get; set; }

        bool IsAddCreateAssetMenu { get; set; }

        bool IsSerializable { get; set; }

        bool UseJsonMember { get; set; }

        string AssetMenuPrefix { get; set; }

        bool IsWriteFileHeader { get; set; }


        bool UsePascalCase { get; set; }
        bool ApplyObfuscationAttributes { get; set; }
        ICodeWriter CodeWriter { get; set; }
        bool ExamplesInDocumentation { get; set; }
        bool ExamplesToType { get; set; }
        string FilePath { get; set; }
    }
}
