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

        public Action<Args> PlayTimelineEvent;

        InstructionPlayTimeline PlayTimelineInstruction;


        PlayableDirector playableDirector { get; set; }

        void DoAwakeTimeline()
        {
            playableDirector = gameObject.GetComponent<PlayableDirector>();
            if (playableDirector != null)
            {
                PlayTimelineEvent += OnPlayTimelineEvent;
                PlayTimelineInstruction = Activator.CreateInstance<InstructionPlayTimeline>();
            }
        }


        void OnPlayTimelineEvent(Args args)
        {
            if (PlayTimelineInstruction != null)
            {
                args.ChangeTarget(gameObject);
                PlayTimelineInstruction.RunImmediate(args);
            }
        }


        public void PlayTimeline()
        {
            PlayTimelineEvent?.Invoke(CurrentArgs);
        }


    }
}