using System;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class VariableFloat : Variable
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        FloatParams m_Params = new FloatParams();

        public override IParams Params => this.m_Params;


    }
}