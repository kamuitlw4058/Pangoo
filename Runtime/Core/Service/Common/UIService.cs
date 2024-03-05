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
        Dictionary<string, UIFormLogic> m_LoadedAsset = new();
        List<string> m_LoadingAssetIds = new List<string>();

        protected override void DoAwake()
        {
            base.DoAwake();
        }

        // public UIFormLogic GetUILogic(string uuid)
        // {
        // }


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

        public void ShowDialogue(DialogueData dialogueData, Action closeAction = null)
        {
            if (!GameMainConfigSrv.GetGameMainConfig().DialoguePanelUuid.IsNullOrWhiteSpace())
            {
                dialogueData.UIService = this;
                ShowUI(GameMainConfigSrv.GetGameMainConfig().DialoguePanelUuid, closeAction, dialogueData);
            }
        }

        public void ShowMainMenu(MainMenuData mainMenuData, Action<UIFormLogic> showAction = null)
        {
            if (!GameMainConfigSrv.GetGameMainConfig().DefaultMainMenuPanelUuid.IsNullOrWhiteSpace())
            {
                mainMenuData.UISrv = this;
                ShowUI(GameMainConfigSrv.GetGameMainConfig().DefaultMainMenuPanelUuid, userData: mainMenuData, showAction: showAction);
            }
        }

        public void ShowUI(string uuid, Action closeAction = null, object userData = null, Action<UIFormLogic> showAction = null)
        {
            if (Loader == null)
            {
                Loader = UILoader.Create(this);
            }
            var info = m_UIInfo.GetRowByUuid<UIInfoRow>(uuid);


            var data = info.GetPanelData(userData);
            data.UI = this;
            data.Main = Parent as MainService;
            int serialId = 0;

            serialId = Loader.ShowUI(data,
               (o) =>
               {
                   m_LoadedAsset.Add(uuid, o);
                   showAction?.Invoke(o);
               },
               (o) =>
               {
                   if (m_LoadedAsset.ContainsKey(uuid))
                   {
                       m_LoadedAsset.Remove(uuid);
                   }

                   closeAction?.Invoke();
               });
        }

    }
}