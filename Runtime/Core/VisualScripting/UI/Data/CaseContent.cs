using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Common;
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

        [ShowInInspector]
        public Dictionary<string, IClueRow> ClueDict = new Dictionary<string, IClueRow>();


        public EntityDynamicObject Entity;

        [ShowInInspector]
        public Dictionary<string, EntityDynamicObject> CluesEntity = new Dictionary<string, EntityDynamicObject>();

        [ShowInInspector]
        public Dictionary<string, string> DynamicObject2Clue = new Dictionary<string, string>();


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

        ClueIntegrate[] m_IntegrateInfos;
        public ClueIntegrate[] IntegrateInfos
        {
            get
            {
                if (m_IntegrateInfos == null)
                {
                    m_IntegrateInfos = JsonMapper.ToObject<ClueIntegrate[]>(CaseRow.CluesIntegrate);
                }
                return m_IntegrateInfos;
            }
        }


    }
}