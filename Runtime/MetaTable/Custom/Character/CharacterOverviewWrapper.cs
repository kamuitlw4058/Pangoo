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
    public partial class CharacterOverviewWrapper : MetaTableOverviewWrapper<CharacterOverview, CharacterDetailRowWrapper, CharacterRowWrapper, CharacterNewRowWrapper, UnityCharacterRow>
    {
        [Button("更新AssetPathUuid通过Id")]
        public void UpdateAssetPathUuidByAssetPathId()
        {
            foreach (var wrapper in m_AllWrappers)
            {
                var detailRowWrapper = wrapper.DetailWrapper as CharacterDetailRowWrapper;
                detailRowWrapper.UpdateAssetPathUuidByAssetPathId();

            }
        }

    }
}
#endif

