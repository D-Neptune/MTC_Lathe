using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData (string student, string studentNumber, bool lang, string scene, bool isTutorial, ComponentHint componentHint)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LatheRight.CEED";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataHandler data = new DataHandler(lang, student, studentNumber, scene, isTutorial, componentHint);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveData(string student, string studentNumber, bool lang, string scene, bool isTutorial, anim currentAnim)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LatheRight.CEED";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataHandler data = new DataHandler(lang, student, studentNumber, scene, isTutorial, currentAnim);

        formatter.Serialize(stream, data);
        stream.Close();
    }



    public static DataHandler LoadData()
    {
        string path = Application.persistentDataPath + "/LatheRight.CEED";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataHandler data = formatter.Deserialize(stream) as DataHandler;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save File not found in " + path);
            return null;
        }

    }

    public static bool SaveFileExists()
    {
        string path = Application.persistentDataPath + "/LatheRight.CEED";
        return File.Exists(path);
    }

    public static void DeleteFileExists()
    {
        string path = Application.persistentDataPath + "/LatheRight.CEED";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
