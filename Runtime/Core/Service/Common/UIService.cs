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

namespace Pangoo.Core.Services
{
    public class UIService : BaseService
    {
        public override int Priority => 6;

        ExcelTableService m_ExcelTableService;


        public ExcelTableService TableService
        {
            get
            {
                return m_ExcelTableService;
            }
        }

        SimpleUITable m_UITable;


        EntityGroupTable m_EntityGroupTable;

        EntityGroupTable.EntityGroupRow m_EntityGroupRow;


        DynamicObjectTable m_DynamicObjectTable;


        UILoader Loader = null;

        GameInfoService m_GameInfoService;


        UIInfo m_UIInfo;

        [ShowInInspector]
        Dictionary<int, UIFormLogic> m_LoadedAssetDict = new Dictionary<int, UIFormLogic>();

        [ShowInInspector]
        List<int> m_LoadingAssetIds = new List<int>();

        protected override void DoAwake()
        {
            base.DoAwake();
            m_ExcelTableService = Parent.GetService<ExcelTableService>();
            m_GameInfoService = Parent.GetService<GameInfoService>();
        }


        protected override void DoStart()
        {

            m_DynamicObjectTable = m_ExcelTableService.GetExcelTable<DynamicObjectTable>();
            m_EntityGroupTable = m_ExcelTableService.GetExcelTable<EntityGroupTable>();
            m_UITable = m_ExcelTableService.GetExcelTable<SimpleUITable>();

            m_UIInfo = m_GameInfoService.GetGameInfo<UIInfo>();
        }

        public void ShowUI(int uiId, Action closeAction = null, object userData = null)
        {
            if (Loader == null)
            {
                Loader = UILoader.Create(this);
            }

            var info = m_UIInfo.GetRowById<UIInfoRow>(uiId);
            if (m_LoadingAssetIds.Contains(uiId) || m_LoadedAssetDict.ContainsKey(uiId))
            {
                return;
            }
            else
            {
                Debug.Log($"Show UI:{uiId}");
                m_LoadingAssetIds.Add(uiId);

                var data = info.GetPanelData(userData);
                data.UI = this;
                data.Main = Parent as MainService;
                int serialId = 0;

                serialId = Loader.ShowUI(data,
                   (o) =>
                   {
                       if (m_LoadingAssetIds.Contains(uiId))
                       {
                           m_LoadingAssetIds.Remove(uiId);
                       }
                       m_LoadedAssetDict.Add(uiId, o);

                   },
                   () =>
                   {
                       if (m_LoadedAssetDict.ContainsKey(uiId))
                       {
                           m_LoadedAssetDict.Remove(uiId);
                       }

                       if (closeAction != null)
                       {
                           closeAction.Invoke();
                       }
                   });
            }
        }

    }
}