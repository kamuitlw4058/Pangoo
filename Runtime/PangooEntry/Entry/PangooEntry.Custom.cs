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

        public static PlayerComponent Player{get;private set;}



        private static void InitCustomComponents()
        {
            PangooScene = UnityGameFramework.Runtime.GameEntry.GetComponent<PangooSceneComponent>();

            GameConfig = UnityGameFramework.Runtime.GameEntry.GetComponent<GameConfigComponent>();

            ExcelTable = UnityGameFramework.Runtime.GameEntry.GetComponent<ExcelTableComponent>();

            FGUI = UnityGameFramework.Runtime.GameEntry.GetComponent<FGUIComponent>();

            Player = UnityGameFramework.Runtime.GameEntry.GetComponent<PlayerComponent>();


        }
    }
}