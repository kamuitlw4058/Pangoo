using System;
using GameFramework;
using GameFramework.Sound;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class ProcedurePreload : ProcedureBase
    {
        bool ResourceInited = false;
        GameMainConfig packageConfig;
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Application.targetFrameRate = -1;
            PangooEntry.Debugger.ActiveWindow = false;
            Log.Info($"Entery ProcedurePreload");


            packageConfig = PangooEntry.GameConfig.GetGameMainConfig();
            if(packageConfig != null && packageConfig.LogoEntries != null
            && packageConfig.LogoEntries.Count > 0){
                var LogEntries = packageConfig.LogoEntries;
                var LogEntiry = LogEntries[0];
                if(!string.IsNullOrEmpty(LogEntiry.LogoUIConfig.PackageName) 
                && !string.IsNullOrEmpty(LogEntiry.LogoUIConfig.ComponentName) 
                && !string.IsNullOrEmpty(LogEntiry.LogoUIConfig.Name)
                && !string.IsNullOrEmpty(LogEntiry.LogoUIType)){
                    Type type = TypeUtility.GetRuntimeType(LogEntiry.LogoUIType);
                    PangooEntry.FGUI.OpenResourceUI(type,LogEntiry.LogoUIConfig);
                }
            }

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
            if(ResourceInited){
                
                if(packageConfig != null){
                    if(!string.IsNullOrEmpty(packageConfig.DefaultJumpScene) && packageConfig.DefaultJumpScene != ConstString.NULL){
                        PangooEntry.PangooScene.LoadScene(packageConfig.GetDefaultJumpScene());
                    }


                    if(!string.IsNullOrEmpty( packageConfig.EntryProcedure)){
                        Type procedureType = Utility.Assembly.GetType( packageConfig.EntryProcedure);
                        ChangeState(procedureOwner,procedureType);
                    }
                }
            }

        }


        void OnInitResourcesComplete()
        {

            // Debug.Log($"Init Resources Finish:{PangooEntry.Resource.ApplicableGameVersion}, {PangooEntry.Resource.AssetCount}");
            PangooEntry.Event.Fire(this,ResourceInitCompleteEventArgs.Create());
            ResourceInited = true;


            // if(packageConfig == null){
            //     PangooEntry.GameConfig.LoadAllConfig();
            // }
        }
    }
}