using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        HotspotTable m_HotspotTable;

        List<HotspotTable.HotspotRow> m_HotspotRows = new();


        private const float TRANSITION_SMOOTH_TIME = 0.25f;
        [ShowInInspector]
        public bool IsHotspotActive
        {
            get
            {
                return IsHotspotDistanceActive && !IsInteracting;
            }
        }

        [ShowInInspector]
        public bool IsHotspotDistanceActive { get; private set; }


        [ShowInInspector]
        public bool IsHotspotInteractActive { get; private set; }


        bool m_CanHotspotBan;
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
                return IsEnterTrigger && CanHotspotBan;
            }
        }

        [ShowInInspector]
        public float Distance { get; private set; }

        public float InteractDistance { get; private set; }




        public Character Target;

        public Vector3 HotspotInteractPosition => m_Tracker?.Position ?? this.CachedTransfrom.position;



        [ShowInInspector]
        public float Transition { get; private set; }

        private float m_Velocity;

        [ShowInInspector]
        public float Radius
        {
            get => this.Row.HotspotRadius >= 0 ? this.Row.HotspotRadius : float.MaxValue;
        }

        public float InteractRadius
        {
            get => this.Row.InteractRadius >= 0 ? this.Row.InteractRadius : float.MaxValue;
        }

        [ShowInInspector]
        List<HotSpot> m_HotSpots = new List<HotSpot>();



        void DoAwakeHotspot()
        {
            m_HotspotTable = TableService?.GetExcelTable<HotspotTable>();

            var ids = Row.GetHotspotIdList();
            m_HotspotRows.Clear();
            m_HotSpots.Clear();

            foreach (var valId in ids)
            {
                HotspotTable.HotspotRow row = GetHotspotRow(valId);
                Debug.Log($"Create Hotspot:{valId}  row:{row}");
                if (row != null)
                {
                    m_HotspotRows.Add(row);
                }
            }

            foreach (var row in m_HotspotRows)
            {
                var instance = ClassUtility.CreateInstance<HotSpot>(row.HotspotType);
                if (instance == null)
                {
                    return;
                }
                instance.Row = row;
                instance.dynamicObject = this;
                instance.LoadParamsFromJson(row.Params);
                m_HotSpots.Add(instance);
            }

        }




        private void DoUpdateHotspot()
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

        public HotspotTable.HotspotRow GetHotspotRow(int id)
        {
            HotspotTable.HotspotRow row = null;
#if UNITY_EDITOR
            if (Application.isPlaying && m_HotspotTable != null)
            {
                row = m_HotspotTable.GetRowById(id);
            }
            else
            {
                row = GameSupportEditorUtility.GetHotspotRowById(id);
            }
#else
            row = m_HotspotTable.GetRowById(id);
#endif
            return row;
        }



    }
}