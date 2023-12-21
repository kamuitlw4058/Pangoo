
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
        MetaTable.AssetPathTable m_AssetPathTable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.AssetPathTable>();

            foreach (var row in m_AssetPathTable.BaseRows)
            {
                IdDict.Add(row.Uuid, row);
            }
        }


    }
}
