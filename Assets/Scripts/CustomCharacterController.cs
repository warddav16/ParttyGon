﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CustomCharacterController : NetworkBehaviour 
{
    public float WalkSpeed = 10.0f;
    public float RunSpeed = 20.0f;
    public float TurnSpeed = 20.0f;
    public float runDuration = 3.0f;
    public enum MovementState { run, idle, walk };
    public MovementState moveState = MovementState.idle;
    public float runtimer = 0.0f;
    private Rigidbody _rigidbody;
    private bool canMove = true;
    public float Acceleration = 3.0f;
    bool isRunning = false;

    public bool CanMove() { return canMove; }

    public GameObject CameraControllerPrefab;
    private CameraController cameraController;
    public CameraController GetCameraController() { return cameraController; }

    [ClientRpc]
    public void RpcSetPosition(Vector3 pos)
    {
        _rigidbody.MovePosition(pos);
    }

    [ClientRpc]
    public void RpcSetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (!isLocalPlayer)
            return;

        GameObject camObject = Instantiate(CameraControllerPrefab) as GameObject;
        cameraController = camObject.GetComponent<CameraController>();
        cameraController.Target = gameObject;
    }

    public void MoveCharacter( Vector3 dir )
    {
        //TODO: Smoothly move the forward towards dir
        if (dir != Vector3.zero)
        {
            dir = cameraController.transform.TransformDirection(dir);
            dir.y = 0;
            dir.Normalize();
            transform.forward = dir;
            float speed = isRunning ? RunSpeed : WalkSpeed;
            _rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
    }
}
