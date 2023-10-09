using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace Pangoo
{

    public static class EntityGroupRowExtension
    {
        public static EntityGroupTable.EntityGroupRow CreateDynamicObjectGroup()
        {
            var row = new EntityGroupTable.EntityGroupRow();
            row.Id = 0;
            row.Name = "DynamicObject";
            row.InstanceAutoReleaseInterval = 0;
            row.InstanceExpireTime = 0;
            row.InstancePriority = 0;
            row.InstanceCapacity = 10000;
            return row;
        }

        public static EntityGroupTable.EntityGroupRow CreateStaticSceneGroup()
        {
            var row = new EntityGroupTable.EntityGroupRow();
            row.Id = 0;
            row.Name = "StaticScene";
            row.InstanceAutoReleaseInterval = 0;
            row.InstanceExpireTime = 0;
            row.InstancePriority = 0;
            row.InstanceCapacity = 10000;
            return row;
        }


    }
}
