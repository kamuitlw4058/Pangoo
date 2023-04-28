// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using FairyGUI;
// using UnityGameFramework.Runtime;

// namespace SteamClient.Hotfix
// {
//     public partial class UIManager
//     {
//         /// <summary> 异步打开界面 </summary>
//         /// <param name="userData">自定义传递给要打开界面的数据</param>
//         public void OpenUI<T>(object userData = null) where T : UILogicBase, new()
//         {
//             var uiConfig = m_UiConfigTable.GetConfigByType<T>();

//             if (HasUI<T>())
//             {
//                 if (IsLoadingUI<T>())
//                 {
//                     //界面正在加载中，属于少数情况下的正常逻辑，直接返回
//                     return;
//                 }
//                 else
//                 {
//                     Log.Error($"{typeof(T).Name}界面已存在，不能重复打开");
//                     CloseUI<T>();
//                 }
//             }

//             InternalOpenUI<T>(uiConfig, userData);
//         }

//         public void OpenUI(Type type, object userData = null)
//         {
//             var uiConfig = m_UiConfigTable.GetConfigByType(type.Name);

//             if (HasUI(type))
//             {
//                 if (IsLoadingUI(type))
//                 {
//                     return;
//                 }
//                 else
//                 {
//                     Log.Error($"{type.Name}界面已存在，不能重复打开");
//                     CloseUI(type);
//                 }
//             }

//             InternalOpenUI(type, uiConfig, userData);
//         }


//         /// <summary> 直接传入参数异步打开界面，应用在打开无配置的界面，不应常规使用 </summary>
//         public void OpenUI<T>(UiConfigTable.ConfigTable uiConfig, object userData = null) where T : UILogicBase, new()
//         {
//             InternalOpenUI<T>(uiConfig, userData);
//         }

//         /// <summary> 通过界面实例id查找实例使其关闭 </summary>
//         public void CloseUI(int instanceId)
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic.InstanceId == instanceId)
//                 {
//                     CloseUI(uiLogic);
//                     return;
//                 }
//             }

//             Log.Error($"未找到要关闭的界面实例id:{instanceId}");
//         }

//         /// <summary> 通过直接指定界面实例使其关闭 </summary>
//         public void CloseUI(UILogicBase uiLogic, bool isClear = false)
//         {
//             InternalCloseUI(uiLogic, isClear);
//         }

//         /// <summary> 通过界面实例类型关闭界面（泛型） </summary>
//         public void CloseUI<T>() where T : UILogicBase
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic is T)
//                 {
//                     CloseUI(uiLogic);
//                     return;
//                 }
//             }

//             Log.Error($"尝试关闭的界面实例不存在或已经处于未激活状态:{typeof(T).Name}");
//         }

//         /// <summary> 通过界面实例类型关闭界面（Type） </summary>
//         public void CloseUI(Type type, bool isClear = false)
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic.GetType() == type)
//                 {
//                     CloseUI(uiLogic, isClear);
//                     return;
//                 }
//             }

//             Log.Error($"尝试关闭的界面实例不存在或已经处于未激活状态:{type.Name}");
//         }

//         /// <summary> 关闭所有界面并清理堆栈，与清空堆栈的不同点在于能关闭那些不会入栈的界面 </summary>
//         public void CloseAllUI()
//         {
//             ClearStack();

//             var tempList = new List<UILogicBase>();
//             tempList.AddRange(m_UIInstanceList);
//             foreach (var uiLogic in tempList)
//             {
//                 CloseUI(uiLogic, true);
//             }
//         }

//         /// <summary> 通过界面实例id获取实例 </summary>
//         public T GetUI<T>(int instanceId) where T : UILogicBase
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic.InstanceId == instanceId)
//                 {
//                     return uiLogic as T;
//                 }
//             }

//             Log.Error($"未找到要获取的界面实例id:{instanceId}");
//             return null;
//         }

//         /// <summary> 通过界面实例类型获取实例（泛型） </summary>
//         public T GetUI<T>() where T : UILogicBase
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic is T logic)
//                 {
//                     return logic;
//                 }
//             }

//             return null;
//         }

