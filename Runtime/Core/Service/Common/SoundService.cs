using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.Services
{
    public class SoundService : BaseService
    {
        ExcelTableService m_ExcelTableService;


        public ExcelTableService TableService
        {
            get
            {
                return m_ExcelTableService;
            }
        }
        SoundTable m_SoundTable;


        protected override void DoAwake()
        {
            base.DoAwake();

            m_ExcelTableService = Parent.GetService<ExcelTableService>();
        }

        protected override void DoStart()
        {

            m_SoundTable = m_ExcelTableService.GetExcelTable<SoundTable>();

            // m_EntityGroupRow = EntityGroupRowExtension.CreateDynamicObjectGroup();

            // m_DynamicObjectInfo = m_GameInfoService.GetGameInfo<DynamicObjectInfo>();
            // Debug.Log($"DoStart DynamicObjectService :{m_EntityGroupRow} m_EntityGroupRow:{m_EntityGroupRow.Name}");
        }

        [Button("播放")]
        public void PlaySound(int id)
        {
            var row = m_SoundTable.GetRowById(id);
            var path = AssetUtility.GetSoundAssetPath(row.PackageDir, row.SoundType, row.AssetPath);
            int traceId = PangooEntry.Sound.PlaySound(path, "Default");
            Debug.Log($"Start Play :{path}. traceId:{traceId}");
        }

        [Button("停止")]
        public void StopSound(int id)
        {
            PangooEntry.Sound.StopSound(id);
        }


    }
}