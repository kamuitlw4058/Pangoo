using System;
using System.Collections;
using System.Threading.Tasks;
using GameFramework;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    [Common.Title("Condition Always")]
    // [Description("Prints a message to the Unity Console")]

    [Category("Debug/Always")]

    // [Parameter(
    //     "Message",
    //     "The text message to log"
    // )]

    // [Keywords("Debug", "Log", "Print", "Show", "Display", "Name", "Test", "Message", "String")]
    // [Image(typeof(IconBug), ColorTheme.Type.TextLight)]

    [Serializable]
    public class ConditionDebug : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        ConditionBoolParams m_Params = new ConditionBoolParams();

        public override IParams Params => m_Params;
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        // PROPERTIES: ----------------------------------------------------------------------------



        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ConditionDebug()
        { }


        protected override bool Run(Args args)
        {
            return m_Params.Ok;
        }


        // METHODS: -------------------------------------------------------------------------------

    }
}
