using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Pangoo.Core.Common
{
    [Serializable]
    public class InputActionFromAssetVector2 : InputActionFromAsset
    {
        public override IEnumerable ActionList()
        {
            if (m_InputAsset == null)
            {
                return null;
            }

            if (m_ActionMap.IsNullOrWhiteSpace())
            {
                return null;

            }

            var ret = new ValueDropdownList<string>();

            var actionMap = this.m_InputAsset.FindActionMap(m_ActionMap);
            foreach (var action in actionMap.actions)
            {
                foreach (var control in action.controls)
                {
                    if (control is Vector2Control)
                    {
                        ret.Add(action.name);
                        break;
                    }
                }
            }
            return ret;
        }

    }
}