using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] LayerMask ChunkLayerMask;

    bool chunkL, chunkR, chunkT, chunkB;
    GameObject chunkObject;

    const float CHUNK_SIZE = 20f;

    private void Awake()
    {
        chunkObject = transform.GetChild(0).gameObject;


    }

    bool CheckNeighborChunk(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, transform.localScale.x * CHUNK_SIZE, ChunkLayerMask);
    }

    public void Enable(bool enable)
    {
        chunkObject.SetActive(enable);
    }

    public List<Vector2> ChunksToSpawn()
    {
        if (chunkL && chunkR && chunkT && chunkB)
            return new List<Vector2>();

        chunkL = CheckNeighborChunk(Vector2.left);
        chunkR = CheckNeighborChunk(Vector2.right);
        chunkT = CheckNeighborChunk(Vector2.up);
        chunkB = CheckNeighborChunk(Vector2.down);

        List<Vector2> missingChunkLocations = new List<Vector2>();

        if (!chunkL) missingChunkLocations.Add(Vector2.left);
        if (!chunkR) missingChunkLocations.Add(Vector2.right);
        if (!chunkT) missingChunkLocations.Add(Vector2.up);
        if (!chunkB) missingChunkLocations.Add(Vector2.down);

        return missingChunkLocations;
    }
}
