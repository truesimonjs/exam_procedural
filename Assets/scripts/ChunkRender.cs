using UnityEngine;

public class ChunkRender : MonoBehaviour
{
    public float renderDistance = 100;
    public Transform player;
    PerlinMaster perlinMaster;
    public GameObject prefab;
    private void Start()
    {
        perlinMaster = GetComponent<PerlinMaster>();
    }
    private void Update()
    {
        Vector3 noHeiDist = new Vector3(transform.position.x-player.position.x, 0, transform.position.z-player.position.z);
        if (Vector3.Distance(Vector3.zero,noHeiDist) < renderDistance)
        {
            for (int i = 1; i < 5; i++)
            {
                int x, z;
                utillity.DirectionLoop(i, out x, out z);
                Debug.Log(x + " " + z);
                x += perlinMaster.chunkPos.x;
                z += perlinMaster.chunkPos.y;
                if (x < 0 || z < 0) continue;
                if (PerlinMaster.chunks[x, z] == null)
                {
                    Instantiate(prefab, new Vector3(x*100, 0, z*100), Quaternion.identity);
                }
            }
        }
    }
}
