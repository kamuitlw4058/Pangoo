
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using System.Collections.Generic;


namespace Pangoo
{
    [Serializable]
    public partial class AssetPathInfo : BaseInfo
    {
        AssetPathTable m_AssetPathTable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();

            foreach (var row in m_AssetPathTable.Rows)
            {
                IdDict.Add(row.Id, new AssetPathInfoRow(row.ToInterface()));
            }
        }


    }
}
