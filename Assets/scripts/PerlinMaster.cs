using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class PerlinMaster : MonoBehaviour
{
    public bool generate = false;
    public int xSize;
    public int zSize;
    public float waterLevel = 0.5f;
    public bool sameHeight = false;
    //perlin noises
    public PerlinNoise perlinNoise;

    //
    public GameObject prefab;
    public GameObject[,] objects;
    private GameObject blockParent;
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
        //float[,] map =  perlinNoise.GeneratePerlin(xSize,zSize);

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                uint xPos = (uint)(x + transform.position.x);
                uint zPos = (uint)(z + transform.position.z);
                float height = perlinNoise.getPerlinCoord(xPos, zPos);
                bool isBelowWater = height <= waterLevel;
                if (isBelowWater||sameHeight) height = waterLevel;
                objects[x, z].transform.position = new Vector3(objects[x, z].transform.position.x, height, objects[x, z].transform.position.z);
                
                objects[x,z].GetComponentInChildren<MeshRenderer>().material = isBelowWater ? Water : ground;

            }
        }
       // StaticBatchingUtility.Combine(this.gameObject);
        
    }


    public void GenerateBlocks()
    {
        if (blockParent == null) blockParent = Instantiate(new GameObject(), transform);
        blockParent.name = "Generated Terrain";
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
                objects[x, z] = Instantiate(prefab, new Vector3(x+transform.position.x,0,z+transform.position.z),Quaternion.identity,blockParent.transform);
                

            }
        }
    }
    
    public void OnValidate()
    {
       if (generate)
        {
            generate = false;
            GenerateBlocks();
            GeneratePerlin();
        }
        
        if (Application.isPlaying&&objects != null) GeneratePerlin();

    }
}
