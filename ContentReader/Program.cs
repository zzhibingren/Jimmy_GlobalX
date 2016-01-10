using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Reflection;
using Ninject.Modules;
using System.IO;

namespace ContentReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Count() != 1)
            {
                System.Console.WriteLine("Please type as : ContentReader.exe c:\\names.txt \n");
                System.Environment.Exit(1);
            }
            string sFilePath = args[0];
            if(!File.Exists(sFilePath) )
            {
                System.Console.WriteLine("File doesn't exist. \n");
                System.Environment.Exit(1);
            }
            ProcessNames(sFilePath);
        }

        static public void ProcessNames(string sFilePath)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IContentReader provider = kernel.Get<IContentReader>();
            string sFilepath = sFilePath;
            DataConsumer consumer = new DataSortingConsumer(provider) { path = sFilepath };
            consumer.Process();
        }

    }
     


    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IContentReader>().To<CSVTextReader>();

            //Bind<IContentReader>().To<XMLTextReader>();

            //Bind<IContentReader>().To<JsonTextReader>();
        }
    }

}
