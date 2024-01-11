using System;
using System.Collections;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    // [Title("Change Camera")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Common/SetSelfTriggerEnabled")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionSetDOTriggerEnabled : Instruction
    {

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionDynamicObejctTriggerEventParams ParamsRaw = new InstructionDynamicObejctTriggerEventParams();
        public override IParams Params => this.ParamsRaw;


        public InstructionSetDOTriggerEnabled()
        { }

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            if (ParamsRaw.DisableSelfTrigger && Trigger != null)
            {
                Trigger.Enabled = false;
            }
            var dynamicObjectEntity = args.dynamicObject?.DynamicObjectService?.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
            if (dynamicObjectEntity != null)
            {
                dynamicObjectEntity?.DynamicObj?.TriggerEnabled(ParamsRaw.TriggerEventUuid, ParamsRaw.Enabled);
                Debug.Log($"dynamicObjectEntity.DynamicObj.{dynamicObjectEntity},{dynamicObjectEntity?.DynamicObj},{Trigger}");
            }
            return;
        }



    }
}
