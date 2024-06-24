using UnityEngine;

public class ChunkRender : MonoBehaviour
{
    
    public Transform player;
    PerlinMaster perlinMaster;
    private void Start()
    {
        perlinMaster = GetComponent<PerlinMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {

        if (!isWithinRender()) return;

        for (int i = 1; i < 5; i++)
        {
            
            utillity.DirectionLoop(i, out int x, out int z);
            Debug.Log(x + " " + z);

            x += perlinMaster.chunkPos.x;
            z += perlinMaster.chunkPos.y;
            Vector3 noHeiDist = new Vector3(x * 100 - player.position.x, 0, z * 100 - player.position.z);
            if (Vector3.Distance(Vector3.zero, noHeiDist) > GameMaster.instance.renderDistance) continue;
            if (x < 0 || z < 0) continue;

            if (PerlinMaster.chunks[x, z] == null)
            {
                PerlinMaster Pm = Instantiate(GameMaster.instance.masterPrefab, new Vector3(x * 100, 0, z * 100), Quaternion.identity).GetComponent<PerlinMaster>();
                Pm.chunkPos = new Vector2Int(x, z);

            }
            else PerlinMaster.chunks[x, z].gameObject.SetActive(true);
            
        }

    }
    private bool isWithinRender()
    {
        Vector3 noHeiDist = new Vector3(transform.position.x - player.position.x, 0, transform.position.z - player.position.z);
        bool withinRender = Vector3.Distance(Vector3.zero, noHeiDist) <= GameMaster.instance.renderDistance;
        if (withinRender) return true;
        gameObject.SetActive(false);
        return false;
        

            
        

    }
}
