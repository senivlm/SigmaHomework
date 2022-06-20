using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.FileWorker
{
    public interface IFileReader<T>
    {
        public string FilePath { get; }
        public T ReadFromFile();
    }
}
