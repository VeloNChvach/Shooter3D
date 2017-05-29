using UnityEngine;

namespace Shooter3D
{
    public class SavePlayerPrefs : MonoBehaviour
    {
        public void Save()
        {
            int age = 5;
            PlayerPrefs.SetInt("Age", age);

            float fractional = 1.5f;
            PlayerPrefs.SetFloat("Money", fractional);

            string line = "Valery";
            PlayerPrefs.SetString("Nick", line);

            PlayerPrefs.Save();
        }

        public void Load()
        {
            int playerAge = PlayerPrefs.GetInt("Age");

            float playerMoney = PlayerPrefs.GetFloat("Money");

            string playerNick = PlayerPrefs.GetString("Nick");
        }

        public void Clear()
        {
            if (PlayerPrefs.HasKey("Nick"))
                PlayerPrefs.DeleteKey("Nick");

            PlayerPrefs.DeleteAll();
        }
    }
}