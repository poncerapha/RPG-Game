using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        target = PlayerController.instance.transform;
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(target.position.x, target.position.y,this.transform.position.z);
    }
}
