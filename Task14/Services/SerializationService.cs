using Task14.Factories;
using Task14.Serialization;

namespace Task14.Services
{
    public class SerializationService<T> where T : class
    {
        public event Action<string>? OnExceptionAction;
        private readonly ISerialization<T> _serialization;
        public SerializationService(ISerializationFactory<T> serializationFactory)
        {
            _serialization = serializationFactory.CreateSerialization();
        }
        public void Serialize(T obj, string serializationPath)
        {
            try
            {
                _serialization.Serialize(obj, serializationPath);
            }
            catch (Exception exception)
            {
                OnExceptionAction?.Invoke($"{exception.GetType().Name}: {exception.Message} ({DateTime.Now})");
            }
        }
        public T Deserialize(string deserializationPath)
        {
            try
            {
                return _serialization.Deserialize(deserializationPath);
            }
            catch (Exception exception)
            {
                OnExceptionAction?.Invoke($"{exception.GetType().Name}: {exception.Message} ({DateTime.Now})");
                return null;
            }
        }
    }
}
