using Sirenix.OdinInspector;
using LitJson;

namespace Pangoo
{
    public abstract class ExcelRowBase
    {
            [TableTitleGroup("Id")]
            [HideLabel]
            [JsonMember("Id")]
            [ExcelTableCol("Id","Id","int","Id",-1)]
            public int Id ;
    }

}
