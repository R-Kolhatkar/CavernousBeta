using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MoveType
    {
        USETRANSFORM,
        USEPHYSICS
    }

    public MoveType moveTypes;
    
    public Transform[] pathPoints;
    public int currentPath = 0;
    public float reachDistance = 5.0f;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(moveTypes)
        {
            case MoveType.USETRANSFORM:
                UseTransform();
                break;
            case MoveType.USEPHYSICS:
                UsePhysics();
                break;
        }
    }

    void UseTransform()
    {
        Vector3 dir = pathPoints[currentPath].position - transform.position;
        Vector3 dirNorm = dir.normalized;

        transform.Translate(dirNorm * speed);
        
        if(dir.magnitude <= reachDistance)
        {
            currentPath++;
            if(currentPath >= pathPoints.Length)
            {
                currentPath = 0;
            }
        }
    }

    void UsePhysics()
    {

    }

    void OnDrawGizmos()
    {
        if(pathPoints.Length == null)
        {
            return;
        }
        foreach(Transform pathPoint in pathPoints)
        {
            if(pathPoint)
            {
                Gizmos.DrawSphere(pathPoint.position, reachDistance);
            }
        }
    }
}
