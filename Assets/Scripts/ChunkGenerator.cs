using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] GameObject ChunkPrefab;
    [SerializeField] Transform ChunkParent;

    readonly List<Chunk> chunks = new List<Chunk>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Chunk chunk))
            return;

        chunk.Enable(true);
        
        foreach (Vector3 chunkLocation in chunk.ChunksToSpawn())
        {
            SpawnChunk(chunk.transform.position + (ChunkPrefab.transform.localScale.x * chunkLocation));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Chunk chunk = collision.GetComponent<Chunk>();
        chunk.Enable(false);
    }

    void SpawnChunk(Vector2 position, int[] poppedBubbles = null)
    {
        Chunk chunk = Instantiate(ChunkPrefab, position, Quaternion.identity, ChunkParent).GetComponent<Chunk>();

        chunks.Add(chunk);

        if(poppedBubbles != null)
            chunk.SetPoppedBubblesInChunk(poppedBubbles);
    }

    public void InitializeWorld()
    {
        SpawnChunk(Vector2.zero);
    }

    public void GenerateWorld(string worldData)
    {
        string[] splitWorldData = worldData.Split("\n");

        foreach (string chunk in splitWorldData)
        {
            string[] chunkData = chunk.Split(",");

            Vector2 chunkPosition = new Vector2(float.Parse(chunkData[0]), float.Parse(chunkData[1]));
            string bubbleData = chunkData[2];

            int[] poppedIndexData = bubbleData.AllIndexesOf("1").ToArray();

            //Spawn the chunk (Position is first 2 values)
            //Tell the chunk to have certain parts popped;
            SpawnChunk(chunkPosition, poppedIndexData);
        }

        Debug.LogFormat("Generated {0} Chunks!", splitWorldData.Length);
    }

    public string WorldData 
    {
        get
        {
            string worldData = "";

            foreach (Chunk chunk in chunks)
            {
                if (chunk.ChunkData[2..(chunk.ChunkData.Length - 1)].All(x => x == '0'))
                    continue;

                worldData += chunk.ChunkData + "\n";
            }

            return worldData;
        } 
    }
}
