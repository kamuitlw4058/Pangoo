using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pangoo.Core.VisualScripting;
using System.Net.NetworkInformation;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Common
{
    public class DynamicObjectMaterialState : MonoBehaviour
    {
        [Serializable]
        public class MaterialStateItem
        {
            public int State;
            public Material material;
        }

        int m_State;
        [ShowInInspector]
        public int State
        {
            get
            {
                return m_State;
            }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                    UpdateMaterial();

                }
            }
        }

        public void UpdateMaterial()
        {
            if (Renderer != null && MaterialDict != null)
            {
                if (MaterialDict.TryGetValue(m_State, out Material mat))
                {
                    Debug.Log($"Set Material State:{m_State} ");
                    Renderer.material = mat;
                }
                else
                {
                    m_State = 0;
                    if (MaterialDict.TryGetValue(m_State, out Material defaultMat))
                    {
                        Renderer.material = defaultMat;
                    }
                }

            }
        }

        [ShowInInspector]
        [HideInEditorMode]
        public Dictionary<int, Material> MaterialDict;

        public List<MaterialStateItem> MaterialStateList;

        Renderer Renderer;

        private void Start()
        {
            Renderer = GetComponent<Renderer>();
            if (MaterialDict == null)
            {
                MaterialDict = new Dictionary<int, Material>();
            }

            if (MaterialStateList != null)
            {
                foreach (var state in MaterialStateList)
                {
                    if (!MaterialDict.ContainsKey(state.State))
                    {
                        MaterialDict.Add(state.State, state.material);
                    }
                }
            }

            if (!MaterialDict.ContainsKey(0))
            {
                MaterialDict.Add(0, Renderer.material);
            }



            UpdateMaterial();




        }

    }
}