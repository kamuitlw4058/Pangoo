using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.Common
{
    [Serializable]
    public class InputActionFromAssetButton : InputActionFromAsset
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
                if (action.type == InputActionType.Button)
                {
                    ret.Add(action.name);
                }
            }
            return ret;
        }

    }
}