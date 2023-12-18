using System.Collections.Generic;
// using SteamClient.Service;
using UnityEngine;
using UnityEngine.Events;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class PangooEntry : MonoBehaviour
    {

        public static PangooSceneComponent PangooScene { get; private set; }

        public static GameConfigComponent GameConfig { get; private set; }

        public static ExcelTableComponent ExcelTable { get; private set; }

        public static FGUIComponent FGUI { get; private set; }

        public static PlayerComponent Player { get; private set; }

        public static ServiceComponent Service { get; set; }

        public static GameInfoComponent GameInfo { get; set; }


        public static MetaTableComponent MetaTable { get; set; }



        private static void InitCustomComponents()
        {
            PangooScene = UnityGameFramework.Runtime.GameEntry.GetComponent<PangooSceneComponent>();

            GameConfig = UnityGameFramework.Runtime.GameEntry.GetComponent<GameConfigComponent>();

            ExcelTable = UnityGameFramework.Runtime.GameEntry.GetComponent<ExcelTableComponent>();

            FGUI = UnityGameFramework.Runtime.GameEntry.GetComponent<FGUIComponent>();

            Player = UnityGameFramework.Runtime.GameEntry.GetComponent<PlayerComponent>();

            Service = UnityGameFramework.Runtime.GameEntry.GetComponent<ServiceComponent>();

            GameInfo = UnityGameFramework.Runtime.GameEntry.GetComponent<GameInfoComponent>();

            MetaTable = UnityGameFramework.Runtime.GameEntry.GetComponent<MetaTableComponent>();



        }
    }
}