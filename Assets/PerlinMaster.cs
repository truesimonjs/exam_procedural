using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class PerlinMaster : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public float waterLevel = 0.5f;
    public PerlinNoise perlinNoise;
  
    //
    public GameObject prefab;
    public GameObject[,] objects;
    //material
    public Material ground;
    public Material Water;
    private void Start()
    {

        GenerateBlocks();
        GeneratePerlin();

    }
    public async void GeneratePerlin()
    {
        
        float[,] map;
        map =  await perlinNoise.AdjustPerlin(xSize,zSize);
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                objects[x, z].transform.position = new Vector3(objects[x, z].transform.position.x, map[x,z]*10, objects[x, z].transform.position.z);
                objects[x,z].GetComponent<MeshRenderer>().material = map[x,z] <= waterLevel ? Water : ground;
            }
        }

    }


    public void GenerateBlocks()
    {
        if (objects != null)
        foreach (GameObject element in objects)
        {
            Destroy(element);
        }

        objects = new GameObject[xSize, zSize];
     
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                objects[x, z] = Instantiate(prefab, new Vector3(x,0,z),Quaternion.identity);
                

            }
        }
    }
}
