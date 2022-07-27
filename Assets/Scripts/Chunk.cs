using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    bool chunkL, chunkR, chunkT, chunkB;
    [SerializeField] GameObject chunkObject;
    [SerializeField] Transform bubbleParent;

    const float CHUNK_SIZE = 20f;

    [SerializeField] Bubble[] bubbles;

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

        if (!chunkL) missingChunkLocations.Add(Vector2.left * CHUNK_SIZE);
        if (!chunkR) missingChunkLocations.Add(Vector2.right * CHUNK_SIZE);
        if (!chunkT) missingChunkLocations.Add(Vector2.up * CHUNK_SIZE);
        if (!chunkB) missingChunkLocations.Add(Vector2.down * CHUNK_SIZE);

        return missingChunkLocations;
    }

    bool CheckNeighborChunk(Vector2 direction)
    {
        return Physics2D.Raycast(transform.position, direction, transform.localScale.x * CHUNK_SIZE, 1 << 6);
    }

    public void SetPoppedBubblesInChunk(int[] indicies)
    {
        foreach (int index in indicies)
        {
            bubbles[index].SetAsPopped();
        }
    }

    public string ChunkData
    {
        get
        {
            string chunkData = "";

            chunkData += transform.position.x.ToString() + ',';
            chunkData += transform.position.y.ToString() + ',';

            foreach (Bubble bubble in bubbles)
            {
                chunkData += (bubble.IsPopped ? 1 : 0).ToString();
            }

            chunkData.TrimEnd(',');

            return chunkData;
        }
    }
}
