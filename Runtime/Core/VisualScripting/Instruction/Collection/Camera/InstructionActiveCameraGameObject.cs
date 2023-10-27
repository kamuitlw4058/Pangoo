using System;
using System.Collections;
using Cinemachine;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    // [Title("Change Camera")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Camera/Active Camera GameObject")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionActiveCameraGameObject : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------


        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSubGameObjectBoolParams ParamsRaw = new InstructionSubGameObjectBoolParams();

        public override IParams Params => this.ParamsRaw;


        public override string Title => $"Change Camera: ";

        public override InstructionType InstructionType => InstructionType.Coroutine;


        public InstructionActiveCameraGameObject()
        { }

        protected override IEnumerator Run(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            Debug.Log($"trans:{trans}");
            if (trans != null)
            {
                trans.gameObject.SetActive(true);
                var camera = trans.GetComponent<CinemachineVirtualCamera>();
                var cameraBrain = GameObject.FindWithTag("MainCamera")?.GetComponent<CinemachineBrain>();
                Debug.Log($"camera:{camera} cameraBrain:{cameraBrain}");
                if (camera == null | cameraBrain == null)
                {
                    yield break;
                }
                else
                {
                    yield return null;
                }

                if (cameraBrain.ActiveVirtualCamera != camera)
                {
                    yield break;
                }

                while (ParamsRaw.Val && cameraBrain.IsBlending)
                {
                    yield return null;
                }

            }
            else
            {
                yield break;
            }



        }

        public override void RunImmediate(Args args)
        {

            return;
        }




    }
}
