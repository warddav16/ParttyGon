using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float Offset = 4.0f; // Overriden in editor
    private Vector3 OffSetVector = Vector3.zero;
    public float RotateSpeed = 10.0f;

    void FixedUpdate()
    {
        Quaternion newRot = Quaternion.Euler(-OffSetVector.y, OffSetVector.x, 0);
        transform.rotation = newRot;
        Vector3 negativeDistace = new Vector3(0, 0, -Offset);
        transform.position = newRot * negativeDistace + Target.transform.position;
    }

    public void Move(Vector3 angles)
    {
        OffSetVector += angles * RotateSpeed * Time.deltaTime;
        OffSetVector.y = Mathf.Clamp(OffSetVector.y, -90f, 90f);
    }
}
