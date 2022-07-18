using Task14.Serialization;

namespace Task14.Factories
{
    internal class JsonSerializationFactory<T> : ISerializationFactory<T> where T : class
    {
        public ISerialization<T> CreateSerialization()
        {
            return new JsonSerialization<T>();
        }
    }
}
