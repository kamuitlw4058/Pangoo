using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo{

    [ExecuteAlways]
    public class ParticleSystemHelper : MonoBehaviour
    {
        [SerializeField][ReadOnly] ParticleSystem m_Com;

        [SerializeField] int EmitCount = 1;

        private void OnEnable() {
             m_Com = GetComponent<ParticleSystem>();
        }

        [Button("Play")]
        public void Play(){
            if(m_Com != null){
                m_Com.Play();
            }
        }

        [Button("Emit")]
        public void Emit(){
            if(m_Com != null){
                m_Com.Emit(EmitCount);
            }
        }


    }
}