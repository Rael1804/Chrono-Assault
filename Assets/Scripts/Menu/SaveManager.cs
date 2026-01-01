using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager
{
 
    public static void SavePuntuacionData(string nombre, string tiempo, int oleada, int aliens)
    {
        Debug.Log("nombre: " + nombre);
        Debug.Log("tiempo: " + tiempo);
        Debug.Log("oleada: " + oleada);
        Debug.Log("aliens: " + aliens);

        SavePuntuacion save = new SavePuntuacion(nombre, tiempo, oleada, aliens);
        string dataPath = Application.persistentDataPath + "/Puntuaciones.save";
        FileStream filestream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(filestream, save);
        filestream.Close();
    }

    public static SavePuntuacion LoadDataPuntuacion()
    {
        string dataPath = Application.persistentDataPath + "/Puntuaciones.save";
        Debug.Log(dataPath);
        if (File.Exists(dataPath))
        {
            FileStream filestream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SavePuntuacion save = (SavePuntuacion)binaryFormatter.Deserialize(filestream);
            filestream.Close();
            return save;
        }
        else
        {
            return null;
        }
    }
}
