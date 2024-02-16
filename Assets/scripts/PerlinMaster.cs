using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class PerlinMaster : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public float waterLevel = 0.5f;
    public bool sameHeight = false;
    //perlin noises
    public PerlinNoise perlinNoise;
    public PerlinNoise waterPerlin;
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
    public void GeneratePerlin()
    {
        //float[,] waterMap = waterPerlin.GeneratePerlin(xSize, zSize);
        float[,] map =  perlinNoise.GeneratePerlin(xSize,zSize);

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                bool isBelowWater = map[x, z] <= waterLevel;
                if (isBelowWater||sameHeight) map[x, z] = waterLevel;
                objects[x, z].transform.position = new Vector3(objects[x, z].transform.position.x, map[x,z]*1, objects[x, z].transform.position.z);
                
                objects[x,z].GetComponentInChildren<MeshRenderer>().material = isBelowWater ? Water : ground;

            }
        }
       // StaticBatchingUtility.Combine(this.gameObject);
        
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
                objects[x, z] = Instantiate(prefab, new Vector3(x,0,z),Quaternion.identity,this.transform);
                

            }
        }
    }
    
    public void OnValidate()
    {
        if (objects != null) GeneratePerlin();

    }
}