using UnityEngine;

public class EndlessChunkGenerator : MonoBehaviour
{
    [SerializeField] GameObject ChunkPrefab;
    [SerializeField] Transform ChunkParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Chunk chunk = collision.GetComponent<Chunk>();
        chunk.Enable(true);
        
        foreach (Vector3 chunkLocation in chunk.ChunksToSpawn())
        {
            Instantiate(ChunkPrefab, chunk.transform.position + (20f * ChunkPrefab.transform.localScale.x * chunkLocation), Quaternion.identity, ChunkParent);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Chunk chunk = collision.GetComponent<Chunk>();
        chunk.Enable(false);
    }
}
