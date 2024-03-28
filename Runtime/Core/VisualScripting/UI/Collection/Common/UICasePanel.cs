using System;
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
using Pangoo.Core.Services;


namespace Pangoo.Core.VisualScripting
{
    [Category("通用/案件")]

    public class UICasePanel : UIPanel
    {


        public UICaseParams ParamsRaw = new UICaseParams();

        protected override IParams Params => ParamsRaw;

        public TextMeshProUGUI m_Text;

        public TextMeshProUGUI m_NameText;

        public TextMeshProUGUI m_DescText;

        public MainService Main
        {
            get
            {
                return PanelData.Main;
            }
        }



        Camera MainCamera { get; set; }


        public Vector3 TargetPoint
        {
            get
            {
                return MainCamera.transform.TransformPoint(Vector3.forward * 0.3f);
            }
        }

        public Vector3 ClueTargetPoint1
        {
            get
            {
                return MainCamera.transform.TransformPoint(Vector3.forward * 0.2f - Vector3.left * 0.1f);
            }
        }

        public Vector3 ClueTargetPoint2
        {
            get
            {
                return MainCamera.transform.TransformPoint(Vector3.forward * 0.2f + Vector3.left * 0.1f);
            }
        }

        public CaseContent ShowContent;


        public bool IsShowingCase;


        public RectTransform Clues1;

        public RectTransform Clues2;

        public float Distance = 0.2f;


        public RectTransform TargetRect;
        public RectTransform CluesListRect;

        Dictionary<string, UICaseClueSlot> CluesDict = new Dictionary<string, UICaseClueSlot>();

        Canvas CurrentCanvas;
        RectTransform CanvasRectTransform;

        public GameObject CluePrefab;

        public float SelectDistanceDelta = 0.01f;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);


            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_NameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            m_DescText = transform.Find("Desc").GetComponent<TextMeshProUGUI>();


            MainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();
            CurrentCanvas = this.GetComponentInParent<Canvas>();
            CanvasRectTransform = CurrentCanvas?.GetComponent<RectTransform>();


            m_Text?.gameObject.SetActive(false);
            m_NameText?.gameObject.SetActive(false);
            m_DescText?.gameObject.SetActive(false);



        }

        public GameObject CreateClueItem()
        {
            if (CluePrefab != null)
            {
                return UnityEngine.Object.Instantiate(CluePrefab);
            }
            var Prefab = Resources.Load<GameObject>("UI/Case/ClueItem");
            return UnityEngine.Object.Instantiate(Prefab);
        }


        public float GetClueWdithByClueCount(int count)
        {
            if (count <= 5)
            {
                return 200f;
            }


            return 1000 / count;
        }

        public void UpdateCaseDynamicObject()
        {
            ShowContent.Position = TargetPoint;
            Vector3 direction = ShowContent.Position - MainCamera.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            ShowContent.Rotation = targetRotation;
            if (ShowContent.AllCluesLoaded)
            {
                var uiSize = new Vector2(GetClueWdithByClueCount(ShowContent.CluesRows.Length), 300);

                foreach (var cluesRow in ShowContent.CluesRows)
                {
                    UICaseClueSlot uICaseClueSlot;
                    EntityDynamicObject Entity = ShowContent.CluesEntity.GetValueOrDefault(cluesRow.DynamicObjectUuid);

                    if (!CluesDict.TryGetValue(cluesRow.Uuid, out uICaseClueSlot))
                    {
                        uICaseClueSlot = new UICaseClueSlot();
                        uICaseClueSlot.ClueEntity = Entity;
                        uICaseClueSlot.Panel = this;
                        uICaseClueSlot.ClueRow = cluesRow;

                        CluesDict.Add(cluesRow.Uuid, uICaseClueSlot);
                    }

                    if (uICaseClueSlot.ClueItemGo == null || uICaseClueSlot.ClueItem == null)
                    {
                        var clueItem = CreateClueItem();
                        uICaseClueSlot.ClueItemGo = clueItem;
                        uICaseClueSlot.ClueItem = clueItem.GetComponent<UIClueItem>();
                        if (uICaseClueSlot.ClueItem != null)
                        {
                            uICaseClueSlot.ClueItem.Canvas = CurrentCanvas;
                            uICaseClueSlot.ClueItem.ClueListRect = CluesListRect;
                            uICaseClueSlot.ClueItem.TargetRect = TargetRect;
                            uICaseClueSlot.ClueItem.Text.text = cluesRow.Name;
                            uICaseClueSlot.ClueItem.Text.gameObject.SetActive(false);
                            uICaseClueSlot.ClueItem.rectTransform = uICaseClueSlot.ClueItem.GetComponent<RectTransform>();
                            uICaseClueSlot.ClueItem.Canvas = CurrentCanvas;
                            uICaseClueSlot.ClueItem.CanvasRect = uICaseClueSlot.ClueItem.Canvas?.GetComponent<RectTransform>();
                        }
                    }


                    uICaseClueSlot.ClueEntity.DynamicObj.ModelActive = true;

                    if (!uICaseClueSlot.Has || (uICaseClueSlot.Has && uICaseClueSlot.IsRemoved))
                    {
                        uICaseClueSlot.ClueEntity.DynamicObj.ModelActive = false;
                        uICaseClueSlot.ClueItem.transform.SetParent(CanvasRectTransform);
                    }
                    else
                    {
                        if (!uICaseClueSlot.ClueItem.IsInCluesList && !uICaseClueSlot.ClueItem.IsInTargetList)
                        {
                            uICaseClueSlot.ClueItem.transform.SetParent(CluesListRect);
                        }

                        var screenPoint = uICaseClueSlot.ClueItem.transform.position;
                        var applyDistance = Distance;
                        if (!uICaseClueSlot.ClueItem.IsPointEnter)
                        {
                            screenPoint = new Vector3(screenPoint.x, screenPoint.y - 50, screenPoint.z);
                            applyDistance = applyDistance + SelectDistanceDelta;
                        }

                        uICaseClueSlot.ClueItem.rectTransform.sizeDelta = uiSize;
                        var worldPoint = MainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, applyDistance));
                        uICaseClueSlot.ClueEntity.CachedTransform.position = worldPoint;
                        uICaseClueSlot.ClueEntity.CachedTransform.Rotate2TransformPlane(MainCamera.transform);
                    }



                }



            }


        }


        public void ShowCase(CaseContent content)
        {
            if (content == null || (content != null && content.Entity == null)) return;

            ShowContent = content;
            UpdateCaseDynamicObject();
            PanelData.Main.CharacterService.SetPlayerControllable(false);
            PanelData.Main.CharacterService.PlayerEnabledHotspot = false;
            IsShowingCase = true;
        }




        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (MainCamera == null)
            {
                Debug.Log($"Main Camera Is Null");
                return;
            }

            if (!IsShowingCase) return;

            UpdateCaseDynamicObject();




            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsShowingCase = false;
                PanelData.Main.CharacterService.SetPlayerControllable(true);
                PanelData.Main.CharacterService.PlayerEnabledHotspot = true;
                ShowContent.CaseModelActive = false;

                if (ShowContent.AllCluesLoaded)
                {
                    if (ShowContent.CluesEntity.Count == 2)
                    {
                        ShowContent.CluesEntity.Values.ToList()[0].DynamicObj.ModelActive = false;
                        ShowContent.CluesEntity.Values.ToList()[1].DynamicObj.ModelActive = false;


                    }
                }
            }

        }


    }
}