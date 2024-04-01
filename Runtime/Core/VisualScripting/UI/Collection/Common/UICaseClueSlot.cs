using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;
using Pangoo.Common;
using System.Linq;
using System.Runtime.InteropServices;
using Pangoo.MetaTable;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class UICaseClueSlot
    {
        public EntityDynamicObject ClueEntity;

        public UIClueItem ClueItem;

        public GameObject ClueItemGo;

        public UICasePanel Panel;


        public IClueRow ClueRow;

        public bool Has
        {
            get
            {
                return Panel.Main.Case.GetClueHas(ClueRow.Uuid);
            }
        }

        public bool IsRemoved
        {
            get
            {
                return Panel.Main.Case.GetClueIsRemoved(ClueRow.Uuid);
            }
        }




    }


}