using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;
using System.ComponentModel;


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

        public T GetVariable<T>(string uuid)
        {
            var row = VariableRowExtension.GetByUuid(uuid, m_VariableHandler);
            if (row != null)
            {
                switch (row.VariableType.ToEnum<VariableTypeEnum>())
                {
                    case VariableTypeEnum.DynamicObject:
                        return Variables.GetVariable<T>(uuid);
                    case VariableTypeEnum.Global:
                        return RuntimeData.GetVariable<T>(uuid);
                }
            }

            return default(T);
        }

        public void SetVariable<T>(string uuid, T val)
        {
            var row = VariableRowExtension.GetByUuid(uuid, m_VariableHandler);
            Debug.Log($"Set Value:{row}");
            if (row != null)
            {
                switch (row.VariableType.ToEnum<VariableTypeEnum>())
                {
                    case VariableTypeEnum.DynamicObject:
                        Variables.SetVariable<T>(uuid, val);
                        break;
                    case VariableTypeEnum.Global:
                        RuntimeData.SetVariable<T>(uuid, val);
                        break;
                }
            }
        }



    }


}