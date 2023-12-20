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
    public partial class CharacterRowWrapper : MetaTableRowWrapper<CharacterOverview, CharacterNewRowWrapper, UnityCharacterRow>
    {
        [ShowInInspector]
        public int AssetPathId
        {
            get
            {
                return UnityRow.Row.AssetPathId;
            }
        }

        [ShowInInspector]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row.AssetPathUuid;
            }
        }

    }
}
#endif

