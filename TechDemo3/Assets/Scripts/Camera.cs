using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Target;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;    
    
    void FixedUpdate()
    {       
        if(Target != null)
        {
            Vector3 targetPos = new Vector3(Target.position.x, Target.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, SmoothTime, Mathf.Infinity, Time.fixedDeltaTime);
        }      
    }
}
