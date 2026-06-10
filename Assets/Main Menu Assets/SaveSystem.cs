using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem
{

    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.seb";
        FileStream stream = new FileStream(path, FileMode.Create);

        Health playerHealth = player.GetComponent<Health>();
        PlayerData data = new PlayerData(playerHealth);
        
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer () 
    {
        string path = Application.persistentDataPath + "/player.seb";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

}
