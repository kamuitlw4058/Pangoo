// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;

namespace Pangoo
{
    public partial class GameSectionTable 
    {
        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {


        }

        public GameSectionRow GetGameSectionRow(int id){
            return GetRowById(id);
        }

        #if UNITY_EDITOR
        public GameSectionRow GetEditorRow(int id){
            foreach(var row in Rows){
                if(row.Id == id){
                    return row;
                }
            }
            return null;            
        }
        #endif

    }
}

