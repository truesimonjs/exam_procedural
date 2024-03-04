using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utillity : MonoBehaviour
{
    public static void DirectionLoop(int i, ref int x, ref int z)
    {
        if (i < 1 || i > 4) Debug.LogError("directionloop got an integer outside of range, only use integeres between 1 and 4");
        //x on 1 and 3 y on 2 and 4
        /*
        int newX =  (i - 2) * (i % 2);
        int newY =  (i - 3) * (i % 2 - 1) * -1;
        */
        
        
        int ueven = (i % 2); //even numbers affect y uneven numbers affect x
        int newX = (i - 2) * ueven;
        int newY = (i - 3) * (ueven - 1) * -1;


    }
}
