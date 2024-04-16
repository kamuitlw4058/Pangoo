using Sirenix.OdinInspector;
using Pangoo.MetaTable;
using System.Collections.Generic;
using System;


namespace Pangoo.Core.Services
{
    public partial class NewDynamicObjectService : EntityLoaderService<EntityDynamicObjectData>
    {
        public override string ServiceName => "NewDynamicObjectService";

        DynamicObjectInfo m_DynamicObjectInfo;

        IEntityGroupRow m_EntityGroupRow;


        Dictionary<string, EntityDynamicObjectData> EntityDataDict = new Dictionary<string, EntityDynamicObjectData>();

        public override void Start()
        {
            m_DynamicObjectInfo = GameInfoSrv.GetGameInfo<DynamicObjectInfo>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();

        }

        [Button]
        public void ShowEntity()
        {
            var uuid = "e69c90b5ed7e4195ae90981fb7cd4eca";
            var info = m_DynamicObjectInfo.GetRowByUuid<DynamicObjectInfoRow>(uuid);
            var data = EntityDynamicObjectData.Create(DynamicObjectSrv, info, m_EntityGroupRow, null);
            if (!EntityDataDict.ContainsKey(uuid))
            {
                EntityDataDict.Add(uuid, data);
            }
            ShowEntity(data, (o) =>
            {
                Log($"On Show Entity Sucess!:{uuid}");
            }, (o) =>
            {
                Log($"On Show Entity Failed!:{uuid}");
            }
            );
        }

        [Button]
        public void HideEntity()
        {
            var uuid = "e69c90b5ed7e4195ae90981fb7cd4eca";
            if (EntityDataDict.TryGetValue(uuid, out EntityDynamicObjectData data))
            {
                HideEntity(data);
            }

        }

    }
}