
namespace SteamClient.Hotfix
{
    public enum UIEventId
    {
        CurrencyChange,//货币资源发生变化
        HeroChange,//英雄发生变化
        UIOpen,//一个界面打开完成，透传参数为该UI逻辑类
        ReceiveMessage,//收到并处理完成一条协议，透传参数为协议ID
        AirshipChange,//空艇数据发生变化
        RoleChange,//角色信息发生变化
        UIClick,//有UI组件触发了点击回调，透传参数为被点击的GObject
        ProcedureChange,//切换流程，透传参数为新流程
        GuideDragEnd,//引导中拖动操作通过
        RoleLvUp,//角色升级，透传参数为旧等级
        WorldMissionUpdate,//有world mission更新
        EventUpdate,//有event更新
        StoryEnd,//有对话结束
        ContactEnd,//通讯结束
        BookClose,//图书关闭
        BuildItemOffset,//设置建筑 GGUI的偏移量
        GuideCastSkill,//引导释放技能
    }
}