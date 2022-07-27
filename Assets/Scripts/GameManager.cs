using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ChunkGenerator ChunkGenerator;
    [SerializeField] SaveManager SaveManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveManager.DeleteSaveData();
        }
    }

    private void Awake()
    {
        LoadWorld();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveWorld();
        }
    }


    void InitializeWorld()
    {
        ChunkGenerator.InitializeWorld();
    }

    void LoadWorld()
    {
        if (!SaveManager.HasSaveData)
        {
            InitializeWorld();
        }
        else
        {
            string worldData = SaveManager.Load();
            ChunkGenerator.GenerateWorld(worldData);
        }
    }

    void SaveWorld()
    {
        SaveManager.Save(ChunkGenerator.WorldData);
    }
}
