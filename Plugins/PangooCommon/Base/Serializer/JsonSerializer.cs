
using LitJson;

namespace Pangoo.Common
{
    public class JsonSerializer : StringSerializer
    {
        public override string SerializeToString()
        {
            return JsonMapper.ToJson(this);
        }


        public override void Deserialize(string data)
        {
            JsonMapper.ToObject(data, this.GetType(), this);
        }

    }
}