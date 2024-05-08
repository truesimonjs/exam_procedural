using UnityEngine;

public class utillity : MonoBehaviour
{
    public static void DirectionLoop(int i, out int x, out int y)
    {
        if (i < 1 || i > 4) Debug.LogError("directionloop got an integer outside of range, only use integeres between 1 and 4");
        int uneven = (i % 2); 
        x = (i - 2) * uneven;
        y = (i - 3) * (uneven - 1) * -1;
        /*
         x on 1 and 3
        1 -2= -1
        3 -2= 1 
        y on 2 and 4
        2-3 = -1
        4-3=1

          */

    }
}
