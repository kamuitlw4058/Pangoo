using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;

namespace Pangoo.Core.Character
{

    public class CharacterBaseService : NestedServiceBase
    {

        public CharacterService Character { get; private set; }


        public override float Time => Character.Time;

        public override float DeltaTime => Character.DeltaTime;

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

        public CharacterBaseService(INestedService parent) : base(parent)
        {
            InitParent(parent);
        }

        public void InitParent(INestedService parent)
        {
            if (parent != null)
            {
                if (parent is CharacterService)
                {
                    Character = parent as CharacterService;
                }

                if (parent is CharacterBaseService)
                {
                    Character = (parent as CharacterBaseService).Character;
                }
            }
        }


        public override void DoAwake(INestedService parent)
        {
            InitParent(parent);
        }
    }

}

