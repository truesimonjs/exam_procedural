using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRender : MonoBehaviour
{
   public GameObject[] NextTo = new GameObject[4];

    public Transform player;

    private void Update()
    {
        Vector3 playerPos = new Vector3(player.position.x, 0, player.position.z);
        if (Vector3.Distance(playerPos, transform.position)<32)
        {
            for (int i = 0; i < NextTo.Length; i++)
            {
                Debug.Log(NextTo[i] == null);
                if (NextTo[i] != null) NextTo[i].SetActive(true);
                else
                {
                    int x=0;
                    int z=0;
                    utillity.DirectionLoop(i + 1, ref x, ref z);
                   //NextTo[i] = Instantiate(this.gameObject,transform.position+ new Vector3(x*16,0,z*16), Quaternion.identity).gameObject;
                }
            }
        }
    }
}
