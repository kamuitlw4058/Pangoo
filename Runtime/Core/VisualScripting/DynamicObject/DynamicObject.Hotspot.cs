using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;
using Pangoo.MetaTable;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        HotspotGetRowByUuidHandler m_HotspotHandler;


        bool m_IsHotspotActive;

        [ShowInInspector]
        public bool IsHotspotActive
        {
            get
            {
                return !IsInteracting && m_IsHotspotActive && (!m_Tracker?.InteractBlocked ?? true);
                // return IsHotspotDistanceActive && !IsInteracting && m_IsHotspotActive && (!m_Tracker?.InteractBlocked ?? true);
            }
            set
            {
                m_IsHotspotActive = value;
            }
        }

        [ShowInInspector]
        public bool IsHotspotDistanceActive { get; private set; }


        [ShowInInspector]
        public bool IsHotspotInteractActive { get; private set; }


        bool m_CanHotspotBan;
        [ShowInInspector]
        public bool CanHotspotBan
        {
            get
            {
                return m_CanHotspotBan;
            }
            set
            {
                m_CanHotspotBan = value;
                if (m_Tracker != null)
                {
                    m_Tracker.InteractCanBan = value;
                }

            }
        }
        [ShowInInspector]
        public bool IsHotspotBanInteractActive
        {
            get
            {
                return EnterTriggerCount > 0 && CanHotspotBan;
            }
        }

        [ShowInInspector]
        public float Distance { get; private set; }

        public float InteractDistance { get; private set; }




        public Character Target;

        public Vector3 HotspotInteractPosition => m_Tracker?.Position ?? this.CachedTransfrom.position;



        [ShowInInspector]
        public float Transition { get; private set; }


        [ShowInInspector]
        public float Radius
        {
            get => this.Row.HotspotRadius > 0 ?
                 this.Row.HotspotRadius : this.Main.DefaultHotspotRadius;
        }


        [ShowInInspector]
        List<HotSpot> m_HotSpots = new List<HotSpot>();



        void DoAwakeHotspot()
        {

            if (Main.MetaTable != null)
            {
                m_HotspotHandler = Main.MetaTable.GetHotspotByUuid;

            }


            var uuids = Row.GetHotspotUuidList();
            m_HotSpots.Clear();
            if (Row.UseHotspot && !Row.DefaultHideHotspot)
            {
                IsHotspotActive = true;
            }
            else
            {
                IsHotspotActive = false;
            }

            if (Row.UseHotspot)
            {
                foreach (var valId in uuids)
                {
                    IHotspotRow row = HotspotRowExtension.GetByUuid(valId, m_HotspotHandler);
                    Log($"Create Hotspot:{valId}  row:{row}");
                    if (row != null)
                    {
                        var instance = ClassUtility.CreateInstance<HotSpot>(row.HotspotType);
                        if (instance == null)
                        {
                            Log($"Create Hotspot Failed!!:{instance}");
                            return;
                        }
                        instance.Row = row;
                        instance.dynamicObject = this;
                        instance.Master = this;
                        instance.LoadParamsFromJson(row.Params);
                        m_HotSpots.Add(instance);
                        Log($"Add Hotspot To List:{m_HotSpots.Count}");

                    }
                }
            }

        }

        private void DoUpdateHotspot()
        {

            if (m_IsHotspotActive && Main.CharacterService.PlayerEnabledHotspot)
            {

                bool wasActive = this.IsHotspotDistanceActive;
                this.Target = Character?.Player?.character;

                if (this.Target == null)
                {
                    this.IsHotspotDistanceActive = false;
                    this.Distance = float.MaxValue;
                }
                else
                {
                    this.Distance = Vector3.Distance(
                        this.Target.CachedTransfrom.position,
                        this.HotspotInteractPosition
                    );

                    this.IsHotspotDistanceActive = this.Distance <= this.Radius;

                    this.IsHotspotInteractActive = (Target.Target == (m_Tracker as IInteractive) && m_Tracker != null);
                }
            }
            else
            {
                this.IsHotspotDistanceActive = false;
                this.IsHotspotInteractActive = false;

            }


            foreach (var spot in m_HotSpots)
            {
                spot.Update();
            }


            // switch (wasActive)
            // {
            //     case false when this.IsHotspotActive: this.EventOnActivate?.Invoke(); break;
            //     case true when !this.IsHotspotActive: this.EventOnDeactivate?.Invoke(); break;
            // }
        }




    }
}