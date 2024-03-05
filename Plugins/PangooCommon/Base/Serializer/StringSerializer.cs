
using LitJson;

namespace Pangoo.Common
{
    public class StringSerializer : BaseSerializer
    {
        public override object Serialize()
        {
            return SerializeToString();
        }

        public virtual string SerializeToString()
        {
            return string.Empty;
        }

        public override void Deserialize(object data)
        {
            if (data is string)
            {
                Deserialize(data as string);
            }
        }

        public virtual void Deserialize(string data)
        {

        }



    }
}