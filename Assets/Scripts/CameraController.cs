using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public  Vector3 OffsetVector = Vector3.zero;

    void FixedUpdate()
    {
        transform.LookAt(Target.transform.position);
        transform.position = Target.transform.position + OffsetVector;
    }
}
