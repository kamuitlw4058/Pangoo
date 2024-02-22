using System;
using UnityEngine;
using Pangoo.Core.Common;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Pangoo;
using System.Collections.Generic;

namespace Pangoo.Core.VisualScripting
{
    // [Title("Show Text")]
    // [Image(typeof(IconString), ColorTheme.Type.Blue)]

    [Category("动态物体鼠标交互")]


    [Serializable]
    public class SpotDynamicObjectMouseInteract : HotSpot
    {

        [HideReferenceObjectPicker]
        [SerializeField]
        HotSpotTipParams m_Params = new HotSpotTipParams();


        [ShowInInspector]
        [HideInEditorMode]
        private GameObject m_HotspotStateGo;



        HotSpotSpriteState HotSpotSpriteState;


        [ShowInInspector]
        [HideInEditorMode]
        public DynamicObjectHotsoptState TargetSpotState
        {
            get
            {
                if (Hide)
                {
                    return DynamicObjectHotsoptState.None;
                }

                if (dynamicObject == null)
                {
                    return DynamicObjectHotsoptState.None;
                }


                return DynamicObjectHotsoptState.ShowUI;
            }
        }

        [HideInEditorMode]
        public DynamicObjectHotsoptState LastestSpotState = DynamicObjectHotsoptState.None;

        [HideInEditorMode]
        public DynamicObjectHotsoptState CurrentSpotState = DynamicObjectHotsoptState.None;


        public void UpdateState()
        {
            HotSpotSpriteState.RunningHotspoState = (int)TargetSpotState;

        }



        protected override void DoUpdate()
        {
            base.DoUpdate();

            GameObject instance = this.RequireInstance();
            if (instance == null) return;

            Vector3 offset = this.m_Params.Space switch
            {
                Space.World => this.m_Params.Offset,
                Space.Self => dynamicObject.CachedTransfrom.TransformDirection(this.m_Params.Offset),
                _ => throw new ArgumentOutOfRangeException()
            };

            instance.transform.SetPositionAndRotation(
                TargetTransform.position + TargetTransform.TransformDirection(this.m_Params.Offset),
                ShortcutMainCamera.Transform.rotation
            );

            bool isActive = this.EnableInstance();
            instance.SetActive(isActive);
            UpdateState();


        }


        protected virtual bool EnableInstance()
        {
            // return true;
            return dynamicObject.IsHotspotActive;
        }


        protected GameObject RequireInstance()
        {
            if (this.m_HotspotStateGo == null)
            {

                this.m_HotspotStateGo = new GameObject("MouseInteractState");

                this.m_HotspotStateGo.transform.SetPositionAndRotation(
                    TargetTransform.position + TargetTransform.TransformDirection(this.m_Params.Offset),
                    ShortcutMainCamera.Transform.rotation
                );
                this.m_HotspotStateGo.transform.SetParent(TargetTransform);
                var StatePrefab = Resources.Load<GameObject>("HotspotSpriteState");
                var StateGo = UnityEngine.Object.Instantiate(StatePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                StateGo.transform.SetParent(this.m_HotspotStateGo.transform);
                StateGo.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                HotSpotSpriteState = StateGo.GetComponent<HotSpotSpriteState>();

            }

            return this.m_HotspotStateGo;
        }


        public override void LoadParamsFromJson(string val)
        {
            m_Params.Load(val);
        }
        public override string ParamsToJson()
        {
            return m_Params.Save();
        }

    }



}