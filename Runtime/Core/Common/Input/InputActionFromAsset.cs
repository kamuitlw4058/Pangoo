using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.Common
{
    [Serializable]
    public class InputActionFromAsset
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] protected InputActionAsset m_InputAsset;
        [SerializeField]
        [ValueDropdown("ActionMapList")]
        protected string m_ActionMap;
        [SerializeField]
        [ValueDropdown("ActionList")]
        protected string m_Action;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] protected InputAction m_InputAction;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public InputAction InputAction
        {
            get
            {
                if (this.m_InputAction != null) return this.m_InputAction;

                if (this.m_InputAsset == null)
                {
                    Debug.LogError("Input Action Asset not found");
                    return null;
                }

                if (string.IsNullOrEmpty(this.m_ActionMap))
                {
                    this.m_InputAction = this.m_InputAsset.FindAction(this.m_Action);
                    return this.m_InputAction;
                }

                InputActionMap map = this.m_InputAsset.FindActionMap(this.m_ActionMap);
                if (map != null)
                {
                    this.m_InputAction = map.FindAction(this.m_Action);
                    return this.m_InputAction;
                }

                Debug.LogErrorFormat(
                    "Unable to find Input Action for asset: {0}. Map: {1} and Action: {2}",
                    this.m_InputAsset != null ? this.m_InputAsset.name : "(null)",
                    this.m_ActionMap,
                    this.m_Action
                );

                return null;
            }
        }

        // STRING: --------------------------------------------------------------------------------

        public override string ToString()
        {
            return this.m_InputAsset != null ? this.m_InputAsset.name : "(none)";
        }

        public IEnumerable ActionMapList()
        {
            if (m_InputAsset == null)
            {
                return null;
            }

            var ret = new ValueDropdownList<string>();

            foreach (var map in m_InputAsset.actionMaps)
            {
                ret.Add(map.name);
            }
            return ret;
        }

        public virtual IEnumerable ActionList()
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
                ret.Add(action.name);
            }
            return ret;
        }

    }
}