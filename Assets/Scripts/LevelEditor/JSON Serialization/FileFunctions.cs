using System.IO;
using UnityEngine;

public static class FileFunctions {
    public static string GetPath(string fileName) {
        if (!Directory.Exists(Application.streamingAssetsPath + "/Levels")) {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Levels");
        }
        return Application.streamingAssetsPath + "/Levels/" + fileName;
    }

    public static void WriteFile(string path, string content) {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream)) {
            writer.Write(content);
        }
    }

    public static string GetFile(string path) {
        if (File.Exists(path)) {
            using (StreamReader reader = new StreamReader(path)) {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    public static LevelData GetLevelDataJSON() {
        return JsonUtility.FromJson<LevelData>(FileFunctions.GetFile(FileFunctions.GetPath(LevelIDs.levelName)));
    }
}