using CodeKata.Models;
using CodeKata.Procesors;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKata
{

    class Program
    {
        static void Main(string[] args)
        {
            string inputfile = Path.Combine(Environment.CurrentDirectory, "input.txt");

            Repositories.IRepository<Driver> repository = new Repositories.DriverRepository();
            ICommandParser<Driver> parser = new FileCommandParser(inputfile,repository);
            var procesor = new DriverProccesor(parser.GetData());

            procesor.GetResults();


            Console.ReadLine();
        }
    }
}
