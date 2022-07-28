using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] int AutoSaveInterval;

    [Header("Managers")]
    [SerializeField] ChunkGenerator ChunkGenerator;
    [SerializeField] SaveManager SaveManager;

    int t;

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

    private void FixedUpdate()
    {
        t++;
        if (t % AutoSaveInterval == 0)
        {
            SaveWorld();
            t = 0;
        }
    }

    void InitializeWorld()
    {
        Debug.Log("Initializing World...");
        ChunkGenerator.InitializeWorld();
    }

    void LoadWorld()
    {
        if (!SaveManager.HasSaveData)
        {
            Debug.Log("No Save Data Found!");
            InitializeWorld();
        }
        else
        {
            Debug.Log("Save Data Found!");
            string worldData = SaveManager.Load();
            ChunkGenerator.GenerateWorld(worldData);
        }
    }

    void SaveWorld()
    {
        SaveManager.Save(ChunkGenerator.WorldData);
    }
}
