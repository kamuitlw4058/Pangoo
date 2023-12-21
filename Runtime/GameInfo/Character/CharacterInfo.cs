
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using System.Collections.Generic;


namespace Pangoo
{
    [Serializable]
    public partial class CharacterInfo : BaseInfo
    {

        MetaTable.AssetPathTable m_AssetPathTable;
        MetaTable.CharacterTable m_Table;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.AssetPathTable>();
            m_Table = PangooEntry.MetaTable.GetMetaTable<MetaTable.CharacterTable>();
            foreach (var row in m_Table.BaseRows)
            {
                var characterRow = row as MetaTable.CharacterRow;
                var assetPath = m_AssetPathTable.GetRowByUuid(characterRow.AssetPathUuid);
                IdDict.Add(row.Uuid, new CharacterInfoRow(characterRow, assetPath));
            }
        }


    }
}
