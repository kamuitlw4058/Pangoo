using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;
using Pangoo.Common;
using System.Linq;
using System.Runtime.InteropServices;
using Pangoo.MetaTable;
using System;
using LitJson;


namespace Pangoo.Core.VisualScripting
{
    [Category("通用/对话")]

    public class UIDialoguePanel : UIPanel
    {
        [Serializable]
        public class DialogueUpdateData
        {
            [ShowInInspector]
            public IDialogueRow DialogueRow;

            [ShowInInspector]
            public DialogueType DialogueType
            {
                get
                {
                    return DialogueRow.DialogueType.ToEnum<DialogueType>();
                }
            }

            [ShowInInspector]
            public IActorsLinesRow ActorsLinesRow;

            public List<DialogueSubtitleInfo> dialogueSubtitleInfos;

            public List<DialogueOptionInfo> dialogueOptionInfos;

            public string audioUuid;

            public bool audioClipStarted;

            public float StartTime;

            public float EndTime;

            public float Progress;
        }

        public List<DialogueUpdateData> dialogueUpdateDatas = new List<DialogueUpdateData>();

        public UIDialogueParams ParamsRaw = new UIDialogueParams();

        protected override IParams Params => ParamsRaw;

        public TextMeshProUGUI m_Subtitle;

        public List<TextMeshProUGUI> m_Options = new List<TextMeshProUGUI>();

        public int OptionMaxCount;

        public int CurrentOptionIndex;


        public DialogueData Data;


        public float CurrentTime;


        public void CloseOptions()
        {
            foreach (var option in m_Options)
            {
                option.gameObject.SetActive(false);
            }
        }

        public void SetOptionActive(int optionCount)
        {
            for (int i = 0; i < m_Options.Count; i++)
            {
                m_Options[i].gameObject.SetActive((i + 1) <= optionCount);
            }

        }

        public void SetOptionText(int index, string option)
        {
            m_Options[index].text = CurrentOptionIndex == index ? $"> {option}" : option;
        }




        public void UpdateDialogue()
        {
            if (dialogueUpdateDatas.Count == 0)
            {
                FinishDialogue();
                return;
            }

            var lastDialogue = dialogueUpdateDatas.Last();
            if (lastDialogue.DialogueRow.DialogueType == DialogueType.ActorsLines.ToString())
            {
                CloseOptions();

                if (lastDialogue.ActorsLinesRow == null)
                {
                    lastDialogue.ActorsLinesRow = Data.args.Main.MetaTable.GetActorsLinesByUuid(lastDialogue.DialogueRow.ActorsLinesUuid);
                }

                if (lastDialogue.ActorsLinesRow == null)
                {
                    FinishDialogue();
                    return;
                }

                if (lastDialogue.dialogueSubtitleInfos == null)
                {
                    try
                    {
                        lastDialogue.dialogueSubtitleInfos = JsonMapper.ToObject<List<DialogueSubtitleInfo>>(lastDialogue.ActorsLinesRow.DialogueSubtitles);
                    }
                    catch
                    {
                        lastDialogue.dialogueSubtitleInfos = null;
                    }

                }

                if (lastDialogue.dialogueSubtitleInfos == null)
                {
                    FinishDialogue();
                    return;
                }

                foreach (var subtitle in lastDialogue.dialogueSubtitleInfos)
                {
                    if (subtitle.InfoType == DialogueSubtitleType.Subtitle)
                    {
                        if (MathUtility.IsInRange(subtitle.Range, CurrentTime - lastDialogue.StartTime))
                        {
                            m_Subtitle.text = subtitle.Content;
                        }
                    }


                    if (subtitle.InfoType == DialogueSubtitleType.Sound)
                    {
                        if (MathUtility.IsInRange(subtitle.Range, CurrentTime - lastDialogue.StartTime) && lastDialogue.audioUuid.IsNullOrWhiteSpace())
                        {
                            lastDialogue.audioUuid = subtitle.SoundUuid;
                            Data.args.Main.Sound.PlaySound(subtitle.SoundUuid, () =>
                            {
                                lastDialogue.audioUuid = string.Empty;
                            });
                        }
                    }

                }
                lastDialogue.Progress = Mathf.InverseLerp(0, lastDialogue.ActorsLinesRow.Duration, CurrentTime - lastDialogue.StartTime);

                if ((CurrentTime - lastDialogue.StartTime) > lastDialogue.ActorsLinesRow.Duration && lastDialogue.audioUuid.IsNullOrWhiteSpace())
                {

                    if (!InsertDialogueUpdateDataRow(lastDialogue.DialogueRow.NextDialogueUuid))
                    {
                        FinishDialogue();
                    }

                }
            }

            if (lastDialogue.DialogueRow.DialogueType == DialogueType.Option.ToString())
            {
                if (lastDialogue.dialogueOptionInfos == null)
                {
                    lastDialogue.dialogueOptionInfos = JsonMapper.ToObject<List<DialogueOptionInfo>>(lastDialogue.DialogueRow.Options);
                }

                if (lastDialogue.dialogueOptionInfos == null)
                {
                    FinishDialogue();
                    return;
                }

                var optionCount = lastDialogue.dialogueOptionInfos.Count();
                SetOptionActive(optionCount);
                for (int i = 0; i < optionCount; i++)
                {
                    var optionInfo = lastDialogue.dialogueOptionInfos[i];
                    SetOptionText(i, optionInfo.Option);
                }
            }



        }

