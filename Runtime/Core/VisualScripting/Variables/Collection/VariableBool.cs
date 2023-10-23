using System;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class VariableBool : Variable
    {
        [SerializeField]
        [HideLabel]
        // [LabelText("参数")]
        [HideReferenceObjectPicker]
        BoolParams m_Params = new BoolParams();

        public override IParams Params => this.m_Params;

    }
}