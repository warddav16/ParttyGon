using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public GameObject Target;
    private Vector3 OffsetVector = Vector3.zero;

    void Awake()
    {
        OffsetVector = transform.position - Target.transform.position;
        transform.parent = null;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.LookAt(Target.transform.position);
        transform.position = Target.transform.position + OffsetVector;
    }
}
