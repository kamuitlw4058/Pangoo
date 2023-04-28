#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
#endif
using System.IO;
using System.Xml.Serialization;

namespace Pangoo
{
    public static class CopyUtility
    {

        public static T Clone<T>(T RealObject)
        {
            using (System.IO.Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}