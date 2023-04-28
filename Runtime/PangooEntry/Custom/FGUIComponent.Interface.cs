//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using GameFramework;
using GameFramework.ObjectPool;
using GameFramework.Resource;
using GameFramework.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;


namespace Pangoo
{

    /// <summary>
    /// FGUI组件。
    /// </summary>
    public sealed partial class FGUIComponent
    {

        public T GetUI<T>(int instanceId) where T : UILogicBase
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic.InstanceId == instanceId)
                {
                    return uiLogic as T;
                }
            }

            Log.Error($"未找到要获取的界面实例id:{instanceId}");
            return null;
        }

        /// <summary> 通过界面实例类型获取实例（泛型） </summary>
        public T GetUI<T>() where T : UILogicBase
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic is T logic)
                {
                    return logic;
                }
            }

            return null;
        }

        /// <summary> 通过界面实例类型获取实例（Type） </summary>
        public UILogicBase GetUI(Type type)
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic.GetType() == type)
                {
                    return uiLogic;
                }
            }

            return null;
        }


        public bool HasUI(int instanceId)
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic.InstanceId == instanceId)
                {
                    return true;
                }
            }

            return false;
        }
        // 
        /// <summary> 判断是否存在指定类型的界面（泛型） </summary>
        public bool HasUI<T>() where T : UILogicBase
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic is T)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary> 判断是否存在指定类型的界面（Type） </summary>
        public bool HasUI(Type type)
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsLoadingUI<T>()
        {
            foreach (var uiLogic in m_LoadingUIList)
            {
                if (uiLogic is T)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary> 判断指定类型的界面是否正在加载中（Type） </summary>
        public bool IsLoadingUI(Type type)
        {
            foreach (var uiLogic in m_LoadingUIList)
            {
                if (uiLogic.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }



        public void OpenUI<T>(object userData = null) where T : UILogicBase, new()
        {
            var uiConfig = m_UiConfigTable.GetConfigByType<T>();
            if (uiConfig == null)
            {
                Log.Error($"{typeof(T).Name} 不存在于 m_UiConfigTable！");
                return;
            }
            if (HasUI<T>())
            {
                if (IsLoadingUI<T>())
                {
                    //界面正在加载中，属于少数情况下的正常逻辑，直接返回
                    return;
                }
                else
                {
                    Log.Error($"{typeof(T).Name}界面已存在，不能重复打开");
                    CloseUI<T>();
                }
            }

            InternalOpenUI<T>(uiConfig, userData);
        }

        public void OpenUI(Type type, object userData = null)
        {
            var uiConfig = m_UiConfigTable.GetConfigByType(type.Name);
            if (uiConfig == null)
            {
                Log.Error($"{type.Name} 不存在于 m_UiConfigTable！");
                return;
            }

            if (HasUI(type))
            {
                if (IsLoadingUI(type))
                {
                    return;
                }
                else
                {
                    Log.Error($"{type.Name}界面已存在，不能重复打开");
                    CloseUI(type);
                }
            }

            InternalOpenUI(type, uiConfig, userData);
        }


        /// <summary> 直接传入参数异步打开界面，应用在打开无配置的界面，不应常规使用 </summary>
        public void OpenUI<T>(UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData = null) where T : UILogicBase, new()
        {
            InternalOpenUI<T>(uiConfig, userData);
        }


    }

}
