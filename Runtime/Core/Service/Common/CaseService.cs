using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Pangoo.Common;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Pangoo.Core.Common;


namespace Pangoo.Core.Services
{
    public class CaseService : MainSubService
    {
        public override int Priority => 7;


        public Dictionary<string, CaseContent> CaseDict = new Dictionary<string, CaseContent>();

        public string LastShowCase;
        // #if SIDE_EFFECT
        public UISideEffectPanel Panel;

        // #else
        //         public UICasePanel Panel;
        // #endif
        protected override void DoStart()
        {


            if (GameMainConfigSrv.GameMainConfig.CaseClueHasVariable.IsNullOrWhiteSpace())
            {
                LogError($"线索拥有变量未设置!");
            }

            if (GameMainConfigSrv.GameMainConfig.CaseClueHasVariable.IsNullOrWhiteSpace())
            {
                LogError($"线索移除变量未设置!");
            }

            var panelUuid = GameMainConfigSrv.GameMainConfig?.CasePanelUuid;

            if (!panelUuid.IsNullOrWhiteSpace())
            {
                // #if SIDE_EFFECT
                UISrv.ShowUI(panelUuid, showAction: (o) =>
                {
                    Panel = o as UISideEffectPanel;
                });
                // #else
                //                 UISrv.ShowUI(panelUuid, showAction: (o) =>
                //                 {
                //                     Panel = o as UICasePanel;
                //                 });
                // #endif
            }

        }


        CaseContent BuildCaseContent(string uuid)
        {

            var ret = new CaseContent();
            ret.args = new Common.Args
            {
                Main = Parent as MainService
            };
            ret.CaseUuid = uuid;
            ret.CaseRow = MetaTableSrv.GetCaseByUuid(uuid);
            var clueUuids = ret.CaseRow.CaseClues.ToSplitArr<string>();
            ret.CluesRows = new IClueRow[clueUuids.Length];
            for (int i = 0; i < clueUuids.Length; i++)
            {
                var clueUuid = clueUuids[i];
                var clueRow = MetaTableSrv.GetClueRowByUuid(clueUuid);
                ret.CluesRows[i] = clueRow;
                ret.DynamicObject2Clue.Add(clueRow.DynamicObjectUuid, clueUuid);
                ret.ClueDict.Add(clueUuid, clueRow);

            }
            return ret;
        }



        public void ShowCase(string uuid, Action<CaseContent> showFinishedCallback = null)
        {
            LastShowCase = uuid;
            if (CaseDict.TryGetValue(uuid, out CaseContent content))
            {
                OnShowCaseComplete(content, showFinishedCallback);
            }
            else
            {
                var newContent = BuildCaseContent(uuid);
                ShowCaseByContent(newContent, showFinishedCallback);
            }

        }

        public void OnShowCaseComplete(CaseContent content, Action<CaseContent> showFinishedCallback = null)
        {
            if (content.Entity == null)
            {
                showFinishedCallback?.Invoke(null);
                return;
            }

            content.CaseModelActive = true;
            Panel.ShowCase(content);
            showFinishedCallback?.Invoke(content);
            if (!CaseDict.ContainsKey(content.CaseUuid))
            {
                CaseDict.Add(content.CaseUuid, content);
            }
        }

        void ShowCaseByContent(CaseContent content, Action<CaseContent> showFinishedCallback = null)
        {

            var dynamicObjectUuid = content.CaseRow?.DynamicObjectUuid;
            if (!dynamicObjectUuid.IsNullOrWhiteSpace())
            {
                DynamicObjectSrv.ShowEntity(dynamicObjectUuid, null, null, "Case", (o) =>
                {
                    content.Entity = o as EntityDynamicObject;
                    OnShowCaseComplete(content, showFinishedCallback);
                });
            }
        }


        protected override void DoUpdate()
        {
            foreach (var kv in CaseDict)
            {
                if (!kv.Value.AllCluesLoaded)
                {
                    foreach (var clueRow in kv.Value.CluesRows)
                    {
                        DynamicObjectSrv.ShowEntity(clueRow.DynamicObjectUuid, kv.Value.Entity, null, "Case", (o) =>
                        {
                            var entity = o as EntityDynamicObject;
                            if (entity != null)
                            {
                                entity.DynamicObj.ModelActive = false;
                                entity.transform.localPosition = Vector3.zero;
                                kv.Value.CluesEntity.Add(clueRow.DynamicObjectUuid, entity);
                            }

                        });
                    }

                }
            }
        }
        public string CaseClueHasVariable
        {
            get
            {
                return GameMainConfigSrv.GameMainConfig.CaseClueHasVariable;
            }
        }

        public string CaseClueIsRemovedVariable
        {
            get
            {
                return GameMainConfigSrv.GameMainConfig.CaseClueIsRemovedVariable;
            }
        }


        public string GetClueKey(string ClueKey, string valKey)
        {
            return String.Join(".", "Clue", ClueKey, valKey);
        }


