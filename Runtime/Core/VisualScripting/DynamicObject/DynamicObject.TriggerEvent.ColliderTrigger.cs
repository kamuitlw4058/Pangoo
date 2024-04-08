using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        int m_EnterTriggerCount;

        [ShowInInspector]
        [FoldoutGroup("碰撞触发")]
        [LabelText("玩家进入触发次数")]
        public int EnterTriggerCount
        {
            get
            {
                return m_EnterTriggerCount;
            }
            set
            {
                m_EnterTriggerCount = value;
                m_EnterTriggerCount = Mathf.Max(0, m_EnterTriggerCount);

                if (m_Tracker != null)
                {
                    if (m_EnterTriggerCount > 0)
                    {
                        m_Tracker.InteractTriggerEnter = true;
                    }
                    else
                    {
                        m_Tracker.InteractTriggerEnter = false;
                    }

                }
            }
        }

        List<DynamicObjectSubObjectTrigger> m_SubObjectTriggerInfo;


        [LabelText("基础碰撞")]
        [FoldoutGroup("碰撞触发")]
        public Collider[] BaseColliders;


        void DoAwakeColliderTrigger()
        {
            BaseColliders = CachedTransfrom.GetComponents<Collider>();

            if (Row.SubObjectTriggerList.IsNullOrWhiteSpace()) return;

            try
            {
                m_SubObjectTriggerInfo = JsonMapper.ToObject<List<DynamicObjectSubObjectTrigger>>(Row.SubObjectTriggerList);
            }
            catch
            {

            }

            if (m_SubObjectTriggerInfo != null)
            {
                foreach (var subObjectTrigger in m_SubObjectTriggerInfo)
                {
                    var collider = CachedTransfrom.Find(subObjectTrigger.Path)?.GetComponent<Collider>();
                    if (collider != null)
                    {
                        var receiver = collider.transform.GetOrAddComponent<PangooColliderReceiver>();
                        if (receiver != null)
                        {
                            receiver.dynamicObject = this;
                            receiver.subObjectTriggerEventType = subObjectTrigger.TriggerEventType;
                            receiver.subObjectTriggerPath = subObjectTrigger.Path;
                        }
                    }

                }
            }
        }

        public void SetColliderTriggerActive(bool val)
        {
            if (BaseColliders == null || (BaseColliders != null && BaseColliders.Length == 0)) return;
            foreach (var collider in BaseColliders)
            {
                if (collider.isTrigger)
                {
                    collider.enabled = val;
                }
            }
        }

        [LabelText("玩家是否在触发内")]
        [FoldoutGroup("碰撞触发")]
        public bool PlayerStayTrigger;

        [LabelText("玩家停留进度")]
        [FoldoutGroup("碰撞触发")]
        public float PlayerStayProgress;

        [LabelText("玩家退出进度")]
        [FoldoutGroup("碰撞触发")]
        public float PlayerStayExitProgress;

        [LabelText("玩家退出是否超时")]
        [FoldoutGroup("碰撞触发")]
        public bool IsTriggeredStayTimeoutExit;


        [LabelText("玩家退出停留是否")]
        [FoldoutGroup("碰撞触发")]
        public bool IsTriggeredStayTimeout;


        [LabelText("玩家上一帧是否在触发中")]
        [FoldoutGroup("碰撞触发")]
        public bool LastFramePlayerStayTrigger;

        protected override void DoFixedUpdate()
        {
            base.DoFixedUpdate();
            PlayerStayTrigger = false;
        }


        public void DoColliderTriggerUpdate()
        {
            switch (PlayerStayTrigger)
            {
                case true:
                    if (!LastFramePlayerStayTrigger)
                    {
                        TriggerInovke(TriggerTypeEnum.OnTriggerEnter3D);
                        PlayerStayExitProgress = 1;
                        IsTriggeredStayTimeoutExit = false;
                    }

                    if (Row.ColliderTriggerStayTimeout >= 0.1)
                    {
                        if (!IsTriggeredStayTimeout)
                        {
                            PlayerStayProgress += DeltaTime * (1 / Row.ColliderTriggerStayTimeout);
                            if (PlayerStayProgress >= 1)
                            {
                                IsTriggeredStayTimeout = true;
                                TriggerInovke(TriggerTypeEnum.OnTriggerStayTimeOut);
                            }
                        }
                    }
                    break;
                case false:
                    if (LastFramePlayerStayTrigger)
                    {
                        TriggerInovke(TriggerTypeEnum.OnTriggerExit3D);
                    }
                    else
                    {
                        if (Row.ColliderTriggerStayTimeout >= 0.1 && Row.ColliderTriggerStayExitDelay >= 0.1 && PlayerStayProgress > 0)
                        {
                            if (!IsTriggeredStayTimeoutExit)
                            {
                                PlayerStayExitProgress -= DeltaTime * (1 / Row.ColliderTriggerStayExitDelay);
                                if (PlayerStayExitProgress <= 0)
                                {
                                    IsTriggeredStayTimeoutExit = true;
                                    TriggerInovke(TriggerTypeEnum.OnTriggerStayTimeOutExit);
                                    PlayerStayProgress = 0;
                                    IsTriggeredStayTimeout = false;
                                }
                            }
                        }
                    }

                    break;
            }

            LastFramePlayerStayTrigger = PlayerStayTrigger;
        }


        public void TriggerStay3d(Collider collider)
        {
            if (collider.tag.Equals("Player"))
            {
                // Debug.Log($"TriggerStay3d:{Row.Name}");
                PlayerStayTrigger = true;
            }
        }

        public void TriggerEnter3d(Collider collider)
        {
            // if (collider.tag.Equals("Player"))
            // {
            //     Log($"EntityDynamicObject OnTriggerEnter,{Row.Name},{collider.gameObject.name}");
            //     if (EnterTriggerCount == 0)
            //     {
            //         TriggerInovke(TriggerTypeEnum.OnTriggerEnter3D);
            //     }
            //     EnterTriggerCount += 1;
            // }


        }

        public void TriggerExit3d(Collider collider)
        {

            // if (collider.tag.Equals("Player"))
            // {
            //     Log($"EntityDynamicObject OnTriggerExit");
            //     EnterTriggerCount -= 1;
            //     if (EnterTriggerCount <= 0)
            //     {
            //         TriggerInovke(TriggerTypeEnum.OnTriggerExit3D);
            //     }
            // }

        }

        public void ExtraTriggerEnter3d(Collider collider, string triggerPath)
        {
            EnterTriggerCount += 1;

            TriggerInovke(TriggerTypeEnum.OnExtraTriggerEnter3D, null, triggerPath);
        }

        public void ExtraTriggerExit3d(Collider collider, string triggerPath)
        {
            EnterTriggerCount -= 1;
            TriggerInovke(TriggerTypeEnum.OnExtraTriggerExit3D, null, triggerPath);

        }


    }
}