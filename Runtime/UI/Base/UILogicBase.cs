using System;
using System.Collections.Generic;
#if ENABLE_FGUI
using FairyGUI;
#endif
using GameFramework.ObjectPool;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;

namespace Pangoo
{
    #if ENABLE_FGUI
    [Serializable]
    public class UILogicBase : ObjectBase
    {
        /// <summary> GComponent根节点，在子类中转换为真实类型使用 </summary>
        private GComponent m_SelfComponent;

        /// <summary> 是否作为子组件被创建 </summary>
        private bool m_IsChildLogic;

        private readonly List<UILogicBase> m_ChildLogicList = new List<UILogicBase>();

        /// <summary> 界面配置，作为子组件时不存在 </summary>
        public UiConfigInfoTable.UiConfigInfoRow UiConfig { get; set; }

        /// <summary> 界面实例id，作为子组件时不存在 </summary>
        public int InstanceId { get; set; }

        /// <summary> 当前界面逻辑类是否处于激活状态 </summary>
        public bool IsActive { get; set; }


        public bool IsTweening { get; set; }

        /// <summary> 父组件的逻辑实例，仅作为子组件时拥有 </summary>
        public UILogicBase ParentLogic;


        public FGUIComponent EntryComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<FGUIComponent>();

        public GComponent GetSelfComponent()
        {
            return m_SelfComponent;
        }

        public void SetSelfComponent(GComponent component)
        {
            m_SelfComponent = component;
            component.Logic = this;
        }

        protected virtual void BindAll()
        {
        }

        /// <summary> 绑定时为子组件自动生成逻辑实例，使组件拥有生命周期 </summary>
        protected T CreateChildLogic<T>(int index) where T : UILogicBase, new()
        {
            var self = (GComponent)m_SelfComponent.GetChildAt(index);
            return CreateChildLogic<T>(self, false);
        }

        /// <summary>
        /// 手动为子组件生成逻辑实例，使其之后能继承父组件的生命周期，callInit决定是否立即调用它的init和open周期。
        /// 若子组件是动态创建销毁的，销毁时必须配套调用RemoveChildLogic
        /// </summary>
        protected T CreateChildLogic<T>(GComponent self, bool callInit = true) where T : UILogicBase, new()
        {
            var logic = CreateSingleLogic<T>(self);
            m_ChildLogicList.Add(logic);
            if (callInit)
                logic.Init(null);

            return logic;
        }

        /// <summary> 从子组件列表中移除实例，并调用其关闭生命周期，并不移除或销毁子组件的Component </summary>
        protected void RemoveChildLogic<T>(T childLogic) where T : UILogicBase
        {
            m_ChildLogicList.Remove(childLogic);
            childLogic.Hide();
        }

        /// <summary> 为一个组件绑定逻辑实例，并不添加为子组件，因此不会有生命周期 </summary>
        protected T CreateSingleLogic<T>(GComponent self) where T : UILogicBase, new()
        {
            if (self.Logic is T logic)
                return logic;

            logic = new T();
            logic.m_SelfComponent = self;
            logic.m_IsChildLogic = true;
            logic.BindAll();
            self.Logic = logic;

            return logic;
        }


        #region 提供给管理器使用的方法

        /// <summary> 创建界面逻辑对象，必须经过管理器 </summary>
        public void CreateLogic(UiConfigInfoTable.UiConfigInfoRow uiConfig)
        {
            UiConfig = uiConfig;
            Initialize(GetType().Name, this); //初始化对象池所需属性
        }

        /// <summary> 初始化界面，必须经过管理器 </summary>
        public void Init(object userData)
        {
            if (IsActive)
                return;
            IsActive = true;
            IsTweening = false;

            //子组件不添加到舞台，子组件在生成逻辑实例时绑定
            if (!m_IsChildLogic)
            {
                GRoot.inst.AddChild(m_SelfComponent);
                BindAll();
            }

            try
            {
                //通知界面初始化
                OnInit(userData);

                for (var i = 0; i < m_ChildLogicList.Count; i++)
                {
                    m_ChildLogicList[i].ParentLogic = this;
                    m_ChildLogicList[i].Init(userData);
                }
                //通知界面打开
                OnOpen(userData);
            }
            catch (Exception ex)
            {
                Log.Error($"{GetType().Name}打开时出现报错:\nMessage:\n{ex}\nStackTrace:\n{ex.Data["StackTrace"]}");
            }

            if (!m_IsChildLogic)
            {
                //需延迟一帧通知，因打开缓存界面时，在点击事件内就会执行该通知
                //而点击引导的步骤前进需要等到点击事件完成后
                //TODO: 事件通知
                // HotfixEntry.UI.FireEventDelay(UIEventId.UIOpen, this);
            }
        }

        /// <summary> 关闭界面，必须经过管理器 </summary>
        public void Hide()
        {
            if (!IsActive)
                return;
            IsActive = false;

            //子组件的显示隐藏不会被改变
            if (!m_IsChildLogic)
                m_SelfComponent.RemoveFromParent();

            for (var i = 0; i < m_ChildLogicList.Count; i++)
            {
                m_ChildLogicList[i].Hide();
            }

            try
            {
                //通知界面关闭
                OnClose();
            }
            catch (Exception ex)
            {
                Log.Error($"{GetType().Name}关闭时出现报错:\nMessage:\n{ex}\nStackTrace:\n{ex.Data["StackTrace"]}");
            }
        }

