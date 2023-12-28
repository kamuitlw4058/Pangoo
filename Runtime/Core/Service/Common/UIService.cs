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
    public class UIService : MainSubService
    {
        public override int Priority => 6;

        public override string ServiceName => "UI";


        UILoader Loader = null;


        UIInfo m_UIInfo;

        [ShowInInspector]
        Dictionary<string, UIFormLogic> m_LoadedAssetDict = new Dictionary<string, UIFormLogic>();
        List<string> m_LoadingAssetIds = new List<string>();

        protected override void DoAwake()
        {
            base.DoAwake();
        }


        protected override void DoStart()
        {

            m_UIInfo = GameInfoSrv.GetGameInfo<UIInfo>();
        }
        public void ShowPreview(PreviewData previewData, Action closeAction = null)
        {
            if (!GameMainConfigSrv.GetGameMainConfig().PreviewPanelUuid.IsNullOrWhiteSpace())
            {
                previewData.UIService = this;
                ShowUI(GameMainConfigSrv.GetGameMainConfig().PreviewPanelUuid, closeAction, previewData);
            }
        }

        public void ShowUI(string uuid, Action closeAction = null, object userData = null)
        {
            if (Loader == null)
            {
                Loader = UILoader.Create(this);
            }
            Log($"Show UI:{uuid} m_UIInfo:{GameInfoSrv}");
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