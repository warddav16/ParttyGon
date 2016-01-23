using UnityEngine;
using System.Collections;

public class UserId
{
    private static string thisId = null;

    public static string GetId()
    {
        if (thisId == null)
        {
            thisId = Random.Range(0, 10000001).ToString(); // TODO: Replace with better id (from steam!)
        }

        return thisId;
    }
}
