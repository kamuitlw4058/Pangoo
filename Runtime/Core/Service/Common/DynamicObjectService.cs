using Sirenix.OdinInspector;
using Pangoo.MetaTable;
using System.Collections.Generic;
using System;
using Pangoo.Common;


namespace Pangoo.Core.Services
{
    public partial class DynamicObjectService : EntityLoaderService<EntityDynamicObjectData>
    {
        public override string ServiceName => "DynamicObjectService";

        DynamicObjectInfo m_DynamicObjectInfo;
        IEntityGroupRow m_EntityGroupRow;

        public override void Start()
        {
            m_DynamicObjectInfo = GameInfoSrv.GetGameInfo<DynamicObjectInfo>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();
        }

        public override EntityDynamicObjectData GetEntityData(string uuid)
        {
            if (EntityDataDict.TryGetValue(uuid, out EntityDynamicObjectData entityStaticSceneData))
            {
                return entityStaticSceneData;
            }

            var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(uuid);
            if (info == null)
            {
                return null;
            }
            var data = EntityDynamicObjectData.Create(this, info, m_EntityGroupRow, null);
            EntityDataDict.Add(uuid, data);
            return data;
        }


        public List<string> DiffItems(List<string> targets, List<string> currents)
        {
            return targets.DiffItems(currents);
        }

    }
}