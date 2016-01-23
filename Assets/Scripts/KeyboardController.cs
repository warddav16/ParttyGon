using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomCharacterController))]
public class KeyboardController : NetworkBehaviour 
{
    private CustomCharacterController _controller;
    void Awake()
    {
        _controller = GetComponent<CustomCharacterController>();
    }

    void Update()
    {
        //No input handling if not local player
        if (!isLocalPlayer || !_controller.CanMove())
            return;
        Vector3 dir = Vector3.zero;
        dir.z += Input.GetAxis("Vertical");
        dir.x += Input.GetAxis("Horizontal");


        _controller.SetRunning(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        _controller.MoveCharacter(dir);
    }
}
