namespace Task13
{
    public class FileWriter : IDisposable
    {
        public StreamWriter Writer { get; private set; }
        public FileWriter(string path, bool append = true)
        {
            Writer = new StreamWriter(path, append);
        }
        public FileWriter ChangePath(string path, bool append = true)
        {
            Writer = new StreamWriter(path, append);
            return this;
        }
        public void WriteLine(string line)
        {
            Writer.WriteLine(line);
            Writer.Flush();//Щоб зразу вівся запис у файл
        }
        public void WriteCollection<T>(IEnumerable<T> objects)
        {
            foreach (var line in objects)
            {
                if (line != null)
                {
                    Writer.WriteLine(line.ToString());
                }
            }
        }
        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}
