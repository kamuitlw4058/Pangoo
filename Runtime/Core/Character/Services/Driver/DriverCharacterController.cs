using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Character{

    public class DriverCharacterController : DriverService
    {
        [ShowInInspector,ReadOnly]
        CharacterController m_Controller;

        [ShowInInspector]
        public bool ControllerEnable{
            get{
                if(m_Controller == null){
                    return false;
                }
                return m_Controller.enabled;
            }
        }

        public override void DoAwake(IServiceContainer services)
        {
            base.DoAwake(services);
            
            this.m_Controller = Character.gameObject.GetComponent<CharacterController>();
            if (this.m_Controller == null)
            {
                GameObject instance = Character.gameObject;
                this.m_Controller = instance.AddComponent<CharacterController>();
                this.m_Controller.hideFlags = HideFlags.HideInInspector;
            }
        }

        public override void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            Debug.Log($"Update Services:{Services}");
            MoveDirection = Services.GetVariable<Vector3>("MoveDirection");
            Debug.Log($"Update MoveDirection:{MoveDirection}");
            // if (this.Character.IsDead) return;
            // if (this.m_Controller == null) return;
            
            // this.UpdateProperties();

            // this.UpdateGravity(this.Character.Motion);
            // this.UpdateJump(this.Character.Motion);

            // this.UpdateTranslation(this.Character.Motion);
            // this.m_Axonometry?.ProcessPosition(this, this.Transform.position);

            if (this.m_Controller.enabled && MoveDirection != Vector3.zero)
            {
                Debug.Log($"Move MoveDirection:{MoveDirection}");
                this.m_Controller.Move(MoveDirection);
            }

            Services.SetVariable<Vector3>("MoveDirection",Vector3.zero);
        }

        public override void DoDestroy()
        {
            Object.Destroy(this.m_Controller);
            base.DoDestroy();
        }
    }

}

