// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    public partial interface IStaticSceneRow : IMetaTableRow
    {

        public string NameCn{ get; set; }

        public int EntityGroupId{ get; set; }

        public string AssetPathUuid{ get; set; }

        public string LoadSceneUuids{ get; set; }

        public bool UseSceneFootstep{ get; set; }

        public string Footsetp{ get; set; }

        public Vector3 Position{ get; set; }

        public Vector3 Rotation{ get; set; }

        public bool ShowOnNoPlayerEnter{ get; set; }

        public string ModelList{ get; set; }

        public string ShowType{ get; set; }

        public bool HideDefault{ get; set; }

    }
}

