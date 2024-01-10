using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public float startZ = -10f;
    public float endZ = -10f;
    public float speed = 0.5f;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, endZ), step);
    }
}
