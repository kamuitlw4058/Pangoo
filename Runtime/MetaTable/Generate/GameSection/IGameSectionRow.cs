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
    public partial interface IGameSectionRow : IMetaTableRow
    {

        public string SectionJumpByScene{ get; set; }

        public string DynamicSceneUuids{ get; set; }

        public string KeepSceneUuids{ get; set; }

        public string InitSceneUuids{ get; set; }

        public string DynamicObjectUuids{ get; set; }

        public string InitedInstructionUuids{ get; set; }

        public string EditorInitedInstructionUuids{ get; set; }

        public string SceneUuids{ get; set; }

    }
}

