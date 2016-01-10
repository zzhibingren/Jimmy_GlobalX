using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace ContentReader
{
    public class Person : System.IComparable<Person> , System.IEquatable<Person>
    {
        public Person(string sFirstName, string sLastName)
        {
            m_sFirstName = sFirstName;
            m_sLastName = sLastName;
        }

        public string FirstName
        {
            get { return m_sFirstName; }
            set { m_sFirstName = value; }
        }
        public string LastName
        {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }

        private string m_sFirstName;
        private string m_sLastName;

        public bool Equals(Person otherobj)
        {
            if (otherobj == null)
                return false;
            else
            {
                string strSortKeySelf = this.m_sFirstName + this.m_sLastName;
                string strSortKeyOther = otherobj.m_sFirstName + otherobj.m_sLastName;
                return strSortKeySelf.Equals(strSortKeyOther);
            }
        }

        public int CompareTo(Person otherobj)
        {
            // A null value means that this object is greater.
            if (otherobj == null)
                return 1;
            else
            {
                string strSortKeySelf = this.m_sFirstName + this.m_sLastName;
                string strSortKeyOther = otherobj.m_sFirstName + otherobj.m_sLastName;
                return strSortKeySelf.CompareTo(strSortKeyOther);
            }
        }

        public override string ToString()
        {
            return System.String.Format("{0},{1}", this.m_sFirstName, this.m_sLastName);
        }

    }

    public interface IContentReader
    {
        System.Collections.Generic.List<Person> readFile(string filename);
    }

    public class CSVTextReader : IContentReader
    {
        public System.Collections.Generic.List<Person> readFile(string filePath)
        {
            List<Person> listUser = new List<Person>();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            string strLine = null;
            while ((strLine = sr.ReadLine()) != null)
            {
                string[] arrLine = strLine.Split(',');
                if (arrLine.Count() == 2)
                {
                    listUser.Add(new Person(arrLine[0].Trim(), arrLine[1].Trim()));
                }
            }
            sr.Close();
            fs.Close();
            return listUser;
        }
    }
    class XMLTextReader : IContentReader
    {
        public System.Collections.Generic.List<Person> readFile(string filePath)
        {
            List<Person> listUser = new List<Person>();
            ////implementation for reading HTML file
            return listUser;
        }
    }
    class JsonTextReader : IContentReader
    {
        public System.Collections.Generic.List<Person> readFile(string filePath)
        {
            List<Person> listUser = new List<Person>();
            //implementation for JSON file
            return listUser;
        }
    }



}
