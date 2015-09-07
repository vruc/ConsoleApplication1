using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using ClassLibrary1;

namespace ConsoleApplication1
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //new SimpleMefDemo().Run();

            new Notifier().Notify("AAAAAAAAAA");

            Console.WriteLine("press any key to continue......");
            Console.ReadKey();
        }

    }
}
