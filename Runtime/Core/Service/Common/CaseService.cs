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

namespace Pangoo.Core.Services
{
    public class CaseService : MainSubService
    {
        public override int Priority => 7;

        public Dictionary<string, CaseContent> CaseDict = new Dictionary<string, CaseContent>();

        public UICasePanel Panel;
        protected override void DoStart()
        {
            var panelUuid = GameMainConfigSrv.GameMainConfig?.CasePanelUuid;

            if (!panelUuid.IsNullOrWhiteSpace())
            {
                UISrv.ShowUI(panelUuid, showAction: (o) =>
                {
                    Panel = o as UICasePanel;
                });
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
                ret.CluesRows[i] = MetaTableSrv.GetClueRowByUuid(clueUuids[i]);
            }
            return ret;
        }



        public void ShowCase(string uuid, Action<CaseContent> showFinishedCallback = null)
        {
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
                DynamicObjectSrv.ShowModuleDynamicObject(ConstString.CaseModule, dynamicObjectUuid, (o) =>
                {
                    content.Entity = o;
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
                        DynamicObjectSrv.ShowSubDynamicObject(clueRow.DynamicObjectUuid, null, kv.Value.Entity, false, (o) =>
                        {
                            o.DynamicObj.ModelActive = false;
                            o.transform.localPosition = Vector3.zero;
                            kv.Value.CluesEntity.Add(clueRow.DynamicObjectUuid, o);
                        });
                    }

                }
            }
        }

        public string GetClueKey(string ClueKey, string valKey)
        {
            return String.Join(".", "Clue", ClueKey, valKey);
        }


        public bool GetClueHas(string uuid)
        {
            var row = MetaTableSrv.GetClueRowByUuid(uuid);
            var hasKey = GetClueKey(row.ClueKey, "Has");
            return RuntimeDataSrv.Get<bool>(hasKey, false);
        }

        public bool GetClueIsRemoved(string uuid)
        {
            var row = MetaTableSrv.GetClueRowByUuid(uuid);
            var hasKey = GetClueKey(row.ClueKey, "IsRemoved");
            return RuntimeDataSrv.Get<bool>(hasKey, false);
        }
    }

}