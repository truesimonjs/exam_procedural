using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading.Tasks;

public class PerlinNoise : MonoBehaviour
{
    
   
    public float Frequency;
    public float FrameLimit = 1;
    public async Task<float[,]> AdjustPerlin(int sizeX, int sizeZ)
    {
        float trueFrequency  = Frequency / 100;
        float[,] map = new float[sizeX, sizeZ];
             
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {

                    if (stopwatch.Elapsed.TotalSeconds > FrameLimit)
                    {
                        stopwatch.Restart();
                        UnityEngine.Debug.Log("stopped frame");
                        await Task.Yield();
                    }
                    float nx = (float)x;
                    float nz = (float)z;
                    float noise = Mathf.PerlinNoise(nx * trueFrequency, nz * trueFrequency);
                    map[x, z] = noise;
                    

                }


            }
        return map;
    }
}
