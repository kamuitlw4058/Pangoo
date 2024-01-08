using System;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class VariableInt : Variable
    {
        [SerializeField]
        [HideLabel]
        // [LabelText("参数")]
        [HideReferenceObjectPicker]
        IntParams m_Params = new IntParams();

        public override IParams Params => this.m_Params;

    }
}