        void FinishDialogue()
        {
            Debug.Log($"End Dialogue");
            Data.args.Main.CharacterService.SetPlayerControllable(false);
            CloseSelf();
        }

        public bool InsertDialogueUpdateDataRow(string dialogueUuid)
        {
            if (dialogueUuid.IsNullOrWhiteSpace())
            {
                return false;
            }
            var dialogue = Data.args.Main.MetaTable.GetDialogueByUuid(dialogueUuid);
            if (dialogue != null)
            {
                InsertDialogueUpdateData(dialogue);
                return true;
            }

            return false;
        }


        public void InsertDialogueUpdateData(IDialogueRow row)
        {
            DialogueUpdateData updateData = new DialogueUpdateData();
            updateData.DialogueRow = row;
            updateData.StartTime = CurrentTime;
            updateData.Progress = 0;
            dialogueUpdateDatas.Add(updateData);
        }



        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            Data = PanelData.UserData as DialogueData;
            if (Data == null) return;



            m_Subtitle = transform.Find("Subtitle").GetComponent<TextMeshProUGUI>();
            m_Options.Clear();
            dialogueUpdateDatas.Clear();
            for (int i = 1; i <= OptionMaxCount; i++)
            {
                var path = $"VerticalLayout/Option{i}";
                var option = transform.Find($"VerticalLayout/Option{i}")?.GetComponent<TextMeshProUGUI>();
                if (option == null)
                {
                    Debug.LogError($"path:{path} no find");
                    FinishDialogue();
                    return;
                }
                m_Options.Add(option);
            }

            Data.args.Main.CharacterService.SetPlayerControllable(false);
            InsertDialogueUpdateData(Data.DialogueRow);
            SetupCursor();

        }

        [Button]
        void DoSelect()
        {
            var lastDialogue = dialogueUpdateDatas.Last();
            if (lastDialogue.DialogueRow.DialogueType == DialogueType.Option.ToString() && lastDialogue.dialogueOptionInfos != null)
            {
                var currentOption = lastDialogue.dialogueOptionInfos[CurrentOptionIndex];
                if (currentOption.NextDialogueUuid.IsNullOrWhiteSpace())
                {
                    FinishDialogue();
                }
                else
                {
                    if (!InsertDialogueUpdateDataRow(currentOption.NextDialogueUuid))
                    {
                        FinishDialogue();
                    }
                }
            }
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            CurrentTime += elapseSeconds;
            UpdateDialogue();


        }




    }
}