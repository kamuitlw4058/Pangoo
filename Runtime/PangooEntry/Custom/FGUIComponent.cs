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
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;


namespace Pangoo
{

    /// <summary>
    /// FGUI组件。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed partial class FGUIComponent : GameFrameworkComponent
    {

        /// <summary> 界面实例ID，递增 </summary>
        private int m_InstanceId;

        /// <summary> 当前激活的所有UI实例，包含加载中的UI </summary>
        [ShowInInspector]
        private GameFrameworkLinkedList<UILogicBase> m_UIInstanceList;

        /// <summary> 加载中的UI实例 </summary>
        private List<UILogicBase> m_LoadingUIList;

        /// <summary> 加载完成后立即关闭的UI实例 </summary>
        private HashSet<UILogicBase> m_CloseAfterLoadUIList;

        /// <summary> UI实例对象池 </summary>
        private IObjectPool<UILogicBase> m_UIInstanceObjectPool;

        /// <summary> 等待预加载的数量 </summary>
        private int m_WaitLoadCount;


        public bool IsFinishPreLoaded;

        /// <summary> 持有UI配置表引用 </summary>

        public UiConfigInfoTable m_UiConfigTable;

        /// <summary> 异形屏缩进比例，范围为[0,0.1] </summary>
        private float m_NotchRate;

        /// <summary> UI事件池 </summary>
        // private UIEventHandlerPool m_EventHandlerPool;

        /// <summary> FGUI包信息扩展管理 </summary>
        private Dictionary<string, FGUIPackageInfo> m_FGUIPackageInfoDic;

        [ShowInInspector]
        private List<string> m_UIStack;

        private const string NameSpacePrefix = "Pangoo.";

        SettingComponent Setting;
        ResourceComponent Resource;

        private UIComponent m_UIComponent;

        private ObjectPoolComponent m_ObjectPoolComponent;
        private GameConfigComponent m_GameConfigComponent;


        public float InstanceAutoReleaseInterval = 60;
        public int InstanceCapacity = 16;
        public float InstanceExpireTime = 60;
        public int InstancePriority;

        public void SetUiConfigTable(UiConfigInfoTable table)
        {
            m_UiConfigTable = table;
        }



        protected override void Awake()
        {
            base.Awake();



        }

        void Start()
        {
            m_UIInstanceList = new GameFrameworkLinkedList<UILogicBase>();
            m_LoadingUIList = new List<UILogicBase>();
            m_CloseAfterLoadUIList = new HashSet<UILogicBase>();
            // m_EventHandlerPool = new UIEventHandlerPool();
            m_FGUIPackageInfoDic = new Dictionary<string, FGUIPackageInfo>();
            m_UIStack = new List<string>();

            m_UIComponent = GameEntry.GetComponent<UIComponent>();
            m_ObjectPoolComponent = GameEntry.GetComponent<ObjectPoolComponent>();
            m_GameConfigComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<GameConfigComponent>();


            m_UIInstanceObjectPool = m_ObjectPoolComponent.CreateSingleSpawnObjectPool<UILogicBase>("FGUIInstance", InstanceAutoReleaseInterval, InstanceCapacity, InstanceExpireTime, InstancePriority);

            Setting = UnityGameFramework.Runtime.GameEntry.GetComponent<SettingComponent>();
            Resource = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();

            Initialize();
        }

        private void Initialize()
        {
            // SetNotchRate(Setting.GetFloat(Constant.Setting.NotchRate, 0.075f));
            GRoot.inst.onSizeChanged.Add(ResetAllAdaptation);
        }

        private void ResetAllAdaptation()
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                var component = uiLogic.GetSelfComponent();
                if (component == null)
                    continue;

                var uiConfig = uiLogic.UiConfig;
                SetAdaptation(component, uiConfig);
                uiLogic.OnAdaptation();
            }
        }

        private void SetAdaptation(GComponent component, UiConfigInfoTable.UiConfigInfoRow uiConfig)
        {
            component.MakeFullScreen();
            //if (uiConfig.IgnoreNotch || GRoot.inst.width / GRoot.inst.height <= 2.0f)
            //{
            //    //纵横比不超过18比9，或该界面是一个需要模糊背景的弹窗，或设置了忽略刘海适配，则直接设为全屏
            //    component.MakeFullScreen();
            //}
            //else
            //{
            //    //若逻辑屏幕纵横比大于18比9则认为是全面屏，宽度减少自定义刘海的宽度
            //     //component.SetSize(GRoot.inst.width * (1 - m_NotchRate), GRoot.inst.height);
            //}

            component.x = (GRoot.inst.width - component.width) / 2;
        }

        public void CloseUI(UILogicBase uiLogic, bool isClear = false)
        {
            InternalCloseUI(uiLogic, isClear);
        }

        /// <summary> 通过界面实例类型关闭界面（泛型） </summary>
        public void CloseUI<T>() where T : UILogicBase
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic is T)
                {
                    CloseUI(uiLogic);
                    return;
                }
            }

            Log.Error($"尝试关闭的界面实例不存在或已经处于未激活状态:{typeof(T).Name}");
        }

        /// <summary> 通过界面实例类型关闭界面（Type） </summary>
        public void CloseUI(Type type, bool isClear = false)
        {
            foreach (var uiLogic in m_UIInstanceList)
            {
                if (uiLogic.GetType() == type)
                {
                    CloseUI(uiLogic, isClear);
                    return;
                }
            }

            Log.Error($"尝试关闭的界面实例不存在或已经处于未激活状态:{type.Name}");
        }


        private void InternalCloseUI(UILogicBase uiLogic, bool isClear)
        {
            if (uiLogic != null)
            {
                if (uiLogic.UiConfig.NeverClose)
                    return;

                if (m_LoadingUIList.Contains(uiLogic))
                {
                    //该UI还在加载中，将其移入完成后关闭的列表中
                    m_CloseAfterLoadUIList.Add(uiLogic);
                    return;
                }
                else
                {
                    //隐藏界面，从激活列表中移除，设置对象池状态为可回收
                    uiLogic.Hide();
                    m_UIInstanceObjectPool.Unspawn(uiLogic);

                    if (m_UIInstanceList.Remove(uiLogic))
                    {
                        HandleStackPop(uiLogic, isClear);
                        return;
                    }
                }
            }

            Log.Error("尝试关闭的界面实例不存在或已经处于未激活状态");
        }


        public void PreLoadPackage(string packageName)
        {
            m_WaitLoadCount++;

            Resource.LoadAsset(AssetUtility.GetFGUIPackage(packageName), 0, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    AddPackage(packageName, (TextAsset)asset);
                    AddPackageRef(packageName);

                    //若全部加载完成，则执行完成回调
                    m_WaitLoadCount--;
                    if (m_WaitLoadCount == 0)
                    {
                        IsFinishPreLoaded = true;
                    }
                },
                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load FGUI desc asset from '{0}' with error message '{1}'.", assetName, errorMessage);
                }
            ));
        }

        private void AddPackage(string packageName, TextAsset descAsset)
        {
            var packageInfo = GetPackageInfo(packageName);
            if (packageInfo.Package == null)
            {
                packageInfo.Package = UIPackage.AddPackage(descAsset.bytes, string.Empty, LoadAsync);
                packageInfo.Package.LoadAllAssets(); //AddPackage时即通知包加载所有资源，最小程度避免异步加载引起的延迟显示
            }

            Resource.UnloadAsset(descAsset);
        }


        /// <summary> FGUI需要资源时采用的异步加载回调 </summary>
        private void LoadAsync(string name, string extension, Type type, PackageItem item)
        {
            var packageName = item.owner.name;
            string fullName;
            // if (type == typeof(SkeletonDataAsset))
            //     fullName = Utility.Text.Format("{0}{1}", name, extension);
            // else
            //     fullName = Utility.Text.Format("{0}_{1}{2}", packageName, name, extension);

            fullName = Utility.Text.Format("{0}_{1}{2}", packageName, name, extension);

            Resource.LoadAsset(AssetUtility.GetFGUIAsset(packageName, fullName), 0, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    try
                    {
                        //为fgui packageItem设置资源
                        item.owner.SetItemAsset(item, asset, DestroyMethod.Custom);
                    }
                    catch (Exception e)
                    {
                        Log.Error("加载FGUI资源失败 '{0}' error message '{1}'.", assetName, e);
                    }

                    //管理加载，当一个包所需要的资源都加载完毕，把其中正在等待的界面添加到舞台上
                    var packageInfo = GetPackageInfo(packageName);
                    packageInfo.LoadingResCount--;
                    if (packageInfo.LoadingResCount == 0)
                    {
                        foreach (var loadInfo in packageInfo.WaitList)
                        {
                            OpenUIFinally(loadInfo.UILogic, loadInfo.UserData, false);
                        }

                        packageInfo.WaitList.Clear();
                    }
                },
                (assetName, status, errorMessage, userData) => { Log.Error("加载FGUI资源失败 '{0}' error message '{1}'.", assetName, errorMessage); }
            ));

            GetPackageInfo(packageName).LoadingResCount++;
        }


        private int InternalOpenUI<T>(UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData) where T : UILogicBase, new()
        {
            return InternalOpenUI(typeof(T), uiConfig, userData);
        }

        private int InternalOpenUI(Type type, UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData)
        {
            UILogicBase uiLogic = m_UIInstanceObjectPool.Spawn(type.Name);
            if (uiLogic == null)
            {
                uiLogic = Activator.CreateInstance(type) as UILogicBase;
                if (uiLogic == null)
                    throw new Exception($"未找到{type.Name}类型的UI逻辑");

                uiLogic.CreateLogic(uiConfig); //构造函数，设置配置和初始化对象池属性
                m_UIInstanceObjectPool.Register(uiLogic, true); //注册到对象池，并标记为已使用
                LoadUI(uiLogic, uiConfig, userData); //开始加载界面资源
            }
            else
            {
                SetAdaptation(uiLogic.GetSelfComponent(), uiConfig);
                OpenUIFinally(uiLogic, userData, true);
            }

            m_InstanceId++;
            uiLogic.InstanceId = m_InstanceId;

            m_UIInstanceList.AddLast(uiLogic);

            return m_InstanceId;
        }


        private void OpenUIFinally(UILogicBase uiLogic, object userData, bool reuse)
        {
            m_LoadingUIList.Remove(uiLogic);
            uiLogic.HandleBlurMask(reuse);
            HandleStackPush(uiLogic);

            if (m_CloseAfterLoadUIList.Contains(uiLogic))
            {
                //在载入过程中被关闭
                m_CloseAfterLoadUIList.Remove(uiLogic);
                CloseUI(uiLogic);
                return;
            }
            // else if (uiLogic.GetType() == m_JumpTarget)
            // {
            //     //该界面是当前跳转的目标，执行跳转相关处理
            //     //ClearStackButTarget(m_JumpTable.GotoUi);
            //     this.JumpClearUI(m_JumpTable.GotoUi);
            //     InsertStack(m_JumpTable.Path.Split('|').ToList());

            //     m_JumpTable = null;
            //     m_JumpTarget = null;
            // }

            //首次打开界面或从对象池复用
            if (reuse)
                uiLogic.Reuse(userData);
            else
                uiLogic.Init(userData);
        }


        //加载流程：
        //先检查包实例是否存在，若存在则直接创建UI
        //若不存在则异步加载UI描述文件asset，随后通知FGUI根据描述文件AddPackage，完成后直接unload描述文件asset，并创建UI
        //AddPackage时传入异步加载资源的方法，FGUI需要资源时会调用该方法，异步加载资源
        //每个资源异步加载完成后，调用SetItemAsset通知UIPackage获得真正的资源并Reload
        //单个包需要用到的资源都加载完毕后，才把该包的UI真正添加到舞台上
        //*同时创建多个来自同一个包的界面时，可能会多次加载包描述文件，但由GF的资源对象池管理，并不重复加载，只是增减引用计数
        //*UIManager持有包引用的计数，和包实例的管理，当已存在包实例时，不重复调用AddPackage
        //*当界面实例对象被对象池释放时，会通知UIManager为包计数减1
        //*当包计数为0时，会移除包实例引用并通知FGUI销毁包
        //*当包被销毁时，包用到的资源经由自定义销毁回调进行UnloadAsset，同样由GF资源对象池对其真实引用数进行管理
        /// <summary> 异步加载描述文件并创建界面 </summary>
        private void LoadUI(UILogicBase uiLogic, UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData)
        {
            m_LoadingUIList.Add(uiLogic);
            var uiPackage = GetPackageInfo(uiConfig.PackageName).Package;
            if (uiPackage == null)
            {
                Resource.LoadAsset(AssetUtility.GetFGUIPackage(uiConfig.PackageName), 0, new LoadAssetCallbacks(
                    (assetName, asset, duration, assetUserData) =>
                    {
                        AddPackage(uiConfig.PackageName, (TextAsset)asset);
                        CreateUI(uiLogic, uiConfig, userData);
                    },
                    (assetName, status, errorMessage, assetUserData) =>
                    {
                        Log.Error("Can not load FGUI desc asset from '{0}' with error message '{1}'.", assetName, errorMessage);
                    }
                ));
            }
            else
            {
                CreateUI(uiLogic, uiConfig, userData);
            }
        }



        private void CreateUI(UILogicBase uiLogic, UiConfigInfoTable.UiConfigInfoRow uiConfig, object userData)
        {
            var packageName = uiConfig.PackageName;
            AddPackageRef(packageName);

            var component = (GComponent)UIPackage.CreateObject(packageName, uiConfig.ComponentName);

            SetAdaptation(component, uiConfig);

            component.AddRelation(GRoot.inst, RelationType.Size); //使分辨率动态变化时仍然能适应
            component.sortingOrder = uiConfig.SortingOrder; //设置sortingOrder，仅用于需要固定在上层的界面，一般界面都应为0
            component.fairyBatching = true; //设置默认为fairyBatching合批，如需要可以在界面中手动取消
            component.name = component.gameObjectName; //为界面名字赋值
            uiLogic.SetSelfComponent(component); //component引用绑定到逻辑类中

            //检查该ui所在包是否仍在加载资源，还在加载中的，等到加载完毕后才把UI添加到舞台。加载已经完毕的则立即添加到舞台
            var packageInfo = GetPackageInfo(packageName);
            if (packageInfo.LoadingResCount == 0)
            {
                OpenUIFinally(uiLogic, userData, false);
            }
            else
            {
                packageInfo.WaitList.Add(new WaitInfo
                {
                    UILogic = uiLogic,
                    UserData = userData,
                });
            }
        }

        private void HandleStackPush(UILogicBase uiLogic)
        {
            //忽略堆栈则不做堆栈相关处理
            if (uiLogic.UiConfig.IgnoreStack)
                return;

            if (m_UIStack.Count >= 1)
            {
                //堆栈内存在上个界面，将其pause
                var lastName = m_UIStack.Last();
                var lastClassName = m_GameConfigComponent.GetGameMainConfig().MainNamespace + "." + lastName;
                Type type;
                if (MainsapceGetType != null)
                {
                    type = MainsapceGetType(lastClassName);
                }
                else
                {
                    type = Type.GetType(lastClassName);
                }
                var stackTopLogic = GetUI(type);
                if (stackTopLogic != null)
                {
                    //根据配置选项决定是否隐藏被pause的界面
                    if (uiLogic.UiConfig.HidePause)
                        stackTopLogic.GetSelfComponent().visible = false;
                    stackTopLogic.OnPause();
                }
                else
                {
                    Log.Info($"FGUI: GetLastTypeNull:{m_UIStack.Last()},lastClassName:{lastClassName}");
                }
            }

            m_UIStack.Add(uiLogic.Name);
        }

        public delegate Type GetTypeFunc(string name);

        public GetTypeFunc MainsapceGetType;



        private void HandleStackPop(UILogicBase uiLogic, bool isClear)
        {
            //忽略堆栈则不做堆栈相关处理
            if (uiLogic.UiConfig.IgnoreStack || isClear)
                return;

            //检查当前关闭的是否为栈顶界面
            if (m_UIStack.Last() == uiLogic.Name)
            {
                m_UIStack.Remove(uiLogic.Name);
                if (m_UIStack.Count >= 1)
                {
                    var lastName = m_UIStack.Last();
                    var lastClassName = m_GameConfigComponent.GetGameMainConfig().MainNamespace + "." + lastName;
                    Type type;
                    if (MainsapceGetType != null)
                    {
                        type = MainsapceGetType(lastClassName);
                    }
                    else
                    {
                        type = Type.GetType(lastClassName);
                    }
                    var stackTopLogic = GetUI(type);
                    if (stackTopLogic != null)
                    {
                        //永远会将栈顶的界面设置成可见，不论自己是否将上级隐藏。因为可能出现穿插关闭的情况
                        stackTopLogic.GetSelfComponent().visible = true;
                        stackTopLogic.OnResume();
                    }
                    else
                    {
                        //堆栈内的上个界面不可用，将其重新创建，并移除多余的记录
                        m_UIStack.Remove(lastName);
                        OpenUI(type);
                    }
                }
            }
            else
                m_UIStack.Remove(uiLogic.Name);
        }




        public void AddPackageRef(string packageName)
        {
            GetPackageInfo(packageName).RefCount++;
        }


        public void RemovePackageRef(string packageName)
        {
            var packageInfo = GetPackageInfo(packageName);
            packageInfo.RefCount--;
            if (packageInfo.RefCount == 0)
            {
                //计数为0时，移除包实例的引用，调用FGUI接口销毁包
                if (UIPackage.GetByName(packageName) != null)
                {
                    UIPackage.RemovePackage(packageName);
                    packageInfo.Package = null;
                }
                else
                    Log.Error($"尝试移除FGUI包失败，已经不存在包 packageName:{packageName}");
            }
        }


        public FGUIPackageInfo GetPackageInfo(string packageName)
        {
            m_FGUIPackageInfoDic.TryGetValue(packageName, out var packageInfo);

            if (packageInfo == null)
            {
                packageInfo = new FGUIPackageInfo();
                packageInfo.WaitList = new List<WaitInfo>();
                m_FGUIPackageInfoDic[packageName] = packageInfo;
            }

            return packageInfo;
        }



        public void ShutDown()
        {
            //卸载FGUI
            GRoot.inst.onSizeChanged.Clear();
            UIPackage.RemoveAllPackages();
        }
    }

    public class FGUIPackageInfo
    {
        public UIPackage Package;

        public int RefCount;

        public int LoadingResCount;

        public List<WaitInfo> WaitList;
    }

    public class WaitInfo
    {
        public UILogicBase UILogic;

        public object UserData;
    }
}
