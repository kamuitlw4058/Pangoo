using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


namespace Pangoo
{
    public partial class AssetPathTable 
    {
        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {

        }

        public AssetPathRow GetAssetPathRow(int id){
            return GetRowById(id);

        }

    }
}

