using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Pangoo.Timeline
{

    [DisallowMultipleComponent]
    public class OpenCtrlCinemahineBase : MonoBehaviour
    {
        public int Index;
        public List<CinemachineVirtualCameraBase> Cameras;

        public Vector3 Forward;



    }
}