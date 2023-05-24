using System.Collections.Generic;

namespace Pangoo
{
    /// <summary>
    /// 数据结点管理器接口。
    /// </summary>
    public interface IStaticSceneManager
    {
        List<int> LoadingScene{ get;}
        public Dictionary<int,EntityStaticScene> LoadedStaticSceneDict{get;}

        void  ShowStaticScene(int id);
        
        void Clear();
    }
}
