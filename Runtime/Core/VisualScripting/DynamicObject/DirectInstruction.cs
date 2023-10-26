using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;


namespace Pangoo.Core.VisualScripting
{
    public enum DirectInstructionTypeEnum
    {
        Unknown,
        [LabelText("动态物体播放Timeline")]
        DynamicObjectPlayTimeline,
    }


    public struct DirectInstruction
    {
        [TableTitleGroup("触发器类型")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        public TriggerTypeEnum TriggerType;

        [TableTitleGroup("指令类型")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        public DirectInstructionTypeEnum InstructionType;

        [ValueDropdown("OnMainIntValueDropdown")]
        [TableTitleGroup("Int参数1")]
        [LabelText("$Int1Label")]
        [ShowIf("$IsMainIntShow")]
        [LabelWidth(50)]
        public int Int1;

        [TableTitleGroup("自动开启")]
        [HideLabel]
        [TableColumnWidth(50, resizable: false)]
        public bool InitEnabled;


        [TableTitleGroup("自动关闭")]
        [TableColumnWidth(50, resizable: false)]
        [HideLabel]
        public bool DisableOnFinish;

#if UNITY_EDITOR
        public bool IsMainIntShow
        {
            get
            {
                return InstructionType == DirectInstructionTypeEnum.DynamicObjectPlayTimeline;
            }
        }

        public string Int1Label
        {
            get
            {
                return "动态物体";
            }
        }




        public IEnumerable OnMainIntValueDropdown()
        {
            switch (InstructionType)
            {
                case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                    return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
            }

            return null;
        }
#endif

        public static DirectInstruction Create(string s)
        {
            DirectInstruction ret = new DirectInstruction();
            var datas = s.Split(',');
            ret.TriggerType = (TriggerTypeEnum)datas[0].ToIntForce();
            ret.InstructionType = (DirectInstructionTypeEnum)datas[1].ToIntForce();
            if (datas.Length >= 3) ret.Int1 = datas[2].ToIntForce();
            if (datas.Length >= 4) ret.InitEnabled = datas[3].ToBoolForce();
            if (datas.Length >= 5) ret.DisableOnFinish = datas[4].ToBoolForce();

            return ret;
        }


        public static DirectInstruction[] CreateArray(string s)
        {
            if (s.IsNullOrWhiteSpace())
            {
                return new DirectInstruction[0];
            }
            var instructionStrs = s.Split('|');
            DirectInstruction[] instructions = new DirectInstruction[instructionStrs.Length];
            for (int i = 0; i < instructionStrs.Length; i++)
            {
                instructions[i] = DirectInstruction.Create(s);
            }
            return instructions;
        }

        public static List<DirectInstruction> CreateList(string s)
        {
            return CreateArray(s).ToList();
        }

        public bool IsVaild
        {
            get
            {
                if (TriggerType == TriggerTypeEnum.Unknown)
                {
                    return false;
                }

                if (InstructionType == DirectInstructionTypeEnum.Unknown)
                {
                    return false;
                }


                return true;
            }
        }

        public static string Save(List<DirectInstruction> directInstructions)
        {
            List<DirectInstruction> ret = new List<DirectInstruction>();
            foreach (var instruction in directInstructions)
            {
                if (instruction.IsVaild)
                {
                    ret.Add(instruction);
                }
            }
            ret.Sort((a, b) =>
            {
                int order = a.TriggerType.CompareTo(b.TriggerType);
                if (order != 0)
                {
                    return order;
                }

                order = a.InstructionType.CompareTo(b.InstructionType);
                if (order != 0)
                {
                    return order;
                }
                order = a.Int1.CompareTo(b.Int1);
                if (order != 0)
                {
                    return order;
                }

                order = a.InitEnabled.CompareTo(b.InitEnabled);
                if (order != 0)
                {
                    return order;
                }

                order = a.DisableOnFinish.CompareTo(b.DisableOnFinish);
                if (order != 0)
                {
                    return order;
                }

                return order;
            });
            StringBuilder sb = new StringBuilder();
            foreach (var instruction in ret)
            {
                sb.Append(((int)instruction.TriggerType).ToString());
                sb.Append(",");
                sb.Append(((int)instruction.InstructionType).ToString());
                sb.Append(",");
                sb.Append(instruction.Int1.ToString());
                sb.Append(",");
                sb.Append(instruction.InitEnabled.ToString());
                sb.Append(",");
                sb.Append(instruction.DisableOnFinish.ToString());
                sb.Append("|");
            }
            return sb.ToString().TrimEnd('|');
        }
    }

}