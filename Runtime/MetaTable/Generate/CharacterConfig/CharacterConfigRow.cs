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
    [Serializable]
    public partial class CharacterConfigRow : MetaTableRow,ICharacterConfigRow
    {

        [JsonMember("WalkSpeed")]
        [MetaTableRowColumn("WalkSpeed","float", "走路速度",1)]
        [LabelText("走路速度")]
        public float WalkSpeed ;

        float ICharacterConfigRow.WalkSpeed {get => WalkSpeed; set => WalkSpeed = value;}

        [JsonMember("RunSpeed")]
        [MetaTableRowColumn("RunSpeed","float", "跑步速度",2)]
        [LabelText("跑步速度")]
        public float RunSpeed ;

        float ICharacterConfigRow.RunSpeed {get => RunSpeed; set => RunSpeed = value;}

        [JsonMember("NoiseProfile")]
        [MetaTableRowColumn("NoiseProfile","string", "噪声配置",3)]
        [LabelText("噪声配置")]
        public string NoiseProfile ;

        string ICharacterConfigRow.NoiseProfile {get => NoiseProfile; set => NoiseProfile = value;}

    }
}

