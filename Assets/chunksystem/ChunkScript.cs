using System.Threading.Tasks;
using UnityEngine;

public class ChunkScript : MonoBehaviour
{
    public static int size = 16;
    private GameObject[,] myBlocks = new GameObject[16, 16];
    public GameObject prefab;
    public PerlinNoise noisemap;
    [HideInInspector] public bool Priority = false;
    private void Start()
    {
        GenerateChunk();

    }

    private async void GenerateChunk()
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                if (!Priority) await Task.Yield();
                myBlocks[x, z] = Instantiate(prefab, new Vector3(), Quaternion.identity, transform);


                myBlocks[x, z].transform.SetLocalPositionAndRotation(Vector3.forward * z + Vector3.right * x, Quaternion.identity);
                float xPos = myBlocks[x, z].transform.position.x;
                float zPos = myBlocks[x, z].transform.position.z;
                float noise = noisemap.getPerlinCoord((uint)xPos, (uint)zPos);

                myBlocks[x, z].transform.position = new Vector3(xPos, noise, zPos);
            }
        }
    }
}
