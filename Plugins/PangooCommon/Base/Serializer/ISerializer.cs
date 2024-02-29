
namespace Pangoo.Common
{
    public interface ISerializer
    {
        object Serialize();

        void Deserialize(object data);
    }
}