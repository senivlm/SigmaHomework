using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    internal interface IPrint<T>
    {
        public T Field { get; }
        public void Print();
    }
    public class Printer<T> : IPrint<T>
    {
        private readonly T _field;
        public T Field => _field;
        public Printer(T field)
        {
            _field = field;
        }
        public void Print()
        {
            Console.WriteLine(Field);
        }
    }
}
