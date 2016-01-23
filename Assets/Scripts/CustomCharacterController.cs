using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CustomCharacterController : NetworkBehaviour 
{
    private float _moveSpeed = 10.0f;
    public float walkSpeed = 10.0f;
    public float runSpeed = 20.0f;
    public float TurnSpeed = 20.0f;
    public float runDuration = 3.0f;
    public float coolDownTime = 3.0f;
    public enum MovementState { run, idle, walk };
    public MovementState moveState = MovementState.idle;
    public float runtimer = 0.0f;
    private Rigidbody _rigidbody;
    private int coolDownNegator = 1;
    private bool CanMove = true;

    public CameraController cameraController;

    [ClientRpc]
    public void RpcSetPosition(Vector3 pos)
    {
        _rigidbody.MovePosition(pos);
    }

    [ClientRpc]
    public void RpcSetCanMove(bool canMove)
    {
        CanMove = canMove;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //No input handling if not local player
        if (!isLocalPlayer || !CanMove)
            return;
        if(_rigidbody.velocity.magnitude > 0)
        {
            moveState = _moveSpeed > walkSpeed ? MovementState.run : MovementState.walk;
        }
        else
        {
            moveState = MovementState.idle;
        }
         //runtimer += moveState == MovementState.run ? Time.deltaTime : -Time.deltaTime;
        runtimer += Time.deltaTime * coolDownNegator;
        if(runtimer >= runDuration)
        {
            StopRun();
        }
        else if(runtimer < 0)
        {
            runtimer = 0;
        }
    }

    public void MoveCharacter( Vector3 dir )
    {
        //TODO: Smoothly move the forward towards dir
        if (dir != Vector3.zero)
        {
            Vector3 toCamera = transform.position - cameraController.transform.position;
            toCamera.Normalize();
            toCamera.y = 0;  
            dir += toCamera;
            dir.Normalize();
            transform.forward = dir;
            _rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * _moveSpeed);
        }
    }

    public void RunCharacter()
    {
        coolDownNegator = 1;
        if (runtimer <= runDuration)
        {
            _moveSpeed = runSpeed; //TODO: speed change smoothly
        }
        else
            StopRun();
    }

    private void StopRun()
    {
        _moveSpeed = walkSpeed; //Todo stop smoothly
    }

    public void RunCoolDown()
    {
        coolDownNegator = -1;
        runtimer = coolDownTime;
        StopRun();
    }
}
