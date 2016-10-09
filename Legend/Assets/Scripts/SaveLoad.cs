using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + GameManager.user.name + ".PUFFYDATA");
        bf.Serialize(file, GameManager.user);
        file.Close();
    }

    public static bool Load(string name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + name + ".PUFFYDATA"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".PUFFYDATA", FileMode.Open);
            GameManager.user = (User)bf.Deserialize(file);
            file.Close();
            return true;
        }
        return false;
    }
}