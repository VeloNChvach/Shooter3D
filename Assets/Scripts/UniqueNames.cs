using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shooter3D.Editor
{
    public class UniqueNames : MonoBehaviour
    {
        private static Dictionary<string, int> nameDictionary = new Dictionary<string, int>();

        [MenuItem("Shooter3D/Проверка на уникальность имен", false, 0)]
        private static void NewMenuOption()
        {
            GameObject[] sceneObj = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

            if (sceneObj != null)
            {
                foreach (var obj in sceneObj)
                    DataCollection(obj);
            }

            foreach (var i in nameDictionary)
            {
                for (int j = 0; j < i.Value; j++)
                {
                    GameObject gameObj = GameObject.Find(i.Key);
                    if (gameObj)
                        gameObj.name += "(" + j + ")";
                }
            }
            nameDictionary.Clear();
        }

        private static void DataCollection(GameObject sceneObj)
        {
            string[] tempName = sceneObj.name.Split('(');
            tempName[0] = tempName[0].Trim();

            if (!nameDictionary.ContainsKey(tempName[0]))
                nameDictionary.Add(tempName[0], 0);
            else
                nameDictionary[tempName[0]]++;

            sceneObj.name = tempName[0];
        }
    }
}