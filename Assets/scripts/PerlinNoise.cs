using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading.Tasks;
[CreateAssetMenu(fileName = "perlinGen", menuName = "ScriptableObjects/PerlinNoise", order = 1)]
public class PerlinNoise : ScriptableObject
{
   
    public Vector2 seed;
    public bool ridged = false;
    public float Frequency;
    public float min;
    private float[,] map;
    public List<Vector2> splines = new List<Vector2>();
    public float getPerlinCoord(uint xCoord,uint yCoord)
    {
        if (xCoord > map.GetLength(0) || yCoord > map.GetLength(1)) GeneratePerlin((int)xCoord, (int)yCoord);
        return map[xCoord, yCoord];

    }
    public float[,] GeneratePerlin(int sizeX, int sizeY)
    {
        float trueFrequency  = Frequency / 100;
        float[,] newMap = new float[sizeX, sizeY];
             
          
            for (int x = 0; x < newMap.GetLength(0); x++)
            {
                for (int z = 0; z < newMap.GetLength(1); z++)
                {


                    float nx = (float)x+seed.x;
                    float ny = (float)z+seed.y;
                    float noise = Mathf.PerlinNoise(nx * trueFrequency, ny * trueFrequency);
                    if (ridged) noise = Mathf.Abs( (noise - 0.5f) * 2);
                     noise = UseSplines(noise);
                    newMap[x, z] = noise;
                    

                }


            }
        map = newMap;
        return newMap;
    }
    public float UseSplines(float input)
    {
        if (splines.Count == 0) return input;
        int startSpline = 0;
        for (int i = 1; i < splines.Count-1; i++)
        {
            if (splines[i].x > splines[startSpline].x && splines[i].x<input) startSpline = i;

        }
        float t = Mathf.InverseLerp(splines[startSpline].x, splines[startSpline + 1].x, input);

        return (int)Mathf.Lerp(splines[startSpline].y, splines[startSpline + 1].y,t);

    }
    private void OnValidate()
    {
        GameObject.FindFirstObjectByType<PerlinMaster>().OnValidate();
       
    }
}
