using UnityEngine;
using Pangoo.Service;

namespace Pangoo.Core.Character
{

    public class CharacterBaseService : ServiceBase
    {

        public CharacterContainer Character { get; private set; }


        public Transform Transform
        {
            get
            {
                if (Character != null)
                {
                    return Character.CachedTransfrom;
                }
                return null;
            }
        }



        public override void DoAwake(IServiceContainer services)
        {
            base.DoAwake(services);
            Character = services as CharacterContainer;
        }
    }

}