        public bool GetClueHas(string uuid)
        {
            var row = MetaTableSrv.GetClueRowByUuid(uuid);
            return RuntimeDataSrv.GetDynamicObjectVariable<bool>(row.DynamicObjectUuid, CaseClueHasVariable);
        }

        public void SetClueHas(string uuid, bool val)
        {
            var row = MetaTableSrv.GetClueRowByUuid(uuid);
            RuntimeDataSrv.SetDynamicObjectVariable<bool>(row.DynamicObjectUuid, CaseClueHasVariable, val);
        }

        public bool GetClueIsRemoved(string uuid)
        {
            var row = MetaTableSrv.GetClueRowByUuid(uuid);
            return RuntimeDataSrv.GetDynamicObjectVariable<bool>(row.DynamicObjectUuid, CaseClueIsRemovedVariable);
        }

        public void SetClueIsRemoved(string uuid, bool val)
        {
            var row = MetaTableSrv.GetClueRowByUuid(uuid);
            RuntimeDataSrv.SetDynamicObjectVariable<bool>(row.DynamicObjectUuid, CaseClueIsRemovedVariable, val);
        }


        string m_FlipVariable;

        public string FlipVariable
        {
            get
            {
                if (m_FlipVariable == null)
                {
                    m_FlipVariable = GameMainConfigSrv.GetGameMainConfig().IsFlipVariable;
                }

                return m_FlipVariable;
            }
        }

        string m_IsCreatedVariable;

        public string CreatedVariable
        {
            get
            {
                if (m_IsCreatedVariable == null)
                {
                    m_IsCreatedVariable = GameMainConfigSrv.GetGameMainConfig().CaseClueHasVariable;
                }

                return m_IsCreatedVariable;
            }
        }


        string m_IsRemovedVariable;

        public string IsRemovedVariable
        {
            get
            {
                if (m_IsRemovedVariable == null)
                {
                    m_IsRemovedVariable = GameMainConfigSrv.GetGameMainConfig().CaseClueIsRemovedVariable;
                }

                return m_IsRemovedVariable;
            }
        }

        Vector3 NewPosition
        {
            get
            {
                return GameMainConfigSrv?.GetGameMainConfig()?.NewPostion ?? Vector3.zero;
            }
        }

        public bool IsFilp
        {
            get
            {
                return RuntimeDataSrv.GetVariable<bool>(FlipVariable); ;
            }
        }

        CaseContent CurrentContent
        {
            get
            {
                if (!LastShowCase.IsNullOrWhiteSpace())
                {
                    if (CaseDict.TryGetValue(LastShowCase, out CaseContent content))
                    {
                        return content;
                    }

                }
                return null;
            }

        }


        public void ShowClueInfo(string uuid)
        {
            var content = CurrentContent;
            if (content == null) return;

            if (content.DynamicObject2Clue.TryGetValue(uuid, out string clueUuid))
            {
                if (content.ClueDict.TryGetValue(clueUuid, out IClueRow clueRow))
                {
                    if (IsFilp)
                    {
                        Panel.ShowTitle(clueRow.ClueBackTitle);
                        Panel.ShowDesc(clueRow.ClueBackDesc);
                    }
                    else
                    {

                        Panel.ShowTitle(clueRow.ClueTitle);
                        Panel.ShowDesc(clueRow.Desc);
                    }
                }
            }
        }

        public void HideClueInfo()
        {
            Panel.HideTitle();
            Panel.HideDesc();
        }

        public void Combine(string uuid, string uuid2)
        {
            var content = CurrentContent;
            if (content == null) return;

            string clueUuid = string.Empty;
            content.DynamicObject2Clue.TryGetValue(uuid, out clueUuid);

            string clueUuid2 = string.Empty;
            content.DynamicObject2Clue.TryGetValue(uuid2, out clueUuid2);

            bool CanCombine = false;
            ClueIntegrate combine = null;

            foreach (var integrateInfo in content.IntegrateInfos)
            {
                if (IsFilp != integrateInfo.FlipCombine) continue;

                if (integrateInfo.Targets.Contains(clueUuid) && integrateInfo.Targets.Contains(clueUuid2))
                {
                    combine = integrateInfo;
                }

            }

            if (combine != null)
            {
                foreach (var result in combine.Results)
                {
                    switch (result.ResultType)
                    {
                        case ClueIntegrateResultType.NewClue:
                            var clueRow = content.ClueDict[result.ClueUuid];
                            var entityDynamicObject = content.CluesEntity[clueRow.DynamicObjectUuid];
                            var dynamicObject = entityDynamicObject.DynamicObj;
                            dynamicObject.SetVariable<bool>(CreatedVariable, true);
                            dynamicObject.ModelActive = true;
                            dynamicObject.CachedTransfrom.position = NewPosition;
                            break;
                        case ClueIntegrateResultType.RemoveClue:
                            break;
                    }
                }

                Debug.Log($"检测完成可以合并");
            }
            else
            {
                Debug.Log($"不能合并:{uuid},{uuid2},{clueUuid},{clueUuid2}");
            }





        }
    }

}