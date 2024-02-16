using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMaker : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public GameObject ChunkPrefab;
    public GameObject[,] Chunks;
    private void Start()
    {
        Chunks = new GameObject[xSize,zSize];
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                Debug.Log("working");
                Chunks[x, z] = Instantiate(ChunkPrefab, new Vector3((x+1)*16, 0, (z+1)*16), Quaternion.identity, this.transform);


            }
        }
    }


}
