using UnityEngine;

public static class SaveLoaderRecord
{
   public static void Save(string key, int saveData)
    {
        if (PlayerPrefs.HasKey(key))
            PlayerPrefs.DeleteKey(key);
        PlayerPrefs.SetInt(key, saveData);
    }

    public static int Load(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
            return 0;
    }
}
