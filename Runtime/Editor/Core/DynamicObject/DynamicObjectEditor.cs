
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Characters;
using System;
using UnityEngine.UI;
using Pangoo.Core.Common;

namespace Pangoo
{

    //[ExecuteInEditMode]
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class DynamicObjectEditor : MonoBehaviour
    {
        private const float CANVAS_WIDTH = 600f;
        private const float CANVAS_HEIGHT = 300f;

        private const float SIZE_X = 2f;
        private const float SIZE_Y = 1f;

        [ReadOnly]
        public int m_DynamicObjectId;

        [ReadOnly]
        [ShowInInspector]
        [ValueDropdown("DynamicObjectIdValueDropdown")]
        [PropertyOrder(0)]
        public int DynamicObjectId
        {
            get
            {
                return m_DynamicObjectId;
            }
            set
            {
                m_DynamicObjectId = value;
                OnValueChanged();
            }
        }

        public IEnumerable DynamicObjectIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
        }

        [ReadOnly]
        DynamicObjectTableOverview Overview;



        [ReadOnly]
        [ShowInInspector]
        DynamicObjectTable.DynamicObjectRow Row;


        [HideLabel]
        [SerializeField]
        [PropertyOrder(10)]
        public DynamicObjectDetailWrapper Wrapper;

        private void OnEnable()
        {
            OnValueChanged();
        }

        private void OnDisable()
        {
            ClearObjects(DynamicObjects);
        }



        void Start()
        {
            DoService = new DynamicObject(gameObject);
            DoService.Row = Row;
            DoService.Awake();
            DoService.Start();
        }

        // public Func<TriggerEventParams, bool> CheckInteract;

        // public Action<TriggerEventParams> InteractEvent;

        [ShowInInspector]
        [field: NonSerialized]
        [LabelText("动态物体")]
        [HideReferenceObjectPicker]
        public DynamicObject DoService { get; private set; }


        [ReadOnly]
        [ListDrawerSettings(Expanded = true)]
        public List<GameObject> DynamicObjects = new List<GameObject>();

        public void ClearObjects(List<GameObject> gameObjects)
        {
            if (gameObjects != null)
            {

                foreach (var scene in gameObjects)
                {
                    try
                    {
                        DestroyImmediate(scene);
                    }
                    catch
                    {
                    }
                }
                gameObjects.Clear();
            }
        }

        public void UpdateObjects(List<SubDynamicObject> subDynamicObjects)
        {
            // ClearObjects(DynamicObjects);
            foreach (var subDo in subDynamicObjects)
            {
                var row = GameSupportEditorUtility.GetDynamicObjectRow(subDo.DynamicObjectId);
                if (row == null)
                {
                    Debug.LogError($"staticScene Id:{subDo.DynamicObjectId} is null");
                    continue;
                }

                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(row.AssetPathId);
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.name = row.Name;
                var subTarget = transform.Find(subDo.Path);
                if (subTarget != null)
                {
                    go.transform.SetParent(subTarget);
                    var helper = go.AddComponent<DynamicObjectEditor>();
                    helper.DynamicObjectId = subDo.DynamicObjectId;
                    DynamicObjects.Add(go);
                }

            }
        }
        GameObject m_Tooltip;

        private Image CreateHand(RectTransform parent)
        {
            return ConfigureImage(parent, "UI/UI_Hand");
        }

        private Image ConfigureImage(RectTransform parent, string resourcePath)
        {
            GameObject imageGo = new GameObject("UI");
            var image = imageGo.AddComponent<Image>();
            var mat = Resources.Load<Material>(resourcePath);
            image.material = mat;
            RectTransform imageTransform = imageGo.GetComponent<RectTransform>();
            PangooRectTransformUtility.SetAndCenterToParent(imageTransform, parent);
            imageTransform.sizeDelta = new Vector2(30, 30);

            return image;
        }
        [BoxGroup("可视化设置")]
        [ShowInInspector]
        [LabelText("设置交互偏移")]
        public Vector3 InteractOffset
        {
            get
            {
                return Row.InteractOffset;
            }
            set
            {
                Row.InteractOffset = value;
                if (DoService.m_Tracker != null)
                {
                    DoService.m_Tracker.InteractOffset = value;
                }
            }
        }



        public void OnValueChanged()
        {
            if (m_DynamicObjectId == 0) return;

            Overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<DynamicObjectTableOverview>(m_DynamicObjectId);
            Row = GameSupportEditorUtility.GetDynamicObjectRow(m_DynamicObjectId);


            Wrapper = new DynamicObjectDetailWrapper();
            Wrapper.Overview = Overview;
            Wrapper.Row = Row;
            ClearObjects(DynamicObjects);
            UpdateObjects(Wrapper.SubDynamicObjects);


            transform.localPosition = Row.Position;
            transform.localRotation = Quaternion.Euler(Row.Rotation);

        }


        private void Update()
        {
            // DoService?.Update();




        }

        // protected override void DoUpdate()
        // {
        //     base.DoUpdate();

        //     GameObject instance = this.RequireInstance();
        //     if (instance == null) return;

        //     Vector3 offset = this.m_Params.Space switch
        //     {
        //         Space.World => this.m_Params.Offset,
        //         Space.Self => dynamicObject.CachedTransfrom.TransformDirection(this.m_Params.Offset),
        //         _ => throw new ArgumentOutOfRangeException()
        //     };

        //     instance.transform.SetPositionAndRotation(
        //         dynamicObject.HotspotInteractPosition + offset,
        //         ShortcutMainCamera.Transform.rotation
        //     );

        //     bool isActive = this.EnableInstance();
        //     instance.SetActive(isActive);

        //     if (CurrentSpotState != SpotState)
        //     {
        //         Transition = 0;
        //         m_Velocity = 0;
        //         if (IsTransitioning)
        //         {
        //             UpdateState(1, true);
        //             LastestSpotState = CurrentSpotState;
        //         }
        //         CurrentSpotState = SpotState;
        //         IsTransitioning = true;

        //     }
        //     else
        //     {
        //         if (LastestSpotState != CurrentSpotState)
        //         {
        //             if (IsTransitioning)
        //             {
        //                 this.Transition = Mathf.SmoothDamp(
        //                     this.Transition,
        //                    1,
        //                     ref this.m_Velocity,
        //                     TRANSITION_SMOOTH_TIME
        //                 );

        //                 UpdateState(Transition);

        //                 if (Transition == 1)
        //                 {
        //                     IsTransitioning = false;
        //                     LastestSpotState = CurrentSpotState;
        //                 }
        //             }
        //         }

        //     }

        // }


        [Button("SetTransfrom")]
        public void SetTransfrom()
        {
            Wrapper.Row.Position = transform.localPosition;
            Wrapper.Row.Rotation = transform.localRotation.eulerAngles;
            Wrapper.Row.Scale = transform.localScale;
            Wrapper.Save();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"DynamicObjectEditor OnTriggerEnter");
            DoService?.TriggerEnter3d(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"DynamicObjectEditor OnTriggerExit");
            DoService?.TriggerExit3d(other);
        }

    }


}
#endif