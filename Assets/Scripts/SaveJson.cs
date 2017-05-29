using System.IO;
using UnityEngine;

namespace Shooter3D
{
    public class SaveJson : MonoBehaviour
    {
        void Start()
        {
            Data info = new Data();
            Save(info);

            Data info_load = Load();
            Debug.Log(info_load.Name);
        }

        public void Save(Data info)
        {
            string str = JsonUtility.ToJson(info);
            File.WriteAllText(Application.dataPath + "/Data.txt", str);
        }

        public Data Load()
        {
            string str = File.ReadAllText(Application.dataPath + "/Data.txt");
            Data info = JsonUtility.FromJson<Data>(str);
            return info;
        }
    }

    public class Data
    {
        public string Name;
    }
}