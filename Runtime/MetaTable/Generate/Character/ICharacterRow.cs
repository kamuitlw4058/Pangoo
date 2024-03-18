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
    public partial interface ICharacterRow : IMetaTableRow
    {

        public string AssetPathUuid{ get; set; }

        public bool IsPlayer{ get; set; }

        public float MoveSpeed{ get; set; }

        public float CameraHeight{ get; set; }

        public Vector3 CameraOffset{ get; set; }

        public float XMaxPitch{ get; set; }

        public float YMaxPitch{ get; set; }

        public bool CameraOnly{ get; set; }

        public int AssetPathId{ get; set; }

        public string SubDynamicObject{ get; set; }

        public float CharacterSlopeLimit{ get; set; }

        public float CharacterStepOffset{ get; set; }

        public float CharacterSkinWidth{ get; set; }

        public float CharacterMinMoveDistance{ get; set; }

        public Vector3 ColliderCenter{ get; set; }

        public float ColliderRadius{ get; set; }

        public float ColliderHeight{ get; set; }

    }
}

