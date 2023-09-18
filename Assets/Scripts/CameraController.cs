using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [FormerlySerializedAs("CameraPos")] [SerializeField] private Vector3 cameraPos;
    [SerializeField] Transform player;
    [SerializeField] private float distanceFromOrigin;
    [SerializeField] private float cameraMoveSpeed;
    
    private Vector3 forward = new(0.0f, 0.0f, 1.0f);
    private Vector3 origin;

    private void Awake()
    {
        origin = cameraPos;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void LateUpdate()
    {
        transform.position = cameraPos;
    }

    private void MoveCamera()
    {
        // var playerDirection = (-cameraPos + player.position).normalized;
        // var distance = Vector2.Distance(cameraPos, player.position);
        // cameraPos += playerDirection * (distance * Time.deltaTime * cameraMoveSpeed);
        // cameraPos.z = origin.z;
        FollowPlayer();
        LimitMovement();
    }

    private void FollowPlayer()
    {
        cameraPos = Vector2.Lerp(origin, player.position, cameraMoveSpeed);
    }
                                                                                                                     
    private void LimitMovement()
    {
        cameraPos.x = Mathf.Clamp(cameraPos.x, -distanceFromOrigin, distanceFromOrigin);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -distanceFromOrigin, distanceFromOrigin);
        cameraPos.z = origin.z;
        // cameraPos.x = cameraPos.x > origin.x + distanceFromOrigin ? distanceFromOrigin : cameraPos.x;                
        // cameraPos.x = cameraPos.x < -(origin.x + distanceFromOrigin) ? -distanceFromOrigin : cameraPos.x;              
        // cameraPos.y = cameraPos.y > origin.y + distanceFromOrigin ? distanceFromOrigin : cameraPos.y;                
        // cameraPos.y = cameraPos.y < -(origin.y + distanceFromOrigin) ? -distanceFromOrigin : cameraPos.y;
    }
}
