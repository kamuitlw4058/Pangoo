
namespace Pangoo.Common
{
    public abstract class BaseSerializer : ISerializer
    {
        public abstract object Serialize();

        public abstract void Deserialize(object data);
    }
}