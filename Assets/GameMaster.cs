using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameMaster : MonoBehaviour
{
    public GameObject masterPrefab;
    public float renderDistance = 200;
    public static GameMaster instance;
    public List<PerlinMaster> missingChunks = new List<PerlinMaster>();
    private Task chunkTask;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (missingChunks.Count > 0&& (chunkTask ==null || chunkTask.IsCompleted))
        {

           chunkTask = missingChunks[0].Activate();
            missingChunks.RemoveAt(0);
        }
    }
    
}
