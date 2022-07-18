using Task14.Serialization;

namespace Task14.Factories
{
    public class XmlSerializationFactory<T> : ISerializationFactory<T> where T : class
    {
        public ISerialization<T> CreateSerialization()
        {
            return new XmlSerialization<T>();
        }
    }
}
