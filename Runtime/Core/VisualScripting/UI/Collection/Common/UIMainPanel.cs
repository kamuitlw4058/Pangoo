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
using UnityEngine.UI;
using System;
using System.IO;


namespace Pangoo.Core.VisualScripting
{
    [Category("通用/主菜单")]

    public class UIMainPanel : UIPanel
    {
        public UIMainParams ParamsRaw = new UIMainParams();

        protected override IParams Params => ParamsRaw;

        public GameObject MainObejct;

        public GameObject MenuObject;

        public GameObject SaveObject;

        public GameObject SaveListObject;

        RectTransform m_RectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }
                return m_RectTransform;
            }
        }

        public Button StartButton;

        public Button LoadButton;

        public Button AboutButton;
        public MainMenuData Context;

        public ScrollRect ScrollRect;

        public SaveItem BuildSaveItem(GameObject parent, Tuple<DateTime, FileInfo> tuple)
        {
            var SaveItemPrefab = Resources.Load<GameObject>("UI/MainMenu/SaveItem");
            var SaveItemGo = UnityEngine.Object.Instantiate(SaveItemPrefab);
            SaveItem saveItem = SaveItemGo.GetComponent<SaveItem>();
            if (saveItem != null)
            {
                saveItem.TupleData = tuple;
                saveItem.Click += OnLoad;
                saveItem.TitleText.text = tuple.Item1.ToString();
            }
            SaveItemGo.transform.SetParent(parent.transform);
            return saveItem;
        }

        public void OnLoad(SaveItem item)
        {
            Debug.Log($"On Load:{item?.TupleData?.Item1}");
            MainObejct?.SetActive(false);
            MenuObject?.SetActive(true);
            SaveObject?.SetActive(false);
            Context.MainMenuSrv.SaveLoadSrv.Load(item.TupleData);
            Context.MainMenuSrv.GameSectionSrv.SetGameSection(firstGameSection: true);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            Context = PanelData.UserData as MainMenuData;
            if (Context == null) return;



            StartButton?.onClick.AddListener(() =>
            {
                Debug.Log("Click Start");
                MainObejct?.SetActive(false);
                Context.MainMenuSrv.SaveLoadSrv.Load();
                Context.MainMenuSrv.GameSectionSrv.SetGameSection(firstGameSection: true);
            });
            LoadButton?.onClick.AddListener(() =>
            {
                Debug.Log("Click Load");
                MenuObject?.SetActive(false);
                SaveObject?.SetActive(true);
                if (SaveListObject != null)
                {
                    SaveListObject.transform.DestroyChildern();
                    var saveList = Context.MainMenuSrv.SaveLoadSrv.GetSaveFiles();
                    Debug.Log($"SaveList:{saveList.Count}");
                    var rectTransform = SaveListObject.GetComponent<RectTransform>();
                    rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, saveList.Count * 200);
                    foreach (var saveItem in saveList)
                    {
                        BuildSaveItem(SaveListObject, saveItem);
                    }
                    if (ScrollRect != null)
                    {
                        ScrollRect.verticalNormalizedPosition = 1;
                    }
                }


            });

        }




    }
}