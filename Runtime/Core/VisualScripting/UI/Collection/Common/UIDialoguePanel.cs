using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Pangoo.Core.Common;
using Pangoo.Common;
using System.Linq;
using Pangoo.MetaTable;
using System;
using LitJson;


namespace Pangoo.Core.VisualScripting
{


    [Serializable]
    public class DialogueUpdateData
    {
        public DialogueData DialogueData;

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

    [Category("通用/对话")]

    public class UIDialoguePanel : UIPanel
    {


        // public List<DialogueUpdateData> dialogueUpdateDatas = new List<DialogueUpdateData>();

        public UIDialogueParams ParamsRaw = new UIDialogueParams();

        protected override IParams Params => ParamsRaw;

        public TextMeshProUGUI m_Subtitle;

        public List<TextMeshProUGUI> m_Options = new List<TextMeshProUGUI>();

        public int OptionMaxCount;

        public int CurrentOptionIndex;


        // public DialogueData Data;

        public List<DialogueData> DataList = new List<DialogueData>();


        public float CurrentTime;


        [ShowInInspector]
        public string SubtitleText
        {
            get
            {
                return m_Subtitle?.text;
            }
            set
            {
                if (m_Subtitle != null)
                {
                    m_Subtitle.text = value;
                }
            }
        }


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

        public bool PlayerControllable
        {
            get
            {
                return PanelData.Main.CharacterService.PlayerControllable;
            }
            set
            {
                PanelData.Main.CharacterService.PlayerControllable = value;
            }
        }

        public IDialogueRow GetDialogueByUuid(string uuid)
        {
            return PanelData.Main.MetaTable.GetDialogueByUuid(uuid);
        }

        public IActorsLinesRow GetActorsLinesByUuid(string uuid)
        {
            return PanelData.Main.MetaTable.GetActorsLinesByUuid(uuid);
        }




        public void UpdateDialogue()
        {
            if (DataList.Count == 0)
            {
                // FinishDialogue();
                return;
            }

            var LastData = DataList.Last();
            if (LastData.dialogueUpdateDatas.Count == 0)
            {
                return;
            }

            var lastDialogue = LastData.dialogueUpdateDatas.Last();
            if (lastDialogue.DialogueRow.DialogueType == DialogueType.ActorsLines.ToString())
            {
                CloseOptions();

                if (lastDialogue.ActorsLinesRow == null)
                {
                    lastDialogue.ActorsLinesRow = GetActorsLinesByUuid(lastDialogue.DialogueRow.ActorsLinesUuid);
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
                            SubtitleText = subtitle.Content;
                        }
                    }


                    if (subtitle.InfoType == DialogueSubtitleType.Sound)
                    {
                        if (MathUtility.IsInRange(subtitle.Range, CurrentTime - lastDialogue.StartTime) && lastDialogue.audioUuid.IsNullOrWhiteSpace())
                        {
                            lastDialogue.audioUuid = subtitle.SoundUuid;
                            PanelData.Main.Sound.PlaySound(subtitle.SoundUuid, () =>
                            {
                                lastDialogue.audioUuid = string.Empty;
                            });
                        }
                    }

                }
                lastDialogue.Progress = Mathf.InverseLerp(0, lastDialogue.ActorsLinesRow.Duration, CurrentTime - lastDialogue.StartTime);

