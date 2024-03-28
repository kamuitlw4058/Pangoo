using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Common;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class CaseContent
    {
        public Args args;

        public UIService UIService;

        public string CaseUuid;

        public ICasesRow CaseRow;

        [ShowInInspector]
        public IClueRow[] CluesRows;

        public EntityDynamicObject Entity;

        public Dictionary<string, EntityDynamicObject> CluesEntity = new Dictionary<string, EntityDynamicObject>();


        [ShowInInspector]
        public bool AllCluesLoaded
        {
            get
            {
                return CluesEntity?.Count == CluesRows?.Length;
            }

        }

        [ShowInInspector]
        public Vector3 Position
        {
            get
            {
                return Entity?.CachedTransform.position ?? Vector3.zero;
            }
            set
            {
                if (Entity != null)
                {
                    Entity.CachedTransform.position = value;
                }
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return Entity?.CachedTransform.localRotation ?? Quaternion.identity;
            }
            set
            {
                if (Entity != null)
                {
                    Entity.CachedTransform.localRotation = value;
                }
            }
        }


        public bool CaseModelActive
        {
            get
            {
                return Entity?.DynamicObj.ModelActive ?? false;
            }
            set
            {
                if (Entity != null) Entity.DynamicObj.ModelActive = value;
            }
        }


    }
}