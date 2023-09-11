using System;
using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Cinemachine;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class CharacterCameraService : CharacterBaseService
    {
        CharacterCameraTypeEnum m_CharacterCameraType = CharacterCameraTypeEnum.FirstPerson;

        CinemachineVirtualCamera m_VirtualCamera;

        public Transform CameraTransform
        {
            get
            {
                if (m_VirtualCamera == null)
                {
                    return null;
                }
                return m_VirtualCamera.transform;
            }
        }

        [ShowInInspector]
        public CharacterCameraTypeEnum CharacterCameraType
        {
            get
            {
                return m_CharacterCameraType;
            }
            set
            {
                ChangeCameraType(value);
                m_CharacterCameraType = value;
            }
        }

        public override int Priority
        {
            get
            {
                return 0;
            }
        }

        public override void DoAwake(IServiceContainer container)
        {
            base.DoAwake(container);
            m_VirtualCamera = Character.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
            if (m_VirtualCamera == null)
            {
                var go = new GameObject("VirtualCamera");
                go.transform.parent = Character.CachedTransfrom;
                m_VirtualCamera = go.AddComponent<CinemachineVirtualCamera>();
            }
            ChangeCameraType(m_CharacterCameraType, true);
        }


        public void ChangeCameraType(CharacterCameraTypeEnum type, bool overwrite = false)
        {
            if (m_CharacterCameraType == type && !overwrite)
            {
                return;
            }

            switch (type)
            {
                case CharacterCameraTypeEnum.FirstPerson: SetFirstPerson(); break;
            }
        }

        public void SetFirstPerson()
        {
            m_VirtualCamera.Follow = Character.CachedTransfrom;
            var transposer = m_VirtualCamera.AddCinemachineComponent<CinemachineTransposer>();
            transposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
            transposer.m_FollowOffset = Vector3.zero;
            transposer.m_XDamping = 0;
            transposer.m_YawDamping = 0;
            transposer.m_ZDamping = 0;
        }

    }

}

