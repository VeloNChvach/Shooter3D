using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

namespace Shooter3D.Helper
{
    public class SceneModel
    {
        public void CreateLevel(int numberLvl)
        {
            string savePath = Application.dataPath + "/" + "Data.xml";
            if (!File.Exists(savePath)) return;

            //ClearSceneModel();
            XmlDocument document = new XmlDocument();
            document.Load(savePath);

            XmlNode root = document.GetElementsByTagName("Scene").Item(0);

            int numberScene = 0;

            if (root != null)
            {
                foreach (XmlNode node in root)
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case "NumberLevl":
                                numberScene = int.Parse(attribute.Value);
                                    break;
                        }
                    }

                    if (numberScene == numberLvl)
                    {
                        foreach (XmlNode xmlNode in node)
                        {
                            switch (xmlNode.Name)
                            {
                                case "Wall":
                                    ParseObj(xmlNode, "Wall");
                                    break;
                            }
                        }
                    }

                    if (numberScene == numberLvl) break;
                }
            }
        }

        private void ParseObj(XmlNode node, string nameObj)
        {
            BaseObjectScene obj = null;
            string name = String.Empty;

            Vector3 position = new Vector3();
            Quaternion rotation = new Quaternion();
            Vector3 scale = new Vector3();

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        name = attribute.Value;
                        break;
                    case "PosX":
                        position.x = float.Parse(attribute.Value);
                        break;
                    case "PosY":
                        position.y = float.Parse(attribute.Value);
                        break;
                    case "PosZ":
                        position.z = float.Parse(attribute.Value);
                        break;
                    case "RotX":
                        rotation.x = float.Parse(attribute.Value);
                        break;
                    case "RotY":
                        rotation.y = float.Parse(attribute.Value);
                        break;
                    case "RotZ":
                        rotation.z = float.Parse(attribute.Value);
                        break;
                    case "ScalX":
                        scale.x = float.Parse(attribute.Value);
                        break;
                    case "ScalY":
                        scale.y = float.Parse(attribute.Value);
                        break;
                    case "ScalZ":
                        scale.z = float.Parse(attribute.Value);
                        break;
                }
            }
            if (nameObj == "Wall")
            {
                //Create objects with parameters
            }

            obj.Position = position;
            obj.Roration = rotation;
            obj.Scale = scale;
            obj.Name = name;
        }
    }
}