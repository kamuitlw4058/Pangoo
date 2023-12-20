using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{

    public static class EntityGroupRowExtension
    {
        public static IEntityGroupRow CreateDynamicObjectGroup()
        {
            var row = new EntityGroupRow();
            row.Id = 0;
            row.Name = "DynamicObject";
            row.InstanceAutoReleaseInterval = 0;
            row.InstanceExpireTime = 0;
            row.InstancePriority = 0;
            row.InstanceCapacity = 10000;
            return row;
        }

        public static IEntityGroupRow CreateStaticSceneGroup()
        {
            var row = new EntityGroupRow();
            row.Id = 0;
            row.Name = "StaticScene";
            row.InstanceAutoReleaseInterval = 0;
            row.InstanceExpireTime = 0;
            row.InstancePriority = 0;
            row.InstanceCapacity = 10000;
            return row;
        }

        public static IEntityGroupRow CreateCharacterGroup()
        {
            var row = new EntityGroupRow();
            row.Id = 0;
            row.Name = "Character";
            row.InstanceAutoReleaseInterval = 0;
            row.InstanceExpireTime = 0;
            row.InstancePriority = 0;
            row.InstanceCapacity = 10000;
            return row;
        }


    }
}
