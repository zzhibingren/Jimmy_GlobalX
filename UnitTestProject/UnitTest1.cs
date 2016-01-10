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
            //unittest will construct the input file , then test the file
            //string sFilePath = "c:\\names.txt";
            string sWorkingPath = System.Environment.CurrentDirectory;
            string sFilePath = sWorkingPath + "\\names.txt";
            if (!File.Exists(sFilePath))
            {
                ConstructInputFile(sFilePath);
            }
            Assert.IsTrue(File.Exists(sFilePath));

            //Manual Dependency Injection
            IContentReader provider = new CSVTextReader();
            string sFilepath = sFilePath;
            DataSortingConsumer consumer = new DataSortingConsumer(provider) { path = sFilepath };
            consumer.Process();

            //verification on the order of names
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

        public void ConstructInputFile(string sFilePath)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(sFilePath);
            List<Person> listPersons = new List<Person>();
            listPersons.Add(new Person("BAKER","THEODORE"));
            listPersons.Add(new Person("SMITH", "ANDREW"));
            listPersons.Add(new Person("KENT", "MADISON"));
            listPersons.Add(new Person("SMITH", "FREDRICK"));
            foreach (Person person in listPersons)
            {
                file.WriteLine(person.ToString());
            }
            file.Close();
        }
    }
}
