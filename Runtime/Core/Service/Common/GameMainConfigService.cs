using Pangoo;
using Pangoo.Core.Characters;
using System;

namespace Pangoo.Core.Services
{
    public class GameMainConfigService : BaseService
    {
        public override int Priority => -1;

        public GameMainConfig GameMainConfig
        {
            get
            {
                return PangooEntry.GameConfig.GetGameMainConfig();
            }
        }

        public string DefaultPlayer
        {
            get
            {
                return GameMainConfig?.DefaultPlayer;
            }
        }


        public GameMainConfig GetGameMainConfig()
        {
            return PangooEntry.GameConfig.GetGameMainConfig();
        }

        public FootstepAsset GetFootstepAsset()
        {
            return PangooEntry.GameConfig.GetFootstepAsset();
        }

    }
}