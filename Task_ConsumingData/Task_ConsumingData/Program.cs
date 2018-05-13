﻿using Newtonsoft.Json;
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
            BookHelper bookHelper = new BookHelper();
            bookHelper.AddBookToList("1984", "Orwell", "Social Science Fiction", 25);
            bookHelper.AddBookToList("Dune", "Herbert", "Science Fiction", 74);
            bookHelper.AddBookToList("Starship Troopers", "Heinlein", "Military Science Fiction", 98);
            bookHelper.AddBookToList("Fahrenheit 451", "Bradbury", "Dystopian", 12);
            bookHelper.AddBookToList("Nightfall", "Asimov", "Science Fiction", 115);
            bookHelper.AddBookToList("Hard to Be a God", "Strugatsky", "Science Fiction", 24);

            SaveXMLFile(AppDomain.CurrentDomain.BaseDirectory + "testxml.xml");
            ReadXMLFile(AppDomain.CurrentDomain.BaseDirectory + "testxml.xml");
            SaveJSONFile(AppDomain.CurrentDomain.BaseDirectory + "testjson.json");
            ReadJSONFile(AppDomain.CurrentDomain.BaseDirectory + "testjson.json");
            //WriteToXmlFile<List<Book>>(AppDomain.CurrentDomain.BaseDirectory, bookHelper);
            //WriteToJsonFile<List<Book>>(AppDomain.CurrentDomain.BaseDirectory, bookHelper);
        }

        public static void SaveXMLFile(string pathToFile)
        {

            using (XmlWriter writer = XmlWriter.Create(pathToFile, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("BOOKS");
                foreach (var x in BookHelper.books)
                {
                    writer.WriteStartElement("Book");
                    writer.WriteElementString("Name", x.Name);
                    writer.WriteElementString("Author", x.AuthorName);
                    writer.WriteElementString("Genre", x.Genre);
                    writer.WriteElementString("Price", x.Price.ToString());
                    writer.WriteEndElement();
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
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("Book");
            foreach(XmlNode xmlNode in xmlNodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                string Name = Convert.ToString(xmlNode.Attributes["Name"].Value);
                string Author = xmlElement.GetElementsByTagName("Author")[0].ChildNodes[0].InnerText;
                string Genre = xmlElement.GetElementsByTagName("Genre")[0].ChildNodes[0].InnerText;
                string Price = xmlElement.GetElementsByTagName("Price")[0].ChildNodes[0].InnerText;
            }
        }

        public static void SaveJSONFile(string pathToFile)
        {
            string json = JsonConvert.SerializeObject(BookHelper.books, Newtonsoft.Json.Formatting.Indented);
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