        /// <summary> 复用界面，必须经过管理器 </summary>
        public void Reuse(object userData)
        {
            if (IsActive)
                return;
            IsActive = true;

            //子组件不添加到舞台，子组件的显示隐藏不会被改变
            if (!m_IsChildLogic)
            {
                GRoot.inst.AddChild(m_SelfComponent);
                m_SelfComponent.visible = true;
            }

            try
            {
                //通知界面打开
                OnOpen(userData);
            }
            catch (Exception ex)
            {
                Log.Error($"{GetType().Name}打开时出现报错:\nMessage:\n{ex}\nStackTrace:\n{ex.Data["StackTrace"]}");
            }

            for (var i = 0; i < m_ChildLogicList.Count; i++)
            {
                m_ChildLogicList[i].Reuse(userData);
            }

            if (!m_IsChildLogic)
            {
                //TODO:事件通知
                // HotfixEntry.UI.FireEventDelay(UIEventId.UIOpen, this);
            }

        }

        public void HandleBlurMask(bool reuse)
        {
            if (UiConfig.BlurMask)
            {
                if (!reuse)
                {
                    //设置模糊背景
                    var mask = (GComponent)m_SelfComponent.GetChild("Mask");
                    if (mask == null)
                    {
                        Log.Error($"{GetType().Name} 被设置为需要模糊背景，但是未找到Mask组件!");
                        return;
                    }

                    //mask统一关联
                    mask.relations.ClearAll();
                    mask.AddRelation(m_SelfComponent, RelationType.Center_Center);
                    mask.AddRelation(m_SelfComponent, RelationType.Height);
                    mask.AddRelation(GRoot.inst, RelationType.Width);
                    var holder = (GLoader)mask.GetChildAt(0);
                    // var texture2D = GRoot.inst.displayObject.GetScreenShot(null, 1);
                    //TODO: 模糊背景
                    // var texture2D = GameEntry.Camera.GetScreenTexture();
                    // holder.texture = new NTexture(GaussianBlurFilter.RenderOnce(texture2D));
                }
                else
                {
                    //更新模糊背景
                    var mask = (GComponent)m_SelfComponent.GetChild("Mask");
                    var holder = (GLoader)mask.GetChildAt(0);
                    holder.texture.Dispose();
                    holder.texture = null;
                    // var texture2D = GRoot.inst.displayObject.GetScreenShot(null, 1);
                    //TODO: 模糊背景
                    // var texture2D = GameEntry.Camera.GetScreenTexture();
                    // holder.texture = new NTexture(GaussianBlurFilter.RenderOnce(texture2D));
                }
            }
        }

        #endregion

        #region 便捷接口

        /// <summary> 关闭本界面 </summary>
        [Button("关闭")]
        public void CloseSelf()
        {
            if (!IsActive)
                return;
            EntryComponent.CloseUI(this);
        }

        /// <summary> 打开界面 </summary>
        public void OpenUI<T>(object userData = null) where T : UILogicBase, new()
        {
            EntryComponent.OpenUI<T>(userData);
        }

        #endregion

        #region 生命周期

        /// <summary> 界面被创建时执行，从池中复用时不会执行 </summary>
        protected virtual void OnInit(object userData)
        {
        }

        /// <summary> 界面每次被打开时执行 </summary>
        protected virtual void OnOpen(object userData)
        {

        }

        /// <summary> 界面每次被关闭时执行 </summary>
        protected virtual void OnClose()
        {

        }

        /// <summary> 界面被销毁时执行，关闭时并不会立即销毁而是入池。注意子组件没有OnRecycle生命周期 </summary>
        protected virtual void OnRecycle()
        {
        }

        /// <summary> 界面处于打开状态下，屏幕分辨率发生变化时执行 </summary>
        public virtual void OnAdaptation()
        {
        }

        /// <summary> 当堆栈有新的最顶层界面时，原先的最顶层界面会调用 </summary>
        public virtual void OnPause()
        {
        }

        /// <summary> 当该界面恢复到堆栈最顶层时调用 </summary>
        public virtual void OnResume()
        {
        }

        #endregion

        #region 对象池周期

        protected override void OnSpawn()
        {
        }

        protected override void OnUnspawn()
        {
        }

        protected override void Release(bool isShutdown)
        {
            //通知界面被回收
            OnRecycle();

            if (!m_IsChildLogic)
            {
                if (UiConfig.BlurMask)
                {
                    //销毁模糊背景
                    var mask = (GComponent)m_SelfComponent.GetChild("Mask");
                    var holder = (GLoader)mask.GetChildAt(0);
                    holder.texture.Dispose();
                    holder.texture = null;
                }
            }

            m_SelfComponent.Dispose();
            if (!isShutdown)
            {
                EntryComponent.RemovePackageRef(UiConfig.PackageName);
            }


        }

        /// <summary> 引用池清理。跨域继承适配中重载的函数，必须在热更中有实现，即使是单纯的调用base版本 </summary>
        public override void Clear()
        {
            base.Clear();
        }

        #endregion
    }
    #endif
}