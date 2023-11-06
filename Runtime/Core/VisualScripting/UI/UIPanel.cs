using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{

    public abstract class UIPanel : UIFormLogic, IParams
    {
        protected virtual IParams Params { get; }

        public void CloseSelf()
        {
            PangooEntry.UI?.CloseUIForm(UIForm);
        }

        public void Load(string val)
        {
            Params.Load(val);
        }

        public string Save()
        {
            return Params.Save();
        }
    }
}