using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {       
        if(Player != null)
        {
            Vector3 CameraPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, CameraPos, ref velocity, SmoothTime, Mathf.Infinity, Time.fixedDeltaTime);
        }
    }
}
