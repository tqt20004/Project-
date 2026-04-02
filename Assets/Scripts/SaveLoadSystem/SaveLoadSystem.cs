using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    // Phải có static ở biến path
    private static string path = Application.persistentDataPath + "/player_save.json";

    // Phải có static ở hàm Save
    public static void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, json);
        Debug.Log("Lưu file thành công tại: " + path);
    }

    // Phải có static ở hàm Load
    public static GameData LoadGame()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        return new GameData();
    }
}
