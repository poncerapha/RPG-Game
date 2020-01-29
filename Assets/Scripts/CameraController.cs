using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap tilemap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;
    void Start()
    {
        target = PlayerController.instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = tilemap.localBounds.min + new Vector3(halfWidth,halfHeight,0f);
        topRightLimit = tilemap.localBounds.max + new Vector3(-halfWidth,-halfHeight,0f);

        PlayerController.instance.SetBounds(tilemap.localBounds.min,tilemap.localBounds.max);
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);

        //keep the camera inside the bounds
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(this.transform.position.y,bottomLeftLimit.y,topRightLimit.y),this.transform.position.z); 
    }
}
