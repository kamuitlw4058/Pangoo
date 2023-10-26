using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject : MonoMasterService, IReference
    {

        public void SetTargetTransformValue(string target, TransformValue transformValue)
        {
            if (target == null || target == string.Empty || target == "Self")
            {
                Variables.transformValue = transformValue;
            }
            else
            {
                Debug.Log($"Set Child:{transformValue}");
                Variables.SetChilernTransforms(target, transformValue);
            }
        }


    }


}