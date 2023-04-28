using UnityEngine;

namespace Pangoo
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class PangooEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
        }
    }
}
