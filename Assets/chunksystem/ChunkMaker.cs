using UnityEngine;

public class ChunkMaker : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public GameObject ChunkPrefab;
    public GameObject[,] Chunks;
    public Transform Player;
    public int renderDistance = 4;
    public int[] t;
    private void Start()
    {

        Chunks = new GameObject[xSize, zSize];


        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                Debug.Log("working");
                Chunks[x, z] = Instantiate(ChunkPrefab, new Vector3((x + 1) * 16, 0, (z + 1) * 16), Quaternion.identity, this.transform);


            }
        }
    }
    private void Update()
    {
        foreach (var item in Chunks)
        {
            if (!item.GetComponentInChildren<ChunkScript>().Priority)
            {
                Vector3 playerPos = new Vector3(Player.position.x, 0, Player.position.z);
                float distance = Vector3.Distance(playerPos, item.transform.position);

                item.SetActive(distance < renderDistance * 16 * 2);
                item.GetComponentInChildren<ChunkScript>().Priority = distance < renderDistance * 16;

            }

        }
    }


}
