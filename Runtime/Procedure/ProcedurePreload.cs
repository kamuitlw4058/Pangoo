using System;
using GameFramework;
using GameFramework.Sound;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;


namespace Pangoo
{
    public class ProcedurePreload : ProcedureBase
    {
        bool ResourceInited = false;
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Application.targetFrameRate = -1;
            PangooEntry.Debugger.ActiveWindow = false;

            if (!PangooEntry.Base.EditorResourceMode)
            {
                PangooEntry.Resource.InitResources(OnInitResourcesComplete);
            }
            else
            {
                OnInitResourcesComplete();
            }
        }

        void ExcelTableLoadTable()
        {
            var table = PangooEntry.ExcelTable.GetExcelTable<UiConfigInfoTable>();
            if(table != null){
                PangooEntry.FGUI.SetUiConfigTable(table);
                PangooEntry.FGUI.MainsapceGetType = GetType;
            }
        }

        public Type GetType(string name)
        {
            return Type.GetType(name);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {

            if (PangooEntry.GameConfig.IsLoaded && ResourceInited)
            {
                PangooEntry.ExcelTable.LoadExcelTable(ExcelTableLoadTable);
                var config = PangooEntry.GameConfig.GetGameMainConfig();
                if(!string.IsNullOrEmpty( config.EntryProcedure)){
                    Type procedureType = Utility.Assembly.GetType( config.EntryProcedure);
                    ChangeState(procedureOwner,procedureType);
                }
            }
        }


        void PlayAudio()
        {
            var path = "Assets/GameMain/StreamRes/Sound/Audio/test.mp3";
            var id = PangooEntry.Sound.PlaySound(path, "Default", new PlaySoundParams()
            {
                Loop = true
            });
            Debug.Log($"Init After play sound:{id}");
        }

        void OnInitResourcesComplete()
        {

            // Debug.Log($"Init Resources Finish:{PangooEntry.Resource.ApplicableGameVersion}, {PangooEntry.Resource.AssetCount}");
            PangooEntry.Event.Fire(this,ResourceInitCompleteEventArgs.Create());
            ResourceInited = true;
            PangooEntry.GameConfig.LoadAllConfig();
        }
    }
}