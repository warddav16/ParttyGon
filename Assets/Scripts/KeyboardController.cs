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
        if (!isLocalPlayer)
            return;
        Vector3 dir = Vector3.zero;
        dir.z += Input.GetAxis("Vertical");
        dir.x += Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            _controller.RunCharacter();

                
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _controller.RunCoolDown();
        }
        _controller.MoveCharacter(dir);
    }
}
