using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentReader;
using System.Collections.Generic;
using System.IO;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSortFile()
        {
            string sFilePath = "c:\\names.txt";
            Assert.IsTrue(File.Exists(sFilePath));

            //Manual Dependency Injection
            IContentReader provider = new CSVTextReader();
            string sWorkingPath = System.Environment.CurrentDirectory;
            string sFilepath = sFilePath;
            DataSortingConsumer consumer = new DataSortingConsumer(provider) { path = sFilepath };
            consumer.Process();

            //verification
            List<Person> listPersons = consumer.GetSorted();
            String prev = null;
            
            foreach (Person person in listPersons)
            {
                if(prev != null)
                {
                    //assert it is going ascending
                    Assert.IsTrue(person.ToString().CompareTo(prev) >= 0);
                }
                prev = person.ToString();
                
            }
        }
    }
}
