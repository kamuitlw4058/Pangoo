using Pangoo;

namespace Pangoo.Core.Services
{
    public class GameInfoService : BaseService
    {
        public T GetGameInfo<T>() where T : BaseInfo
        {
            return PangooEntry.GameInfo.GetGameInfo<T>();
        }

    }
}