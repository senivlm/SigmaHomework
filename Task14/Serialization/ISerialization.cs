namespace Task14.Serialization
{
    public interface ISerialization<T> where T : class
    {
        public void Serialize(T obj, string serializationPath);
        public T Deserialize(string deserializationPath);
    }
}
