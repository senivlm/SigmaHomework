namespace Task11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var MyList1 = new MyList();
            MyList1.Add(1);
            MyList1.Add(2);
            MyList1.Add(3);
            MyList1.Add(4);
            MyList1.Add(5);

            MyList1.Sort();
            Console.WriteLine(MyList1);

            Console.WriteLine(MyList1.Find(4));
            Console.WriteLine(MyList1.Find(10));
            
            Console.ReadLine();
        }
    }
}