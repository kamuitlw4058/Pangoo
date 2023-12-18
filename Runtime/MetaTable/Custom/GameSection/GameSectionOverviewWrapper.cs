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
    public partial class GameSectionOverviewWrapper : MetaTableOverviewWrapper<GameSectionOverview, GameSectionDetailRowWrapper, GameSectionRowWrapper, GameSectionNewRowWrapper, UnityGameSectionRow>
    {
        [Button("更新AssetPathUuid通过Id")]
        public void UpdateAssetPathUuidByAssetPathId()
        {
            foreach (var wrapper in m_AllWrappers)
            {
                var detailRowWrapper = wrapper.DetailWrapper as GameSectionDetailRowWrapper;
                detailRowWrapper.UpdateAssetPathUuidByAssetPathId();

            }
        }

    }
}
#endif

