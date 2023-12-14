using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        bool m_IsEnterTrigger;

        public bool IsEnterTrigger
        {
            get
            {
                return m_IsEnterTrigger;
            }
            set
            {
                m_IsEnterTrigger = value;
                if (m_Tracker != null)
                {
                    m_Tracker.InteractTriggerEnter = value;
                }
            }
        }




        public void TriggerEnter3d(Collider collider)
        {
            IsEnterTrigger = true;

            TriggerInovke(TriggerTypeEnum.OnTriggerEnter3D);

        }

        public void TriggerExit3d(Collider collider)
        {
            IsEnterTrigger = false;
            TriggerInovke(TriggerTypeEnum.OnTriggerExit3D);

        }




    }
}