                if ((CurrentTime - lastDialogue.StartTime) > lastDialogue.ActorsLinesRow.Duration && lastDialogue.audioUuid.IsNullOrWhiteSpace())
                {

                    if (!InsertDialogueUpdateDataRow(lastDialogue.DialogueRow.NextDialogueUuid, LastData))
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

        public DialogueData LastData
        {
            get
            {
                var data = DataList.Last();
                if (data != null)
                {
                    return data;
                }

                return null;
            }

        }


        void FinishDialogue()
        {
            Debug.Log($"End Dialogue");
            var data = LastData;
            if (data != null)
            {
                DataList.Remove(data);
            }

            if (data != null && !data.DontControllPlayer)
            {
                PlayerControllable = true;
            }

            SubtitleText = string.Empty;
            RecoverCursor();
            CloseOptions();

            var nextData = LastData;
            if (nextData != null)
            {
                if (!nextData.DontControllPlayer)
                {
                    PlayerControllable = false;
                }

                if (nextData.ShowCursor)
                {
                    ShowCursor();
                }
            }

        }

        public bool InsertDialogueUpdateDataRow(string dialogueUuid, DialogueData data)
        {
            if (dialogueUuid.IsNullOrWhiteSpace())
            {
                return false;
            }
            var dialogue = GetDialogueByUuid(dialogueUuid);
            if (dialogue != null)
            {
                InsertDialogueUpdateData(dialogue, data);
                return true;
            }

            return false;
        }


        public void InsertDialogueUpdateData(IDialogueRow row, DialogueData data)
        {
            DialogueUpdateData updateData = new DialogueUpdateData();
            updateData.DialogueData = data;
            updateData.DialogueRow = row;
            updateData.StartTime = CurrentTime;
            updateData.Progress = 0;
            data.dialogueUpdateDatas.Add(updateData);
        }


        public void InsertDialogue(DialogueData data)
        {
            if (data == null) return;

            if (!data.DontControllPlayer)
            {
                PlayerControllable = false;
            }
            InsertDialogueUpdateData(data.DialogueRow, data);
            ShowCursor();
            DataList.Add(data);
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            m_Subtitle = transform.Find("Subtitle").GetComponent<TextMeshProUGUI>();
            m_Options.Clear();
            for (int i = 1; i <= OptionMaxCount; i++)
            {
                var path = $"VerticalLayout/Option{i}";
                var option = transform.Find($"VerticalLayout/Option{i}")?.GetComponent<TextMeshProUGUI>();
                if (option == null)
                {
                    Debug.LogError($"path:{path} no find");
                    return;
                }
                m_Options.Add(option);
            }

            SubtitleText = string.Empty;
            CloseOptions();
        }


        public DialogueUpdateData LastDialogueUpdateData
        {
            get
            {

                if (DataList.Count == 0)
                {
                    return null;
                }

                var LastData = DataList.Last();
                if (LastData.dialogueUpdateDatas.Count == 0)
                {
                    return null;
                }

                return LastData.dialogueUpdateDatas.Last();
            }
        }

        void CheckOptionIndex()
        {

            var lastDialogue = LastDialogueUpdateData;
            if (lastDialogue == null) return;

            if (lastDialogue.DialogueRow.DialogueType == DialogueType.Option.ToString() && lastDialogue.dialogueOptionInfos != null)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    CurrentOptionIndex += 1;
                }


                if (Input.GetKeyDown(KeyCode.S))
                {
                    CurrentOptionIndex -= 1;
                }

                if (CurrentOptionIndex >= 0)
                {
                    CurrentOptionIndex %= lastDialogue.dialogueOptionInfos.Count;
                }
                else
                {
                    CurrentOptionIndex = lastDialogue.dialogueOptionInfos.Count - 1;
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    DoSelect();
                }

            }
        }

        [Button]
        void DoSelect()
        {
            var lastDialogue = LastDialogueUpdateData;
            if (lastDialogue == null) return;


            if (lastDialogue.DialogueRow.DialogueType == DialogueType.Option.ToString() && lastDialogue.dialogueOptionInfos != null)
            {
                var currentOption = lastDialogue.dialogueOptionInfos[CurrentOptionIndex];
                if (currentOption.NextDialogueUuid.IsNullOrWhiteSpace())
                {
                    FinishDialogue();
                }
                else
                {
                    if (!InsertDialogueUpdateDataRow(currentOption.NextDialogueUuid, lastDialogue.DialogueData))
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
            CheckOptionIndex();
        }




    }
}