
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

using UnityEngine.UI;
using Pangoo.Core.Common;
using Pangoo.MetaTable;
using System.Linq;
using UnityEngine.UIElements;

namespace Pangoo
{

    //[ExecuteInEditMode]
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class DynamicObjectEditor : MonoBehaviour
    {

        Dictionary<string, string> m_Uuids = new();
        public Dictionary<string, string> Uuids
        {
            get
            {
                return m_Uuids;
            }
            set
            {

                Clear();
                if (value == null)
                {
                    m_Uuids.Clear();
                }
                else
                {
                    m_Uuids = value;
                }
            }
        }

        [ReadOnly]
        string m_DynamicObjectUuid;

        [ReadOnly]
        [ShowInInspector]
        [ValueDropdown("DynamicObjectUuidValueDropdown")]
        [PropertyOrder(0)]
        public string DynamicObjectUuid
        {
            get
            {
                return m_DynamicObjectUuid;
            }
            set
            {
                m_DynamicObjectUuid = value;
                OnValueChanged();
            }
        }

        public IEnumerable DynamicObjectUuidValueDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }



        [ReadOnly]
        [ShowInInspector]
        UnityDynamicObjectRow UnityRow;


        [SerializeField]
        [PropertyOrder(10)]
        [ShowIf("@this.UnityRow != null")]
        public DynamicObjectDetailRowWrapper Wrapper;

        private void OnEnable()
        {
            OnValueChanged();
        }

        private void OnDisable()
        {
            Clear();
        }



        void Start()
        {

        }




        [ReadOnly]
        public Dictionary<string, GameObject> DynamicObjects = new();

        public void Clear()
        {
            if (DynamicObjects != null)
            {

                foreach (var scene in DynamicObjects.Values)
                {
                    try
                    {
                        if (scene != null)
                        {
                            DestroyImmediate(scene);
                        }
                    }
                    catch
                    {
                    }
                }
                DynamicObjects.Clear();
            }
        }

        public void UpdateObjects()
        {
            DynamicObjects.SyncKey(Uuids.Keys.ToList(), (uuid) =>
            {
                var row = DynamicObjectOverview.GetUnityRowByUuid(uuid);
                if (row == null)
                {
                    Debug.LogError($"DynmaicObject Uuid:{uuid} is null");
                    return null;
                }

                var assetPathRow = AssetPathOverview.GetUnityRowByUuid(row.Row.AssetPathUuid);
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.name = row.Name;

                Transform subTarget;
                if (Uuids[uuid].Equals("Self"))
                {
                    subTarget = transform;
                }
                else
                {
                    subTarget = transform.Find(Uuids[uuid]);
                }

                if (subTarget != null)
                {
                    go.transform.SetParent(subTarget);
                    var helper = go.AddComponent<DynamicObjectEditor>();
                    helper.DynamicObjectUuid = uuid;
                }
                return go;
            });

        }


        [BoxGroup("可视化设置")]
        [ShowInInspector]
        [LabelText("设置交互偏移")]
        [ShowIf("@this.UnityRow != null")]

        public Vector3 InteractOffset
        {
            get
            {
                return UnityRow.Row.InteractOffset;
            }
            set
            {
                UnityRow.Row.InteractOffset = value;
                EditorUtility.SetDirty(UnityRow);
                AssetDatabase.SaveAssets();
            }
        }



        public void OnValueChanged()
        {
            if (m_DynamicObjectUuid.IsNullOrWhiteSpace()) return;

            var overview = DynamicObjectOverview.GetOverviewByUuid(m_DynamicObjectUuid);
            UnityRow = DynamicObjectOverview.GetUnityRowByUuid(m_DynamicObjectUuid);



            Wrapper = new DynamicObjectDetailRowWrapper();
            Wrapper.Overview = overview;
            Wrapper.UnityRow = UnityRow;

            Dictionary<string, string> uuidDict = new();
            foreach (var sdo in Wrapper.SubDynamicObjects)
            {
                if (sdo.DynamicObjectUuid.IsNullOrWhiteSpace()) continue;
                uuidDict.Add(sdo.DynamicObjectUuid, sdo.Path);
            }

            Uuids = uuidDict;


            if (Wrapper.PositionSpace == Space.Self)
            {
                transform.localPosition = Wrapper.Postion;
                transform.localRotation = Quaternion.Euler(Wrapper.Rotation);
            }
            else
            {
                transform.position = Wrapper.Postion;
                transform.rotation = Quaternion.Euler(Wrapper.Rotation);
            }


            if (Wrapper.Scale == Vector3.zero)
            {
                transform.localScale = Vector3.zero;
            }
            else
            {
                transform.localScale = Wrapper.Scale;
            }

        }


        private void Update()
        {
            if (!Application.isPlaying)
            {
                UpdateObjects();
            }
        }



        [Button("SetTransfrom")]
        [ShowIf("@this.UnityRow != null")]

        public void SetTransfrom()
        {
            Wrapper.UnityRow.Row.Position = transform.localPosition;
            Wrapper.UnityRow.Row.Rotation = transform.localRotation.eulerAngles;
            Wrapper.UnityRow.Row.Scale = transform.localScale;
            Wrapper.Save();
        }

        public Color GizmosColor = Color.red;
        public Color GizmosRadiusColor = new Color(0, 1, 0, 0.3f);
        public Color GizmosSelectRadiusColor = new Color(1, 0.5f, 0.8f, 0.8f);



        public Vector3 GizmosSize
        {
            get
            {
                return Vector3.one * 0.1f;
            }
        }

        private void OnDrawGizmos()
        {
            // var oldColor = Gizmos.color;
            // Gizmos.color = GizmosColor;
            // var InteractPosition = transform.position;

            // if (UnityRow.Row.InteractTarget.IsNullOrWhiteSpace() || UnityRow.Row.InteractTarget.Equals("Self"))
            // {
            //     InteractPosition = transform.position;
            // }
            // else
            // {
            //     var interactionTarget = transform.Find(UnityRow.Row.InteractTarget);
            //     if (interactionTarget != null)
            //     {
            //         InteractPosition = interactionTarget.position;
            //     }
            //     else
            //     {
            //         Debug.LogError($"Target is Null");
            //     }
            // }

            // Gizmos.DrawCube(InteractPosition + InteractOffset, GizmosSize);

            // var InteractRadius = UnityRow.Row.InteractRadius > 0 ? UnityRow.Row.InteractRadius : GameSupportEditorUtility.GetGameMainConfig().DefaultInteractRadius;
            // if (Selection.activeGameObject == gameObject)
            // {
            //     Gizmos.color = GizmosSelectRadiusColor;
            //     Gizmos.DrawSphere(InteractPosition + InteractOffset, InteractRadius);
            // }
            // else
            // {
            //     Gizmos.color = GizmosRadiusColor;
            //     Gizmos.DrawSphere(InteractPosition + InteractOffset, InteractRadius);
            // }



            // Gizmos.color = oldColor;
        }



    }


}
#endif