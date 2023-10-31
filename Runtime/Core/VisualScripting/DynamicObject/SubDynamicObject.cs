using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;


namespace Pangoo.Core.VisualScripting
{

    public struct SubDynamicObject
    {
        [TableTitleGroup("动态物体Id")]
        [HideLabel]
        [TableColumnWidth(200, resizable: false)]
        [JsonMember("DynamicObjectId")]
        public int DynamicObjectId;

        public string Path;

    }

}