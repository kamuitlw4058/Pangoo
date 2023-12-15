using System;
using System.Collections;
using System.Linq;
using Pangoo.Core.Common;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    // [Title("Change Camera")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Common/SetGameObjectActive")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class InstructionGameObjectActive : Instruction
    {

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSubGameObjectBoolParams ParamsRaw = new InstructionSubGameObjectBoolParams();

        public override IParams Params => this.ParamsRaw;
        public InstructionGameObjectActive()
        { }

        protected override IEnumerator Run(Args args)
        {
            yield break;

        }

        public override void RunImmediate(Args args)
        {
            Transform trans=null;
            if (!ParamsRaw.IsGlobal)
            {
                trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            }
            else
            {
                trans = GameObject.Find(ParamsRaw.Root).transform.Find(ParamsRaw.Path);
            }
            Debug.Log("节点:"+ParamsRaw.Root+"<>"+ParamsRaw.Path);
            if (trans != null)
            {
                trans.gameObject.SetActive(ParamsRaw.Val);
            }
            return;
        }


    }
}
