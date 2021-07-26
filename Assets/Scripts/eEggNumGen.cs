using JetBrains.Annotations;

using Sirenix.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eEggNumGen : MonoBehaviour
{
    public int aRange;
    public int ID;
    public bool active;
    public void isThereEgg() 
    {
        int[] Random = returnNum();
        active = returnEgg(Random);
        ID = UnityEngine.Random.Range(0,3);
    }
    public int[] returnNum() 
    {
        return new int[] { UnityEngine.Random.Range(0, 1000000), UnityEngine.Random.Range(0, 1000000)}; 
    }
    public bool returnEgg(int[] nums) 
    {
        return nums[0] > nums[1] - aRange || nums[0] < nums[1] + aRange;
    }
}