//         /// <summary> 通过界面实例类型获取实例（Type） </summary>
//         public UILogicBase GetUI(Type type)
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic.GetType() == type)
//                 {
//                     return uiLogic;
//                 }
//             }

//             return null;
//         }

//         /// <summary> 判断是否存在指定ID的界面 </summary>
//         public bool HasUI(int instanceId)
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic.InstanceId == instanceId)
//                 {
//                     return true;
//                 }
//             }

//             return false;
//         }
// // 
//         /// <summary> 判断是否存在指定类型的界面（泛型） </summary>
//         public bool HasUI<T>() where T : UILogicBase
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic is T)
//                 {
//                     return true;
//                 }
//             }

//             return false;
//         }

//         /// <summary> 判断是否存在指定类型的界面（Type） </summary>
//         public bool HasUI(Type type)
//         {
//             foreach (var uiLogic in m_UIInstanceList)
//             {
//                 if (uiLogic.GetType() == type)
//                 {
//                     return true;
//                 }
//             }

//             return false;
//         }

//         /// <summary> 判断指定类型的界面是否正在加载中（泛型） </summary>
//         public bool IsLoadingUI<T>()
//         {
//             foreach (var uiLogic in m_LoadingUIList)
//             {
//                 if (uiLogic is T)
//                 {
//                     return true;
//                 }
//             }

//             return false;
//         }

//         /// <summary> 判断指定类型的界面是否正在加载中（Type） </summary>
//         public bool IsLoadingUI(Type type)
//         {
//             foreach (var uiLogic in m_LoadingUIList)
//             {
//                 if (uiLogic.GetType() == type)
//                 {
//                     return true;
//                 }
//             }

//             return false;
//         }

//         /// <summary> 关闭堆栈顶部的UI </summary>
//         public void CloseStackTop()
//         {
//             if (m_UIStack.Count <= 1)
//             {
//                 Log.Error("已无上层界面，无法返回");
//                 return;
//             }
//             var type = Type.GetType(NameSpacePrefix + m_UIStack.Last());
//             CloseUI(type);
//         }

//         /// <summary> 关闭堆栈内所有UI并清空堆栈 </summary>
//         public void ClearStack()
//         {
//             CloseAllStackLoadingUI();

//             var tempStack = new List<string>();
//             tempStack.AddRange(m_UIStack);
//             foreach (var typeName in tempStack)
//             {
//                 var type = Type.GetType(NameSpacePrefix + typeName);
//                 if (HasUI(type))
//                     CloseUI(type, true);
//             }

//             m_UIStack.Clear();
//         }
//         public int GetStackCount()
//         {
//             return m_UIStack.Count;
//         }

//         public string GetStackString()
//         {
//             var sb = new StringBuilder();
//             sb.Append("(");
//             foreach (var s in m_UIStack)
//             {
//                 sb.Append($"{s},");
//             }
//             sb.Append(")");
//             return sb.ToString();
//         }

//         /// <summary> 关闭堆栈内除了指定界面之外的所有UI </summary>
//         public void ClearStackButTarget(string target)
//         {
//             CloseAllStackLoadingUI();

//             var tempStack = new List<string>();
//             tempStack.AddRange(m_UIStack);
//             tempStack.Remove(target);
//             foreach (var typeName in tempStack)
//             {
//                 var type = Type.GetType(NameSpacePrefix + typeName);
//                 if (HasUI(type))
//                     CloseUI(type, true);
//             }

//             m_UIStack.Clear();
//             m_UIStack.Add(target);
//         }

//         /// <summary> 获取栈顶UI实例 </summary>
//         public UILogicBase GetStackTop()
//         {
//             if (m_UIStack.Count == 0)
//                 return null;


//             var type = Type.GetType(NameSpacePrefix + m_UIStack.Last());
//             var ret = GetUI(type);
//             Log.Info($"GetStackTop :{HotfixEntry.UI.GetStackCount()} {HotfixEntry.UI.GetStackString()} -{m_UIStack.Last()}-{type}-{ret}");
//             return ret;
//         }


//         public string GetStackTopName()
//         {
//             if (m_UIStack.Count == 0)
//                 return null;

