using UnityEngine;

public static class SaveUtill
{
    public static void Save(string key, object playerData)
    {
        var save = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(key, save);
        PlayerPrefs.Save();
    }

    public static T GetSave<T>(string key)
    {
        return PlayerPrefs.HasKey(key) ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(key)) : default;
    }
}