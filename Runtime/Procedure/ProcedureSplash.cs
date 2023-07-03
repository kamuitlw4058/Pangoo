using System;
#if ENABLE_FGUI
using FairyGUI;
#endif
using UnityGameFramework.Runtime;
using GameFramework.Resource;
using GameFramework;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Pangoo
{
    public class ProcedureSplash : PangooProcedureBase

    {
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            Splash();
            base.OnEnter(procedureOwner);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (PangooEntry.Base.EditorResourceMode)
            {
                // 编辑器模式
                Log.Info("Editor resource mode detected.");
                ChangeState<ProcedurePreload>(procedureOwner);
            }
            else if (PangooEntry.Resource.ResourceMode == ResourceMode.Package)
            {
                // 单机模式
                Log.Info("Package resource mode detected.");
                ChangeState<ProcedureInitResources>(procedureOwner);
            }
            else
            {
                // 可更新模式
                Log.Info("Updatable resource mode detected.");
                //TODO: 暂时不考虑更新模式。
                // ChangeState<ProcedureCheckVersion>(procedureOwner);
            }
        }

    #if ENABLE_FGUI

        private GComponent view;
    #endif
        private string fguiPath;
        private Type type;
        void Splash(){
    #if ENABLE_FGUI
                var packageConfig = PangooEntry.GameConfig.GetGameMainConfig();
                if(packageConfig != null && packageConfig.LogoEntries != null
                && packageConfig.LogoEntries.Count > 0){
                    var LogEntries = packageConfig.LogoEntries;
                    var LogEntiry = LogEntries[0];
                    if(!string.IsNullOrEmpty(LogEntiry.LogoUIConfig.PackageName) 
                    && !string.IsNullOrEmpty(LogEntiry.LogoUIConfig.ComponentName) 
                    && !string.IsNullOrEmpty(LogEntiry.LogoUIConfig.Name)
                    && !string.IsNullOrEmpty(LogEntiry.LogoUIType)){
                        
                        type = Utility.Assembly.GetType(LogEntiry.LogoUIType);
                        PangooEntry.FGUI.OpenResourceUI(type,LogEntiry.LogoUIConfig);
                    }
                }
#endif
        }


        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            //view.Dispose();
           #if ENABLE_FGUI
            PangooEntry.FGUI.CloseUI(type);
            #endif
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}