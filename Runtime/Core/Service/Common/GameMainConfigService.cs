using Pangoo;
using System;

namespace Pangoo.Core.Services
{
    [Serializable]
    public class GameMainConfigService : BaseService
    {
        public override int Priority => -1;


        public GameMainConfig GetGameMainConfig()
        {
            return PangooEntry.GameConfig.GetGameMainConfig();
        }

    }
}