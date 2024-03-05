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
    public class MainMenuService : MainSubService
    {
        public override int Priority => 11;
        public override string ServiceName => "MainMenu";

        UIMainPanel MainPanel;

        protected override void DoStart()
        {
            base.DoAwake();
            MainMenuData mainMenuData = new MainMenuData();
            mainMenuData.MainMenuSrv = this;
            UISrv.ShowMainMenu(mainMenuData, OnShowMainMenu);
        }


        public void OnShowMainMenu(UIFormLogic logic)
        {
            MainPanel = logic as UIMainPanel;
            if (MainPanel != null)
            {
                MainPanel.RectTransform.offsetMin = Vector2.zero;
                MainPanel.RectTransform.offsetMax = Vector2.zero;
            }
        }



    }
}