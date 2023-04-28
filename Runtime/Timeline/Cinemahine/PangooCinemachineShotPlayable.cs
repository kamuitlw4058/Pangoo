

using Cinemachine;
using UnityEngine.Playables;

namespace Pangoo.Timeline
{
    internal sealed class PangooCinemachineShotPlayable : PlayableBehaviour
    {
        public CinemachineVirtualCameraBase VirtualCamera;
        public bool IsValid { get { return VirtualCamera != null; } }
    }
}
