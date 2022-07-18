using System.Runtime.Serialization;
using Task14.StorageInformation;

namespace Task14.Serialization
{
    public class XmlSerialization<T> : ISerialization<T> where T : class
    {
        public void Serialize(T obj, string serializationPath)
        {
            using var fileStream = new FileStream(serializationPath, FileMode.Truncate);

            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(fileStream, obj);
        }
        public T Deserialize(string deserializationPath)
        {
            using var fileStream = new FileStream(deserializationPath, FileMode.Open);

            var deserializer = new DataContractSerializer(typeof(T));
            var deserializedObject = deserializer.ReadObject(fileStream) as T;
            if (deserializedObject is Storage deserializedStorage)
            {
                var (goods, normatives) = deserializedStorage;
                return Storage.GetInstance(normatives, goods) as T;
            }
            return deserializedObject;
        }
    }
}
