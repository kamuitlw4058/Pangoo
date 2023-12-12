using Sirenix.OdinInspector;
using LitJson;
using Pangoo.Common;

namespace Pangoo
{
    public abstract class ExcelNamedRowBase : ExcelRowBase
    {
        [TableTitleGroup("Name")]
        [HideLabel]
        [JsonMember("Name")]
        [ExcelTableCol("Name", "Name", "string", "Name", 0)]
        public string Name;
    }

}
