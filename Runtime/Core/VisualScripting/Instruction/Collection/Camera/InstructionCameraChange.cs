using System;
using System.Collections;
using System.Threading.Tasks;
using Cinemachine;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting{

    // [Version(0, 1, 1)]

    [Title("Change Camera")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Camera/Change Camera")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]
    
    [Serializable]
    public class InstructionCameraChange : Instruction
    {
        public const int ActiveCameraPriority = 1000;
        // MEMBERS: -------------------------------------------------------------------------------
        
        public enum ChangeType{
            SelfCamera,
        }

        public ChangeType m_ChangeType;

        
        public override string Title => $"Change Camera: {this.m_ChangeType}";



        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionCameraChange()
        { }

        protected override IEnumerator Run(Args args)
        {
            if(args.Self == null){
                Debug.Log($"Self is null");
                yield break;
            }

            var camera = args.Self.GetComponentInChildren<CinemachineVirtualCamera>();
            if(camera == null){
                Debug.Log($"Self Camera is null");
                yield break;
            }
            camera.Priority = ActiveCameraPriority;
            Debug.Log($"ChangeType:{m_ChangeType} camera:{camera}");
            yield break;
        }

        public override string ParamsString()
        {
            return m_ChangeType.ToString();
        }

        public override void LoadParams(string instructionParams)
        {
            m_ChangeType = instructionParams.ToEnum<ChangeType>();
        }

        // METHODS: -------------------------------------------------------------------------------

    }
}
