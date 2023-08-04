using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // -26 -16 29 16
    private Vector2 minPos;
    private Vector2 maxPos;
    private GameObject player;
    private float cameraHalfWidth, cameraHalfHeight;
    void Start()
    {
        player = GameScene.player;
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
        minPos = new Vector2(-26 + cameraHalfWidth, -16 + cameraHalfHeight);
        maxPos = new Vector2(29 - cameraHalfWidth, 16 - cameraHalfHeight);
    }
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        
        targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
        transform.position = targetPos;
    }
}
