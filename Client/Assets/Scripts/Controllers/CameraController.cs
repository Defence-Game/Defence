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
        minPos = new Vector2(GameScene.LimitXDown + cameraHalfWidth, GameScene.LimitYDown + cameraHalfHeight);
        maxPos = new Vector2(GameScene.LimitXUp - cameraHalfWidth, GameScene.LimitYUp - cameraHalfHeight);
    }
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        
        targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

        transform.position = targetPos;
    }
}
