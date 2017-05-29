using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Xml;
using System.IO;

namespace Shooter3D
{
    public class SaveLevel
    {
        private static GameObject[] _walls;
        private static int _numberScene = 0;
        private static List<Levels> _levelsList = new List<Levels>();

        [MenuItem("Shooter3D/Сохранить уровень", false, 1)]
        private static void NewMenuOption()
        {
            string savePath = Application.dataPath + "/" + "Data.xml";
            _walls = GameObject.FindGameObjectsWithTag("Wall");

            FromXmlDocument(savePath);

            if (_walls != null)
            {
                XmlDocument document = new XmlDocument();
                XmlNode root = document.CreateElement("Scene");

                ToXmlDocument(document, root);
                document.AppendChild(root);
                document.Save(savePath);

                Debug.Log("Сохранение прошло успешно!");
            }

            Clear();
        }

        private struct Levels
        {
            private string _numberLevl;
            private List<Walls> _wallses;

            public Levels(string numberLevl, List<Walls> wallses)
            {
                _numberLevl = numberLevl;
                _wallses = wallses;
            }

            public List<Walls> Walles
            {
                get { return _wallses; }
            }

            public string NumberLevl
            {
                get { return _numberLevl; }
            }
        }

        private struct Walls
        {
            private string _name;
            private string _posX;
            private string _posY;
            private string _posZ;
            private string _rotX;
            private string _rotY;
            private string _rotZ;
            private string _scalX;
            private string _scalY;
            private string _scalZ;

            public Walls(string name, string posX, string posY, string posZ,
                string rotX, string rotY, string rotZ, string scalX, string scalY, string scalZ)
            {
                _name = name;
                _posX = posX;
                _posY = posY;
                _posZ = posZ;
                _rotX = rotX;
                _rotY = rotY;
                _rotZ = rotZ;
                _scalX = scalX;
                _scalY = scalY;
                _scalZ = scalZ;
            }

            public string Name
            {
                get { return _name; }
            }
        }

        private static void FromXmlDocument(string path)
        {
            XmlDocument document = new XmlDocument();

            if (File.Exists(path))
                document.Load(path);

            XmlNode root = document.GetElementsByTagName("Scene").Item(0);

            string numberScene = String.Empty;

            string name = String.Empty;
            string posX = String.Empty;
            string posY = String.Empty;
            string posZ = String.Empty;
            string rotX = String.Empty;
            string rotY = String.Empty;
            string rotZ = String.Empty;
            string scalX = String.Empty;
            string scalY = String.Empty;
            string scalZ = String.Empty;

            List<Walls> wallsesList = new List<Walls>();

            if (root != null)
            {
                foreach (XmlNode node in root)
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case "NumberLevl":
                                numberScene = attribute.Value;
                                if (int.Parse(numberScene) > _numberScene)
                                    _numberScene = int.Parse(numberScene);
                                break;
                        }
                    }
                    wallsesList = new List<Walls>();

                    foreach (XmlNode xmlNode in node)
                    {
                        switch (xmlNode.Name)
                        {
                            case "Wall":
                                foreach (XmlAttribute attribute in xmlNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            name = attribute.Value;
                                            break;
                                        case "PosX":
                                            posX = attribute.Value;
                                            break;
                                        case "PosY":
                                            posY = attribute.Value;
                                            break;
                                        case "PosZ":
                                            posZ = attribute.Value;
                                            break;
                                        case "RotX":
                                            rotX = attribute.Value;
                                            break;
                                        case "RotY":
                                            rotY = attribute.Value;
                                            break;
                                        case "RotZ":
                                            rotZ = attribute.Value;
                                            break;
                                        case "ScalX":
                                            scalX = attribute.Value;
                                            break;
                                        case "ScalY":
                                            scalY = attribute.Value;
                                            break;
                                        case "ScalZ":
                                            scalZ = attribute.Value;
                                            break;
                                    }
                                }
                                wallsesList.Add(new Walls(name, posX, posY, posZ, rotX, rotY, rotZ, scalX, scalY, scalZ));
                                break;
                        }
                    }
                    _levelsList.Add(new Levels(numberScene, wallsesList));
                }
            }
            wallsesList = new List<Walls>();
            foreach (var i in _walls)
            {
                name = i.name;
                posX = i.transform.position.x.ToString();
                posY = i.transform.position.y.ToString();
                posZ = i.transform.position.z.ToString();
                rotX = i.transform.rotation.x.ToString();
                rotY = i.transform.rotation.y.ToString();
                rotZ = i.transform.rotation.z.ToString();
                scalX = i.transform.localScale.x.ToString();
                scalY = i.transform.localScale.y.ToString();
                scalZ = i.transform.localScale.z.ToString();

                Walls walls = new Walls(name, posX, posY, posZ, rotX, rotY, rotZ, scalX, scalY, scalZ);
                wallsesList.Add(walls);
            }

            numberScene = (++_numberScene).ToString();

            _levelsList.Add(new Levels(numberScene, wallsesList));
        }

        private static void ToXmlDocument(XmlDocument document, XmlNode node)
        {
            foreach (var levelse in _levelsList)
            {
                XmlNode levl = document.CreateElement("Level");

                XmlAttribute numberLevl = document.CreateAttribute("NumberLevl");
                numberLevl.InnerText = levelse.NumberLevl;
                levl.Attributes.Append(numberLevl);

                node.AppendChild(levl);

                foreach (var i in levelse.Walles)
                {
                    XmlNode wall = document.CreateElement("Wall");

                    //SaveObj(document, wall, levl, i.Name, i.PosX, i.PosY, i.PosZ, i.RotX, i.RotY, i.RotZ, i.ScaleX, i.ScaleY, i.ScaleZ); //need correct
                }
            }
        }

        private static void SaveObj(XmlDocument document, XmlNode node, XmlNode appendNode, string name, 
            string positionX, string positionY, string positionZ,
            string rotationX, string rotationY, string rotationZ,
            string scaleX, string scaleY, string scaleZ)
        {
            XmlAttribute nameObject = document.CreateAttribute("Name");
            nameObject.InnerText = name;
            node.Attributes.Append(nameObject);

            XmlAttribute posX = document.CreateAttribute("PosX");
            posX.InnerText = positionX;
            node.Attributes.Append(posX);

            XmlAttribute posY = document.CreateAttribute("PosY");
            posY.InnerText = positionY;
            node.Attributes.Append(posY);

            XmlAttribute posZ = document.CreateAttribute("PosZ");
            posZ.InnerText = positionZ;
            node.Attributes.Append(posZ);

            XmlAttribute rotX = document.CreateAttribute("RotX");
            rotX.InnerText = rotationX;
            node.Attributes.Append(rotX);

            XmlAttribute rotY = document.CreateAttribute("RotY");
            rotY.InnerText = rotationY;
            node.Attributes.Append(rotY);

            XmlAttribute rotZ = document.CreateAttribute("RotZ");
            rotZ.InnerText = rotationZ;
            node.Attributes.Append(rotZ);

            XmlAttribute scalX = document.CreateAttribute("ScalX");
            scalX.InnerText = scaleX;
            node.Attributes.Append(scalX);

            XmlAttribute scalY = document.CreateAttribute("ScalY");
            scalY.InnerText = scaleY;
            node.Attributes.Append(scalY);

            XmlAttribute scalZ = document.CreateAttribute("ScalZ");
            scalZ.InnerText = scaleZ;
            node.Attributes.Append(scalZ);

            appendNode.AppendChild(node);
        }

        private static void Clear()
        {
            _levelsList.Clear();
            _numberScene = 0;
            _walls = null;
        }

    }
}