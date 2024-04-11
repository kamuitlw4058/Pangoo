using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using Pangoo.Core.Services;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    public class GameSectionValue : TargetValue
    {
        public override VariableTypeEnum TargetVariableType => VariableTypeEnum.GameSection;

    }
}