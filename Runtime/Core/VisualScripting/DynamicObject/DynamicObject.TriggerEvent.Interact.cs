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
        [ShowInInspector]
        public bool? IsInteractDisable
        {
            get
            {
                return m_Tracker?.InteractDisabled;
            }
        }

        [ShowInInspector]
        public bool IsInteracting
        {
            get
            {
                return m_Tracker?.IsInteracting ?? false;
            }
        }


        public InteractionItemTracker m_Tracker = null;


        void OnInteract(Characters.Character character, IInteractive interactive)
        {

            TriggerInovke(TriggerTypeEnum.OnInteract);

        }


        [Button("触发交互指令")]
        void OnInteract()
        {
            OnInteract(null, null);
        }



        void OnInteractEnd()
        {
            bool isRunning = TriggerIsRunning(TriggerTypeEnum.OnInteract);
            if (!isRunning)
            {
                m_Tracker?.Stop();
            }
        }


    }
}