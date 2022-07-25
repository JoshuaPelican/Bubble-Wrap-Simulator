using UnityEngine;

public class EndlessChunkGenerator : MonoBehaviour
{
    [SerializeField] GameObject ChunkPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Chunk chunk = collision.GetComponent<Chunk>();
        chunk.Enable(true);
        foreach (Vector3 chunkLocation in chunk.ChunksToSpawn())
        {
            Debug.Log(chunkLocation);
            Instantiate(ChunkPrefab, chunk.transform.position + (chunkLocation * 20f), Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Chunk chunk = collision.GetComponent<Chunk>();
        chunk.Enable(false);
    }
}
