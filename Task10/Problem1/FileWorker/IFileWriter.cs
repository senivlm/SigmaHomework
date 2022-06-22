namespace Task10.FileWorker
{
    public interface IFileWriter<T> : IDisposable
    {
        public StreamWriter Writer { get; }
        public string? GetPath()
        {
            return (Writer.BaseStream as FileStream)?.Name;
        }
        public void WriteLineToFile(T line);
    }
}