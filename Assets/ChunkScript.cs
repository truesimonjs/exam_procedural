using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkScript : MonoBehaviour
{
    public static int size = 4;
    private GameObject[,] myBlocks = new GameObject[16,16];
    public GameObject prefab;
    public PerlinNoise noisemap;
    private void Start()
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
              
                myBlocks[x, z] = Instantiate(prefab, new Vector3(), Quaternion.identity,transform);
                //-0.5 to adjust for size of blocks, +1 to adjust for the fact that it counts from 0 and not 1 = 0.5

                myBlocks[x,z].transform.SetLocalPositionAndRotation(Vector3.forward*(z-size/2+0.5f)+Vector3.right*(x-size/2+0.5f), Quaternion.identity);
                float xPos = myBlocks[x, z].transform.position.x;
                float zPos = myBlocks[x, z].transform.position.z;
                float noise = noisemap.getPerlinCoord((uint)xPos, (uint)zPos);
                myBlocks[x,z].transform.position = new Vector3(xPos, noise,zPos);
            }
        }
        
    }
}
