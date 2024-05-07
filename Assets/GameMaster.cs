using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject masterPrefab;
    public static GameMaster instance;
    private void Awake()
    {
        instance = this;
    }
}
