using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;
using Pangoo.Core.Service;

namespace Pangoo.Core.Character
{

    public class CharacterBaseService : MonoSubService<CharacterService>
    {

        public CharacterService Character
        {
            get
            {
                return Master;
            }
            set
            {
                Master = value;
            }
        }



        public CharacterBaseService(INestedService parent) : base()
        {
            Parent = parent;
            InitParent(parent);
        }

    }

}

