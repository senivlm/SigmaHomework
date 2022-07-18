using Task14.Serialization;

namespace Task14.Factories
{
    public interface ISerializationFactory<T> where T : class
    {
        public ISerialization<T> CreateSerialization();
    }
}
