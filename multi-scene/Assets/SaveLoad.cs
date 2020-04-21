using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    private const string FILE_NAME = "/progres.gd";

    public static GameData data = new GameData();

    public static void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_NAME);

        binaryFormatter.Serialize(file, SaveLoad.data);
        file.Close();
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + FILE_NAME;

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            SaveLoad.data = (GameData)binaryFormatter.Deserialize(file);
            file.Close();
        }
    }
}
