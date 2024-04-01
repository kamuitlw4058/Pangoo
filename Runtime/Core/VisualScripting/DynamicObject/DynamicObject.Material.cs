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
        [ShowInInspector]
        [FoldoutGroup("材质状态")]
        [LabelText("材质列表")]
        DynamicObjectMaterialState[] MaterialStateList;

        public void DoAwakeMaterial()
        {
            MaterialStateList = CachedTransfrom.GetComponentsInChildren<DynamicObjectMaterialState>(includeInactive: true);
            Log($"{gameObject.name} Get MaterialStateList:{MaterialStateList}");
        }

        public void SetMaterialState(int state)
        {
            if (MaterialStateList != null)
            {
                foreach (var matState in MaterialStateList)
                {
                    matState.State = state;
                }
            }
        }
    }


}