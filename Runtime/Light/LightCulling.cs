using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{

    public class LightCulling : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlayerCamera;
        [SerializeField] private float m_ShadowCullingDistance = 15f;
        [SerializeField] private float m_LightCullingDistance = 20f;

        [SerializeField] private float m_CameraDistance;

        private Light  _Light;
        // Start is called before the first frame update
        void Start()
        {
            _Light = GetComponent<Light>();
            m_PlayerCamera = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if(m_PlayerCamera == null){
                return;
            }

            m_CameraDistance =  Vector3.Distance(m_PlayerCamera.transform.position,transform.position);
            if(m_CameraDistance <= m_ShadowCullingDistance){
                _Light.shadows = LightShadows.Soft;
            }else {
                _Light.shadows = LightShadows.None;
            }

            if(m_CameraDistance <= m_LightCullingDistance){
                _Light.enabled = true;
            }else{
                _Light.enabled = false;
            }
        }
    }
}