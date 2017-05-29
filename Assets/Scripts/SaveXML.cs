using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Shooter3D
{
    public class SaveXML : MonoBehaviour
    {
        private string savePath = Application.dataPath + "/" + "Date.Shooter3D";

        public void Save()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Inventory");
            xmlDoc.AppendChild(rootNode);

            XmlElement element;
            element = xmlDoc.CreateElement("Element1");
            element.SetAttribute("value", "Gun");
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("Element2");
            element.SetAttribute("value", "Medicine chest");
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("Element3");
            element.SetAttribute("value", "Flashlight");
            rootNode.AppendChild(element);

            XmlNode userNode;
            XmlAttribute attribute;
            userNode = xmlDoc.CreateElement("Info");
            attribute = xmlDoc.CreateAttribute("Unity");
            attribute.Value = Application.unityVersion;
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "Conpany Name: " + Application.companyName;
            rootNode.AppendChild(userNode);

            xmlDoc.Save(savePath);
        }

        public void Load()
        {
            try
            {
                List<string> tmp = new List<string>();
                XmlTextReader reader = new XmlTextReader(savePath);

                while (reader.Read())
                {
                    if (reader.IsStartElement("Element1"))
                        tmp.Add(reader.GetAttribute("value"));
                    if (reader.IsStartElement("Element2"))
                        tmp.Add(reader.GetAttribute("value"));
                    if (reader.IsStartElement("Element3"))
                        tmp.Add(reader.GetAttribute("value"));
                }
                reader.Close();
            }

            catch (System.Exception)
            {
                Debug.Log("Ошибка чтения файла!");
            }
            
        }

    }
}