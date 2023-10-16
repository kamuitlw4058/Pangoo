using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;
using Pangoo.Core.Service;

namespace Pangoo.Core.Characters
{

    public class CharacterBaseService : MonoSubService<Character>
    {

        public Character Character
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



        public CharacterBaseService(NestedBaseService parent) : base()
        {
            Parent = parent;
            InitParent();
        }

    }

}

