
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

        AssetPathTable m_AssetPathTable;
        CharacterTable m_Table;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();
            m_Table = PangooEntry.ExcelTable.GetExcelTable<CharacterTable>();
            foreach (var row in m_Table.Rows)
            {
                var assetPath = m_AssetPathTable.GetRowById(row.AssetPathId);
                IdDict.Add(row.Id, new CharacterInfoRow(row, assetPath));
            }
        }


    }
}
