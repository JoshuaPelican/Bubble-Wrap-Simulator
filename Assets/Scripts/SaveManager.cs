using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string SAVE_FILE_NAME = "save.txt";
    string SaveFilePath => Application.persistentDataPath + '/' + SAVE_FILE_NAME;

    public bool HasSaveData { get { return File.Exists(SaveFilePath); } }

    public void Save(string worldData)
    {
        Debug.Log("Saving World Data...");
        File.WriteAllText(SaveFilePath, worldData);
    }

    public string Load()
    {
        Debug.Log("Loading World Data...");
        return File.ReadAllText(SaveFilePath).Trim();
    }

    [ContextMenu("DeleteSaveData")]
    public void DeleteSaveData()
    {
        Debug.Log("Deleting Saved World Data...");
        File.Delete(SaveFilePath);
    }
}
