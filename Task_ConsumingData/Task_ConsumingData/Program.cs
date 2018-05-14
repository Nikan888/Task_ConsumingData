using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Task_ConsumingData
{
    class Program
    {
        static void Main(string[] args)
        {
            BookList bookList = new BookList();
            bookList.AddBookToList("1984", "Orwell", "Social Science Fiction", 25);
            bookList.AddBookToList("Dune", "Herbert", "Science Fiction", 74);
            bookList.AddBookToList("Starship Troopers", "Heinlein", "Military Science Fiction", 98);
            bookList.AddBookToList("Fahrenheit 451", "Bradbury", "Dystopian", 12);
            bookList.AddBookToList("Nightfall", "Asimov", "Science Fiction", 115);
            bookList.AddBookToList("Hard to Be a God", "Strugatsky", "Science Fiction", 24);

            try
            {
                SaveXMLFile(AppDomain.CurrentDomain.BaseDirectory + "testxml.xml");
                ReadXMLFile(AppDomain.CurrentDomain.BaseDirectory + "testxml.xml");
                SaveJSONFile(AppDomain.CurrentDomain.BaseDirectory + "testjson.json");
                ReadJSONFile(AppDomain.CurrentDomain.BaseDirectory + "testjson.json");
                //WriteToXmlFile<List<Book>>(AppDomain.CurrentDomain.BaseDirectory, bookList);
                //WriteToJsonFile<List<Book>>(AppDomain.CurrentDomain.BaseDirectory, bookList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                Console.ReadKey();
            }
        }

        public static void SaveXMLFile(string pathToFile)
        {

            using (XmlWriter writer = XmlWriter.Create(pathToFile, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("BOOKS");
                foreach (var x in BookList.books)
                {
                    writer.WriteStartElement("Book");
                    writer.WriteElementString("name", x.Name);
                    writer.WriteElementString("author", x.AuthorName);
                    writer.WriteElementString("genre", x.Genre);
                    writer.WriteElementString("price", x.Price.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                writer.WriteEndDocument();
            }
        }

        public static void ReadXMLFile(string pathToFile)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToFile);
            if (BookList.books.Count > 0)
            {
                XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("Book");
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    string Name = xmlElement.GetElementsByTagName("name")[0].ChildNodes[0].InnerText;
                    string Author = xmlElement.GetElementsByTagName("author")[0].ChildNodes[0].InnerText;
                    string Genre = xmlElement.GetElementsByTagName("genre")[0].ChildNodes[0].InnerText;
                    string Price = xmlElement.GetElementsByTagName("price")[0].ChildNodes[0].InnerText;
                }
            }
            else throw new Exception("Списка не существует");
        }

        public static void SaveJSONFile(string pathToFile)
        {
            string json = JsonConvert.SerializeObject(BookList.books, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(pathToFile, json);
        }

        public static void ReadJSONFile(string pathToFile)
        {
            string json = File.ReadAllText(pathToFile);
            List<Book> jBook = JsonConvert.DeserializeObject<List<Book>>(json);
            foreach (var j in jBook)
            {
                string name = j.Name;
                string author = j.AuthorName;
                string genre = j.Genre;
                string price = j.Price.ToString();
            }
        }

        //public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        //{
        //    TextWriter writer = null;
        //    try
        //    {
        //        var serializer = new XmlSerializer(typeof(T));
        //        writer = new StreamWriter(filePath, append);
        //        serializer.Serialize(writer, objectToWrite);
        //    }
        //    finally
        //    {
        //        if (writer != null)
        //            writer.Close();
        //    }
        //}

        //public static T ReadFromXmlFile<T>(string filePath) where T : new()
        //{
        //    TextReader reader = null;
        //    try
        //    {
        //        var serializer = new XmlSerializer(typeof(T));
        //        reader = new StreamReader(filePath);
        //        return (T)serializer.Deserialize(reader);
        //    }
        //    finally
        //    {
        //        if (reader != null)
        //            reader.Close();
        //    }
        //}

        //public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        //{
        //    TextWriter writer = null;
        //    try
        //    {
        //        var contentsToWriteToFile = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite);
        //        writer = new StreamWriter(filePath, append);
        //        writer.Write(contentsToWriteToFile);
        //    }
        //    finally
        //    {
        //        if (writer != null)
        //            writer.Close();
        //    }
        //}

        //public static T ReadFromJsonFile<T>(string filePath) where T : new()
        //{
        //    TextReader reader = null;
        //    try
        //    {
        //        reader = new StreamReader(filePath);
        //        var fileContents = reader.ReadToEnd();
        //        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(fileContents);
        //    }
        //    finally
        //    {
        //        if (reader != null)
        //            reader.Close();
        //    }
        //}
    }
}
