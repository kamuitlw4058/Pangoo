using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using LitJson;
using Pangoo.Common;


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

        [ShowInInspector]
        public CaseContent ShowContent;


        public bool IsShowingCase;


        public RectTransform Clues1;

        public RectTransform Clues2;

        public float Distance = 0.2f;


        public RectTransform TargetRect;
        public RectTransform CluesListRect;

        [ShowInInspector]
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

        List<UICaseClueSlot> ShowSlots = new List<UICaseClueSlot>();
        List<UICaseClueSlot> TargetSlots = new List<UICaseClueSlot>();

        [ShowInInspector]
        Dictionary<string, bool> Dict = new Dictionary<string, bool>();



        public void UpdateCaseDynamicObject()
        {
            ShowContent.Position = TargetPoint;
            Vector3 direction = ShowContent.Position - MainCamera.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            ShowContent.Rotation = targetRotation;
            if (ShowContent.AllCluesLoaded)
            {


                ShowSlots.Clear();
                TargetSlots.Clear();
                Dict.Clear();

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

;

                    if (!uICaseClueSlot.Has || (uICaseClueSlot.Has && uICaseClueSlot.IsRemoved))
                    {
                        uICaseClueSlot.ClueEntity.DynamicObj.ModelActive = false;
                        uICaseClueSlot.ClueItem.BackImage.enabled = false;
                        uICaseClueSlot.ClueItem.transform.SetParent(CanvasRectTransform);
                    }
                    else
                    {
                        if (uICaseClueSlot.ClueItem.IsInTargetList)
                        {
                            TargetSlots.Add(uICaseClueSlot);
                        }
                        ShowSlots.Add(uICaseClueSlot);

                    }

                }

                var uiSize = new Vector2(GetClueWdithByClueCount(ShowSlots.Count), 300);
                foreach (var slot in ShowSlots)
                {
                    if (!slot.ClueItem.IsDraging)
                    {
                        if (!slot.ClueItem.IsInCluesList && !slot.ClueItem.IsInTargetList)
                        {
                            slot.ClueItem.transform.SetParent(CluesListRect);
                        }
                    }

                    slot.ClueItem.BackImage.enabled = true;
                    slot.ClueEntity.DynamicObj.ModelActive = true;
                    var screenPoint = slot.ClueItem.transform.position;
                    var applyDistance = Distance;
                    if (!slot.ClueItem.IsPointEnter)
                    {
                        screenPoint = new Vector3(screenPoint.x, screenPoint.y - 50, screenPoint.z);
                        applyDistance = applyDistance + SelectDistanceDelta;
                    }

                    slot.ClueItem.rectTransform.sizeDelta = uiSize;
                    var worldPoint = MainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, applyDistance));
                    slot.ClueEntity.CachedTransform.position = worldPoint;
                    slot.ClueEntity.CachedTransform.Rotate2TransformPlane(MainCamera.transform);
                }

                if (TargetSlots.Count >= 2)
                {
                    ClueIntegrate[] clueIntegrates;
                    try
                    {
                        clueIntegrates = JsonMapper.ToObject<ClueIntegrate[]>(ShowContent.CaseRow.CluesIntegrate);
                    }
                    catch
                    {
                        clueIntegrates = null;
                    }

                    if (clueIntegrates != null)
                    {

                        foreach (var clueIntegrate in clueIntegrates)
                        {
                            bool intergrateMatch = true;
                            if (clueIntegrate.Targets.Length == TargetSlots.Count)
                            {

                                foreach (var target in clueIntegrate.Targets)
                                {
                                    bool clueMatch = false;
                                    foreach (var targetSlot in TargetSlots)
                                    {
                                        if (target.Equals(targetSlot.ClueRow.Uuid))
                                        {
                                            clueMatch = true;
                                        }
                                    }
                                    if (!clueMatch)
                                    {
                                        intergrateMatch = false;
                                        break;
                                    }

                                }
                            }
                            if (intergrateMatch)
                            {
                                foreach (var result in clueIntegrate.Results)
                                {
                                    switch (result.ResultType)
                                    {
                                        case ClueIntegrateResultType.NewClue:
                                            var ClueRow = PanelData.Main.MetaTable.GetClueRowByUuid(result.ClueUuid);
                                            PanelData.Main.RuntimeData.SetDynamicObjectVariable<bool>(ClueRow.DynamicObjectUuid, PanelData.Main.GameConfig.GameMainConfig.CaseClueHasVariable, true);
                                            break;
                                        case ClueIntegrateResultType.RemoveClue:
                                            var RemoveClueRow = PanelData.Main.MetaTable.GetClueRowByUuid(result.ClueUuid);
                                            PanelData.Main.RuntimeData.SetDynamicObjectVariable<bool>(RemoveClueRow.DynamicObjectUuid, PanelData.Main.GameConfig.GameMainConfig.CaseClueIsRemovedVariable, true);
                                            break;
                                        case ClueIntegrateResultType.CaseVariableBool:
                                            PanelData.Main.RuntimeData.SetDynamicObjectVariable<bool>(ShowContent.CaseRow.DynamicObjectUuid, result.VariableUuid, result.VariableValue);
                                            break;

                                    }
                                }

                            }
                        }

                        foreach (var slot in TargetSlots)
                        {
                            slot.ClueItem.IsInCluesList = true;
                        }

                    }

                }



                foreach (var caseVariable in ShowContent.CaseRow.CaseVariables.ToSplitArr<string>())
                {
                    Dict.Add(caseVariable, PanelData.Main.RuntimeData.GetDynamicObjectVariable<bool>(ShowContent.CaseRow.DynamicObjectUuid, caseVariable));
                }

                var ShowType = ShowContent.CaseRow.CaseShowType.ToEnum<CaseShowType>();
                switch (ShowType)
                {
                    case CaseShowType.State:
                        ShowCaseState();
                        break;
                    case CaseShowType.Variable:
                        ShowCaseVariableState();
                        break;
                }

            }


        }

        public void ShowCaseState()
        {
            List<CaseStateCheckItem> States;
            try
            {
                States = JsonMapper.ToObject<List<CaseStateCheckItem>>(ShowContent.CaseRow.CaseStates);
            }
            catch
            {
                States = null;
            }


            if (States != null)
            {
                bool TotalMatch = false;
                foreach (var state in States)
                {
                    bool stateMatch = true;
                    foreach (var kv in Dict)
                    {
                        bool foundTrue = false;
                        foreach (var trueVar in state.VariableUuids)
                        {
                            if (kv.Key.Equals(trueVar))
                            {
                                foundTrue = true;
                                if (!kv.Value)
                                {
                                    stateMatch = false;
                                }
                                break;
                            }
                        }


                        if (!foundTrue && kv.Value)
                        {
                            stateMatch = false;
                        }

                        if (!stateMatch)
                        {
                            break;
                        }
                    }

                    if (stateMatch)
                    {
                        TotalMatch = true;
                        ShowContent.Entity.DynamicObj.SetMaterialState(state.State);
                        break;
                    }


                }

                if (!TotalMatch)
                {
                    ShowContent.Entity.DynamicObj.SetMaterialState(0);

                }
            }
        }

        public void ShowCaseVariableState()
        {
            List<CaseVariableCheckItem> States;
            try
            {
                States = JsonMapper.ToObject<List<CaseVariableCheckItem>>(ShowContent.CaseRow.CaseVariableState);
            }
            catch
            {
                States = null;
            }


            if (States != null)
            {
                foreach (var state in States)
                {
                    if (state.ControlChildList == null) continue;

                    bool stateVal = Dict.GetValueOrDefault(state.VariableUuid, false);
                    foreach (var path in state.ControlChildList)
                    {
                        var trans = ShowContent.Entity.DynamicObj.GetTransform(path);
                        trans?.gameObject.SetActive(stateVal);
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
            PanelData.Main.CharacterService.PlayerIsInteractive = false;
            PanelData.Main.Cursor.CursorType = CursorTypeEnum.Show;
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
                PanelData.Main.CharacterService.PlayerIsInteractive = true;
                ShowContent.CaseModelActive = false;
                PanelData.Main.Cursor.CursorType = CursorTypeEnum.Hide;

                foreach (var cluesRow in ShowContent.CluesRows)
                {
                    if (CluesDict.TryGetValue(cluesRow.Uuid, out UICaseClueSlot uICaseClueSlot))
                    {
                        uICaseClueSlot.ClueEntity.DynamicObj.ModelActive = false;
                        uICaseClueSlot.ClueItem.BackImage.enabled = false;
                        uICaseClueSlot.ClueItem.transform.SetParent(CanvasRectTransform);
                    }
                }
            }

        }


    }
}