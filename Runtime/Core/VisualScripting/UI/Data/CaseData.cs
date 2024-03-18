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
    public class CaseContent
    {
        public Args args;

        public UIService UIService;

        public ICasesRow CaseRow;

        public void ShowCaseDynamicObject()
        {
            args?.Main?.DynamicObject.ShowModuleDynamicObject(ConstString.CaseModule, CaseRow.Uuid);
        }

    }
}