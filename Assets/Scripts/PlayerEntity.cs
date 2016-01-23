using UnityEngine;
using System.Collections;

public class PlayerEntity : MonoBehaviour
{
	void Start ()
    {
        GameManager.instance.CmdRegisterPlayer(UserId.GetId());
    }
}
