using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PerlinTest : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public float waterLevel = 0.5f;
    public float Frequency;
    private float trueFrequency = 1.1f;
    public float FrameLimit = 1;
  
    //
    public GameObject prefab;
    public GameObject[,] objects;
    //material
    public Material ground;
    public Material Water;

    private void Start()
    {
        objects = new GameObject[xSize, zSize];


        StartCoroutine(GeneratePerlin());
    }
    public IEnumerator GeneratePerlin()
    {
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
                objects[x, z] = Instantiate(prefab, transform);
                if (stopwatch.Elapsed.TotalSeconds > FrameLimit)
                {
                    
                    stopwatch.Restart();
                    UnityEngine.Debug.Log("stopped frame while instantiatingg  ");
                    yield return new WaitForSeconds(0);
                }

            }
        }
        StartCoroutine(AdjustPerlin());

    }
    public IEnumerator AdjustPerlin()
    {

        if (objects != null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int x = 0; x < objects.GetLength(0); x++)
            {
                for (int z = 0; z < objects.GetLength(1); z++)
                {
                    
                    if (stopwatch.Elapsed.TotalSeconds > FrameLimit)
                    {
                        stopwatch.Restart();
                        UnityEngine.Debug.Log("stopped frame");
                        yield return new WaitForSeconds(0);
                    }
                    float nx = (float)x;
                    float nz = (float)z;
                    float noise = Mathf.PerlinNoise(nx * trueFrequency, nz * trueFrequency);
                    GameObject current = objects[x, z];
                    current.transform.position = new Vector3(x * 1.1f, noise*10, z * 1.1f);
                    current.GetComponent<MeshRenderer>().material = noise <= waterLevel ? Water : ground;

                }


            }
        }
    }
    private void OnValidate()
    {
        trueFrequency = Frequency / 100;
        StartCoroutine(AdjustPerlin());

    }

}
