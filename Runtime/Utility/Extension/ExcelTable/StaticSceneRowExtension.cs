using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{

    public static class StaticSceneRowExtension
    {

        public static IStaticSceneRow ToInterface(this StaticSceneTable.StaticSceneRow row)
        {
            var json = LitJson.JsonMapper.ToJson(row);
            return LitJson.JsonMapper.ToObject<Pangoo.MetaTable.StaticSceneRow>(json);
        }

    }
}