//             return m_UIStack.Last();
//         }


//         public string GetStackName(int index)
//         {
//             if (m_UIStack.Count <= (index + 1))
//                 return null;

//             return m_UIStack[index];
//         }

//         public UILogicBase GetByIndex(int index)
//         {
//             if (m_UIStack.Count <= (index + 1))
//                 return null;

//             var s = m_UIStack[index];
//             var type = Type.GetType(NameSpacePrefix + s);
//             return GetUI(type);
//         }

//         /// <summary> 关闭所有载入中的UI </summary>
//         public void CloseAllLoadingUI()
//         {
//             foreach (var uiLogic in m_LoadingUIList)
//             {
//                 CloseUI(uiLogic, true);
//             }
//         }

//         /// <summary> 关闭所有载入中的UI，仅对会入栈的UI有效 </summary>
//         public void CloseAllStackLoadingUI()
//         {
//             foreach (var uiLogic in m_LoadingUIList)
//             {
//                 if (uiLogic.UiConfig.IgnoreStack)
//                     continue;
//                 CloseUI(uiLogic, true);
//             }
//         }

//         /// <summary> 跳转到指定UI </summary>
//         /// <param name="jumpId">jump表中的id</param>
//         /// <param name="userData">透传参数，若配置表中的data不为空，则优先使用配置表的data</param>
//         public void JumpUI(int jumpId, object userData = null)
//         {
//             if (!(HotfixEntry.Procedure.CurrentProcedure is GameMain.Scripts.Hotfix.Procedure.ProcedureMain))
//                 return;

//             if (jumpId == JumpIdDefine.None)//跳转到空界面
//                 return;

//             jumpId = this.CheckJumpChange(jumpId, userData, out var newUserData);
//             userData = newUserData;

//             var jumpTable = HotfixEntry.DataTable.GetDataTable<TrGotoTable>().GetGotoTable(jumpId);

//             if (jumpTable == null)
//             {
//                 //未找到配置
//                 return;
//             }

//             //检查解锁
//             if (!UnlockUtility.CheckUnlock(jumpTable.Unlock))
//             {
//                 return;
//             }

//             var type = Type.GetType(NameSpacePrefix + jumpTable.GotoUi);
//             if (type == null)
//             {
//                 //不存在该UI类
//                 Log.Error($"没有找到要跳转的目标界面 {jumpTable.GotoUi}");
//                 return;
//             }

//             var stackTop = GetStackTop();
//             if (type == stackTop?.GetType())
//             {
//                 //目标界面是栈顶，不进行跳转，仍发出通知
//                 HotfixEntry.UI.FireEventDelay(UIEventId.UIOpen, stackTop);
//                 return;
//             }


//             var data = this.JumpUserData(jumpTable.GotoUi, jumpTable.Data);
//             if (data != null)
//                 userData = data;

//             m_JumpTarget = type;
//             m_JumpTable = jumpTable;

//             if (HasUI(type))
//                 CloseUI(type);

//             OpenUI(type, userData);
//         }

//         /// <summary> 使用名字获取用于打开UI界面的类型 </summary>
//         public Type GetUIFullType(string name)
//         {
//             return Type.GetType(NameSpacePrefix + name);
//         }

//         /// <summary> 订阅UI事件自定义回调 </summary>
//         public void Subscribe(UIEventId id, Action<object> handler)
//         {
//             m_EventHandlerPool.Subscribe(id, handler);
//         }

//         /// <summary> 取消订阅UI事件自定义回调 </summary>
//         public void Unsubscribe(UIEventId id, Action<object> handler)
//         {
//             m_EventHandlerPool.Unsubscribe(id, handler);
//         }

//         /// <summary> 抛出UI事件 </summary>
//         public void FireEvent(UIEventId id, object eventData = null)
//         {
//             m_EventHandlerPool.HandleEvent(id, eventData);
//         }

//         /// <summary> 延后一帧抛出UI事件 </summary>
//         public void FireEventDelay(UIEventId id, object eventData = null)
//         {
//             Timers.inst.CallLater(o =>
//             {
//                 m_EventHandlerPool.HandleEvent(id, eventData);
//             });
//         }
//     }
// }