using System;
using UnityEngine;
using Pangoo.Service;

namespace Pangoo.Core.Character{

    [Serializable]
    public class DriverService : CharacterBaseService
    {
        public Vector3  MoveDirection {get; set;}

        public MovementTypeEnum CurrentMovementType { get; set; }

        public override int Priority{
            get{
                return 3;
            }
        }


    }

}

