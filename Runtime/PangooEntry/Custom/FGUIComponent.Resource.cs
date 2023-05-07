//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
#if ENABLE_FGUI
using FairyGUI;
#endif
using GameFramework;
using GameFramework.ObjectPool;
// using GameFramework.Resource;
using GameFramework.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;


namespace Pangoo
{

#if ENABLE_FGUI
    /// <summary>
    /// FGUI组件。
    /// </summary>
    public sealed partial class FGUIComponent
    {
        public void OpenResourceUI(Type type,UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData = null) 
        {
            InternalOpenUI(type,uiConfig, userData,useResource:true);
        }
        public void OpenResourceUI<T>(UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData = null) where T : UILogicBase, new()
        {
            InternalOpenUI<T>(uiConfig, userData,useResource:true);
        }


        public void OpenResourceUI<T>(string PackageName, string ComponentName, object userData = null,int sortingOrder = 0,bool blurMask = false,bool HidePause = false) where T : UILogicBase, new()
        {
            var uiConfig = new UiConfigInfoTable.UiConfigInfoRow();
            uiConfig.PackageName = PackageName;
            uiConfig.ComponentName = ComponentName;
            uiConfig.SortingOrder = sortingOrder;
            uiConfig.BlurMask = blurMask;
            uiConfig.HidePause = HidePause;
            OpenResourceUI<T>(uiConfig, userData);
        }

        private void LoadResourceUI(UILogicBase uiLogic, UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData)
        {
            var uiPackage = GetPackageInfo(uiConfig.PackageName).Package;
            if (uiPackage == null)
            {
                var path = AssetUtility.GetResourceFGUIPackage(uiConfig.PackageName);
                // Log.Info($"Load Resource FGUI desc asset from path:'{path}':{Time.frameCount}");
                var asset = Resources.Load<TextAsset>(path);
                //  Log.Info($"After Load Resource FGUI desc asset from path:'{path}':{Time.frameCount}");
                if(asset != null){
                    AddResourcePackage(uiConfig.PackageName, asset);
                    CreateUI(uiLogic, uiConfig, userData);
                    // Log.Info($"Load Resource FGUI desc asset from '{uiConfig.PackageName}.{uiConfig.ComponentName}' Suceess.");
                }else{
                    Log.Error($"Can not load Resource FGUI desc asset from '{uiConfig.PackageName}.{uiConfig.ComponentName}'.");
                }
            }
            else
            {
                CreateUI(uiLogic, uiConfig, userData);
                // Log.Error($"Load Resource FGUI desc asset from '{uiConfig.PackageName}.{uiConfig.ComponentName}' Cache Suceess.");
            }
        }

        private void AddResourcePackage(string packageName, TextAsset descAsset)
        {
            UIPackage.LoadResource loadFromResourcesPath = (string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
            {
               
                string fullName;
                if(extension == ".png"){
                    fullName = Utility.Text.Format("{0}_{1}", packageName, name);
                }else{
                    fullName = Utility.Text.Format("{0}_{1}{2}", packageName, name, extension);
                }

                destroyMethod = DestroyMethod.Unload;
                var path = AssetUtility.GetFGUIResourceAsset(packageName, fullName);
                //  Debug.Log($"Load Data:{path} type:{type}");
                return Resources.Load(path, type);
            };

            var packageInfo = GetPackageInfo(packageName);
            if (packageInfo.Package == null)
            {
                packageInfo.Package = UIPackage.AddPackage(descAsset.bytes, string.Empty, loadFromResourcesPath);
                packageInfo.Package.LoadAllAssets(); //AddPackage时即通知包加载所有资源，最小程度避免异步加载引起的延迟显示
            }
        }




 

    }


#endif

}
