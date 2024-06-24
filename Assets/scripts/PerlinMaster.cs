using UnityEngine;
using System.Diagnostics;
using System.Threading.Tasks;

public class PerlinMaster : MonoBehaviour
{
    public static PerlinMaster[,] chunks = new PerlinMaster[100, 100];
    public static int xSize = 100;
    public static int zSize = 100;
    //
    public Vector2Int chunkPos;
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
    private void Awake()
    {
        
    }
    private void Start()
    {
      
        transform.position = new Vector3(chunkPos.x * xSize, 0, chunkPos.y * zSize);
        GameMaster.instance.missingChunks.Add(this);

    }
    public async Task Activate()
    {
           Task task = GenerateBlocks();
           await task;
            GeneratePerlin();
    }
    public void GeneratePerlin()
    {

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                uint xPos = (uint)(x + transform.position.x);
                uint zPos = (uint)(z + transform.position.z);
                float height = perlinNoise.getPerlinCoord(xPos, zPos);
                bool isBelowWater = height <= waterLevel;
                if (isBelowWater || sameHeight) height = waterLevel;
                objects[x, z].transform.position = new Vector3(objects[x, z].transform.position.x, height, objects[x, z].transform.position.z);

                objects[x, z].GetComponentInChildren<MeshRenderer>().material = isBelowWater ? Water : ground;

            }
        }
        StaticBatchingUtility.Combine(blockParent);

    }


    public async Task GenerateBlocks()
    {
        const float frameLimit = 1000/30;
        if (blockParent == null)
        {
            blockParent = new GameObject();
            blockParent.transform.parent = transform;
            blockParent.transform.localPosition = Vector3.zero;
            blockParent.name = "Generated Terrain";

        }

        if (objects != null)
            foreach (GameObject element in objects)
            {
                Destroy(element);
            }

        objects = new GameObject[xSize, zSize];
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                objects[x, z] = Instantiate(prefab, new Vector3((x - xSize / 2) + transform.position.x, 0, (z - zSize / 2) + transform.position.z), Quaternion.identity, blockParent.transform);
                objects[x, z].name = "block";
                if (stopwatch.ElapsedMilliseconds > frameLimit)
                {
                    await Task.Yield();
                    stopwatch.Restart();
                }

            }
        }

    }

    public void OnValidate()
    {
        

        if (Application.isPlaying && objects != null) GeneratePerlin();

    }
}
