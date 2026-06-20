using UnityEngine;

namespace AndroidParkingIdle.Core
{
    // Bu dosya, oyun ilerlemesini PlayerPrefs icinde JSON olarak kaydeder ve geri yukler.
    public static class SaveSystem
    {
        private const string SaveKey = "android_parking_idle_save";

        public static GameData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
            {
                return new GameData();
            }

            string json = PlayerPrefs.GetString(SaveKey);
            return string.IsNullOrWhiteSpace(json) ? new GameData() : JsonUtility.FromJson<GameData>(json);
        }

        public static void Save(GameData data)
        {
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public static void Reset()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.Save();
        }
    }
}
