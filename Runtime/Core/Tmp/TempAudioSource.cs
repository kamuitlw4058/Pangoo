using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo
{

    public class TempAudioSource : MonoBehaviour
    {

        public AudioSource audioSource;

        public bool AfterStart;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

        }

        [ShowInInspector]
        public bool? IsPlaying
        {
            get
            {
                if (!AfterStart)
                {
                    return false;
                }
                return audioSource?.isPlaying;
            }
        }

        public float? Time
        {
            get
            {
                if (!AfterStart)
                {
                    return null;
                }

                return audioSource.time;
                // return audioSource
                // return audioSource?.
            }
        }

        [Button("Play")]
        public void Play()
        {
            AfterStart = true;
            audioSource?.Play();
        }

        [Button("Pause")]
        public void Pause()
        {
            audioSource?.Pause();
        }


        [Button("Stop")]
        public void Stop()
        {
            audioSource?.Stop();
        }

        [Button("Unpause")]
        public void Unpause()
        {
            audioSource?.UnPause();
        }



        private void Update()
        {
            if (AfterStart && !IsPlaying.Value && audioSource.clip != null)
            {
                Debug.Log($"Reset");
                Reset();
            }


        }

        public void Reset()
        {
            audioSource.clip = null;

        }

    }
}
