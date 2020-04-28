using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class StateManager
{
    [System.Serializable]
    private class State
    {
        public List<string> levelsDone;

        public State() {
            levelsDone = new List<string>();
        }
    }
    private const string FILE_NAME = "/progres.gd";

    private static readonly StateManager instance = new StateManager();

    private State state;

    private StateManager()
    {
        Load();
    }

    private void Save()
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + FILE_NAME);

        binaryFormatter.Serialize(file, state);
        file.Close();
    }

    private void Load()
    {
        var path = Application.persistentDataPath + FILE_NAME;

        if (File.Exists(path))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(path, FileMode.Open);

            state = binaryFormatter.Deserialize(file) as State;
            file.Close();
        }

        if (state == null)
        {
            state = new State();
        }
    }

    public static StateManager GetInstance()
    {
        return instance;
    }

    public void MarkLevelAsFinished(string sceneName)
    {
        state.levelsDone.Add(sceneName);
        Save();
    }
}
