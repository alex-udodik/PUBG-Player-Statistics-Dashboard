using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace JSONLibrary
{
    public class Xml : IFile
    {
        private const string KEYVALUEPAIRS = "KeyValuePairs";
        private const string PAIR = "Pair";
        private const string NAME = "Name";
        private const string ACCOUNT_ID = "AccountID";

        public Dictionary<string, string> LoadFromFile(Stream stream)
        {
            if (stream != null)
            {
                var settings = new XmlReaderSettings()
                {
                    IgnoreWhitespace = true,
                };

                FileStream filestream = (FileStream)stream;
                string fileContents;
                using (StreamReader reader = new StreamReader(filestream))
                {
                    fileContents = reader.ReadToEnd();
                }

                var doc = XDocument.Parse(fileContents);

                // filter by tags
                var tags = new XElement(
                    KEYVALUEPAIRS,
                    from setting in doc.Element(KEYVALUEPAIRS).Elements(PAIR) select new XElement(PAIR, setting.Element(NAME), setting.Element(ACCOUNT_ID)));

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(tags.ToString());

                XmlNodeList elemName = xml.GetElementsByTagName(NAME);
                XmlNodeList elemAccountID = xml.GetElementsByTagName(ACCOUNT_ID);

                Dictionary<string, string> pairs = new Dictionary<string, string>();

                for (int i = 0; i < elemName.Count; i++)
                {
                    pairs.Add(elemName[i].InnerXml.ToString(), elemAccountID[i].InnerXml.ToString());
                }

                return pairs;
            }
            else
            {
                return null;
            }
            
        }


        public void SaveToFile(Stream stream, Dictionary<string, string> nameAndIds)
        {
            if (nameAndIds != null || nameAndIds.Count > 0)
            {
                var settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "   ",
                };

                XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(KEYVALUEPAIRS);

                foreach (KeyValuePair<string, string> pair in nameAndIds)
                {
                    xmlWriter.WriteStartElement(PAIR);
                    xmlWriter.WriteStartElement(NAME);
                    xmlWriter.WriteString(pair.Key.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(ACCOUNT_ID);
                    xmlWriter.WriteString(pair.Value.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            

        }
    }
}
