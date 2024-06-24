using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "perlinGen", menuName = "ScriptableObjects/PerlinNoise", order = 1)]
public class PerlinNoise : ScriptableObject
{

    public Vector2 seed;
    public bool ridged = false;
    public float Frequency;
    public float min;
    public int octaves = 1;
    //public float[,] map;
    public List<Vector2> splines = new List<Vector2>();
    public int xSize;
    public int ySize;

    public float getPerlinCoord(uint xCoord, uint yCoord)
    {

        //if (map == null || xCoord >= map.GetLength(0) || yCoord >= map.GetLength(1)) GeneratePerlin((int)xCoord+100, (int)yCoord+100);
        
    


        float nx = (float)xCoord + seed.x;
        float ny = (float)yCoord + seed.y;
        float noise = 0;
        float trueFrequency = Frequency / 100;
        for (int i = 1; i <= octaves; i++)
        {
            noise += Mathf.PerlinNoise(nx * trueFrequency * i, ny * trueFrequency * i) / octaves;

        }

        if (ridged) noise = Mathf.Abs((noise - 0.5f) * 2);
        noise = UseSplines(noise);
        return noise;


    }
  
    
    public float UseSplines(float input)
    {
        if (splines.Count == 0) return input;
        int startSpline = 0;
        for (int i = 1; i < splines.Count - 1; i++)
        {
            if (splines[i].x > splines[startSpline].x && splines[i].x < input) startSpline = i;

        }
        float t = Mathf.InverseLerp(splines[startSpline].x, splines[startSpline + 1].x, input);

        return (int)Mathf.Lerp(splines[startSpline].y, splines[startSpline + 1].y, t);
        
    }
    private void OnValidate()
    {
        //GameObject.FindFirstObjectByType<PerlinMaster>()?.OnValidate();
        PerlinMaster[] masters = GameObject.FindObjectsByType<PerlinMaster>(FindObjectsSortMode.None);
        foreach (PerlinMaster master in masters)
        {
            master.OnValidate();
        }
    }
}

