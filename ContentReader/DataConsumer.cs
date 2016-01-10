using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContentReader
{
    public abstract class DataConsumer
    {

        public IContentReader provider
        {
            get;
            set;
        }

        public string path
        {
            get;
            set;
        }

        abstract public void Process();

    }

    public class DataSortingConsumer : DataConsumer
    {
        public const string m_sSortedSuffix = "-sorted";
        //Dependency Injection interface for Ninject
        public DataSortingConsumer(IContentReader provider)
        {
            this.provider = provider;
        }

        public override void Process()
        {
            List<Person> listPersons = this.provider.readFile(this.path);
            //Sort the person object in ascending order.
            listPersons.Sort();
            System.IO.StreamWriter file = new System.IO.StreamWriter(this.path + m_sSortedSuffix);
            foreach (Person person in listPersons)
            {
                string personPrint = GetFormat(person); 
                System.Console.WriteLine(personPrint);
                file.WriteLine(personPrint);
            }
            file.Close();
        }

        private string GetFormat(Person person)
        {
            //Customized ToString of Person, space separated first and last names.
            return person.ToString();
        }

        //Reading interface for unittest verification
        public List<Person> GetSorted()
        {
            return this.provider.readFile(this.path + m_sSortedSuffix);
        }
    }
}
