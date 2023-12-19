using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;


namespace Pangoo.Core.VisualScripting
{


    public struct DirectInstructionGroup
    {
        [LabelText("触发器类型")]
        // [HideLabel]
        [JsonMember("TriggerType")]
        [BoxGroup("组配置")]
        public TriggerTypeEnum TriggerType;

        [JsonMember("DirectInstructionList")]
        [HideReferenceObjectPicker]
        [LabelText("指令列表")]
        [TableList]
        public DirectInstruction[] DirectInstructionList;

        [LabelText("自动开启")]
        // [HideLabel]
        // [TableColumnWidth(50, resizable: false)]
        [JsonMember("InitEnabled")]
        [BoxGroup("组配置")]
        public bool InitEnabled;


        // [TableTitleGroup("自动关闭")]
        // [TableColumnWidth(50, resizable: false)]
        [LabelText("自动关闭")]
        [JsonMember("DisableOnFinish")]
        [BoxGroup("组配置")]
        public bool DisableOnFinish;


        public static List<DirectInstructionGroup> CreateList(string s)
        {
            List<DirectInstructionGroup> ret = null;
            if (s.IsNullOrWhiteSpace())
            {
                return new List<DirectInstructionGroup>();
            }
            try
            {
                ret = JsonMapper.ToObject<List<DirectInstructionGroup>>(s); ;
            }
            catch (Exception e)
            {
                ret = new List<DirectInstructionGroup>();
            }

            return ret;
        }

        public static DirectInstructionGroup[] CreateArray(string s)
        {
            return JsonMapper.ToObject<DirectInstructionGroup[]>(s);
        }

        public void UpdateUuidById()
        {
            if (DirectInstructionList == null) return;
            for (int i = 0; i < DirectInstructionList.Length; i++)
            {
                DirectInstructionList[i].UpdateUuidById();
            }
        }

        public string Save()
        {
            return JsonMapper.ToJson(this);
        }

        public static string Save(List<DirectInstructionGroup> directInstructionGroups)
        {

            return JsonMapper.ToJson(directInstructionGroups);

        }
    }

}