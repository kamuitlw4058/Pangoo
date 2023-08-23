using Pangoo;

namespace Pangoo.Service
{
    public class GameInfoService : ServiceBase
    {
        public T GetGameInfo<T>() where T:BaseInfo
        {
            return  PangooEntry.GameInfo.GetGameInfo<T>();
        }

    }
}