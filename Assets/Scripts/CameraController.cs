using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    private void LateUpdate()
    {
        transform.position = Target?.position ?? Vector2.zero;
        transform.position += Offset;
    }
}
