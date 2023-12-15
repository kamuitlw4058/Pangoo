#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class StaticSceneOverviewWrapper : MetaTableOverviewWrapper<StaticSceneOverview, StaticSceneDetailRowWrapper, StaticSceneRowWrapper, StaticSceneNewRowWrapper, UnityStaticSceneRow>
    {
        [Button("升级到Uuid")]
        public void UpgradeToUuuid()
        {
            foreach (var wrapper in m_AllWrappers)
            {
                var detailRowWrapper = wrapper.DetailWrapper as StaticSceneDetailRowWrapper;
                detailRowWrapper.UpgradeToUuuid();
            }

        }
    }
}
#endif

