using UnityEngine;
using System.IO;

namespace Shooter3D
{
    public class SaveTxt : MonoBehaviour
    {
        private string savePath = Application.dataPath + "/" + "Date.Shooter3D";
        private float x, y, z;

        public void Save()
        {
            StreamWriter sw = new StreamWriter(savePath);
            sw.WriteLine(transform.position.x);
            sw.WriteLine(transform.position.y);
            sw.WriteLine(transform.position.z);
            sw.Flush();
            sw.Close();
        }

        public void Load()
        {
            if (File.Exists(savePath))
            {
                using (StreamReader streamReader = new StreamReader(savePath))
                {
                    if (streamReader != null)
                    {
                        while (!streamReader.EndOfStream)
                        {
                            x = System.Convert.ToSingle(streamReader.ReadLine());
                            y = System.Convert.ToSingle(streamReader.ReadLine());
                            z = System.Convert.ToSingle(streamReader.ReadLine());
                        }
                        transform.position = new Vector3(x, y, z);
                    }
                }
            }
        }
    }
}
