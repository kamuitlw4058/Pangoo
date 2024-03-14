using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class CaseData
    {
        public Args args;

        public UIService UIService;

        public ICasesRow CaseRow;


        public Vector3 OldPosition { get; set; }

        public Vector3 OldRotation { get; set; }


        public Vector3 OldScale { get; set; }

    }
}