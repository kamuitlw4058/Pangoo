using System;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class VariableString : Variable
    {
        [SerializeField]
        [HideLabel]
        // [LabelText("参数")]
        [HideReferenceObjectPicker]
        StringParams m_Params = new StringParams();

        public override IParams Params => m_Params;


    }
}