using System;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerable_Hell
{
    class Program
    {

        public static List<ClassA> SourceDB = null;

        private static List<ClassA> CreateSourceDB()
        {
            var result = Enumerable.Range(0, 10)
                .Select(r => new ClassA {Id = r, Code = string.Format("Code{0}", r)})
                .ToList();
            return result;
        }

        private static IEnumerable<ClassA> GetClassAEnum()
        {
            for (var i = 0; i < SourceDB.Count; i++)
            {
                Console.WriteLine("Yielded item {0}", i);
                yield return SourceDB[i];
            }
        }

        private static IEnumerable<ClassB> GetMappedEnum(IEnumerable<ClassA> source)
        {
            return source.Select(a => new ClassB() {Identification = a.Id, CustomerCode = "B_" + a.Code});
        }

        static void Main(string[] args)
        {
            SourceDB = SourceDB ?? CreateSourceDB();

            Console.WriteLine("Direct from SourceDB");
            var a = GetClassAEnum().Where(item => item.Id > 2).Take(2);
            a.ToList().ForEach(item => Console.WriteLine("Id: {0}, Code: {1}]",item.Id,item.Code));

            Console.WriteLine("Press any key...");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Mapped access:");

            var b = GetMappedEnum(GetClassAEnum()).Where(item => item.Identification > 2).Take(2);
            b.ToList().ForEach(item => Console.WriteLine("Id: {0}, Code: {1}]", item.Identification, item.CustomerCode));

            Console.WriteLine("Press any key...");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("ToList access:");

            var c = GetMappedEnum(GetClassAEnum().ToList().Where(item => item.Id > 2)).Take(2);
            c.ToList().ForEach(item => Console.WriteLine("Id: {0}, Code: {1}]", item.Identification, item.CustomerCode));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
