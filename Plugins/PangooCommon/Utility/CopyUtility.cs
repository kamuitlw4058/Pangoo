
using System.IO;
using System.Xml.Serialization;

namespace Pangoo.Common
{
    public static class CopyUtility
    {

        public static T Clone<T>(T RealObject)
        {
            using (Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}