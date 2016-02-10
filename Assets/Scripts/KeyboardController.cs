using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(CustomCharacterController))]
public class KeyboardController : NetworkBehaviour 
{
    private CustomCharacterController _controller;
    private CameraController _camController;

    private Vector2 _prevMouseLoc = Vector2.zero;

    void Awake()
    {
        _controller = GetComponent<CustomCharacterController>();
    }

    void Start()
    {
        _camController = _controller.GetCameraController();
        _prevMouseLoc = Input.mousePosition;
    }

    void FixedUpdate()
    {
        //No input handling if not local player
        if (!isLocalPlayer || !_controller.CanMove())
            return;
        Vector3 dir = Vector3.zero;
        dir.z += Input.GetAxis("Vertical");
        dir.x += Input.GetAxis("Horizontal");

        if(Input.GetMouseButton(0) || Input.mouseScrollDelta.y != 0)
        {
            Vector2 mousePosDiff = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _prevMouseLoc;
            mousePosDiff.Normalize();
                _camController.Move(mousePosDiff);

            _prevMouseLoc = Input.mousePosition;
        }

        _controller.SetRunning(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        _controller.MoveCharacter(dir);
    }
}
