using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.FileWorker
{
    public interface IFileReader<T> : IDisposable
    {
        public StreamReader Reader { get; }
        public T GetLineFromFile();
        public string? GetPath()
        {
            return (Reader.BaseStream as FileStream)?.Name;
        }
        public void RestartStreamReader()
        {
            Reader.BaseStream.Position = 0;
            Reader.DiscardBufferedData();
        }
    }
}
