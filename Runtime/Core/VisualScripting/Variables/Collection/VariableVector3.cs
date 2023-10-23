using System;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class VariableVector3 : Variable
    {
        [SerializeField]
        [HideLabel]
        // [LabelText("参数")]
        [HideReferenceObjectPicker]
        Vector3Params m_Params = new Vector3Params();

        public override IParams Params => m_Params;


    }
}