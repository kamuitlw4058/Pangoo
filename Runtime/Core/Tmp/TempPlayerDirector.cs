using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;
using Pangoo.Core.Services;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace Pangoo
{

    public class TempPlayerDirector : MonoBehaviour
    {

        public PlayableDirector playableDirector;
        public bool AfterStart;

        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            AfterStart = true;

        }

        [ShowInInspector]
        public PlayState? playState
        {
            get
            {
                if (!AfterStart)
                {
                    return null;
                }
                return playableDirector?.state;
            }
        }

        [ShowInInspector]
        public bool IsFinished
        {
            get
            {
                if (!AfterStart)
                {
                    return false;
                }

                if (playableDirector?.playableAsset == null)
                {
                    return false;
                }



                return playableDirector.time == playableDirector.playableAsset.duration;
            }
        }

    }
}
