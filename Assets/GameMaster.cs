using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject masterPrefab;
    public float renderDistance = 200;
    public static GameMaster instance;
    public List<PerlinMaster> missingChunks = new List<PerlinMaster>();
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
       if (missingChunks.Count > 0)
        {
            missingChunks[0].Activate();
            missingChunks.RemoveAt(0);
        }
    }
}
