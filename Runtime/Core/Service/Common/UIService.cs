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

        public override string ServiceName => "UI";

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
        Dictionary<string, UIFormLogic> m_LoadedAssetDict = new Dictionary<string, UIFormLogic>();
        List<string> m_LoadingAssetIds = new List<string>();

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

        public void ShowUI(string uuid, Action closeAction = null, object userData = null)
        {
            if (Loader == null)
            {
                Loader = UILoader.Create(this);
            }
            Log($"Show UI:{uuid} m_UIInfo:{m_UIInfo}");
            var info = m_UIInfo.GetRowByUuid<UIInfoRow>(uuid);
            Log($"Show UI:{uuid} info:{info}");
            if (info == null || m_LoadingAssetIds.Contains(uuid) || m_LoadedAssetDict.ContainsKey(uuid))
            {
                return;
            }
            else
            {
                m_LoadingAssetIds.Add(uuid);

                var data = info.GetPanelData(userData);
                data.UI = this;
                data.Main = Parent as MainService;
                int serialId = 0;

                serialId = Loader.ShowUI(data,
                   (o) =>
                   {
                       if (m_LoadingAssetIds.Contains(uuid))
                       {
                           m_LoadingAssetIds.Remove(uuid);
                       }
                       m_LoadedAssetDict.Add(uuid, o);

                   },
                   () =>
                   {
                       if (m_LoadedAssetDict.ContainsKey(uuid))
                       {
                           m_LoadedAssetDict.Remove(uuid);
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