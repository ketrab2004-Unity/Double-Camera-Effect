using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraFocus : MonoBehaviour
{
    public Camera left;
    public Camera right;
    public Transform middle;

    public DepthOfField dof;
    
    [Space(10)]
    
    [Tooltip("x rotation offset for cameras to fight z-fighting")]
    public float xOffset = .001f;

    public float lerp = .1f;

    public float dofRange = 15f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit cast;
        bool hit = Physics.Raycast(middle.position, middle.forward, out cast, right.farClipPlane);
        float dist = cast.distance;

        dof.gaussianStart.min = Mathf.Lerp(dof.gaussianStart.min, dist - dofRange, lerp);
        dof.gaussianEnd.min = Mathf.Lerp(dof.gaussianEnd.min, dist + dofRange, lerp);

        Quaternion leftQ;
        Quaternion rightQ;
        
        if (hit)
        {
            rightQ = Quaternion.Euler(0, Mathf.Atan2(-right.transform.localPosition.x, dist)*Mathf.Rad2Deg, 0) * Quaternion.Euler(xOffset,0,0);
            leftQ = Quaternion.Euler(0, Mathf.Atan2(-left.transform.localPosition.x, dist)*Mathf.Rad2Deg, 0) * Quaternion.Euler(-xOffset,0,0);
            
            Debug.DrawLine(middle.position, cast.point, Color.green);
        }
        else
        {
            rightQ = Quaternion.Euler(xOffset, 0, 0);
            leftQ = Quaternion.Euler(-xOffset, 0, 0);
            
            Debug.DrawLine(middle.position, middle.position + middle.forward*right.farClipPlane, Color.red);
        }
        
        left.transform.localRotation = Quaternion.Slerp(left.transform.localRotation,leftQ, lerp);
        right.transform.localRotation = Quaternion.Slerp(right.transform.localRotation, rightQ, lerp);
        
        Debug.DrawLine(left.transform.position, left.transform.position + left.transform.forward*left.farClipPlane);
        Debug.DrawLine(right.transform.position, right.transform.position + right.transform.forward*right.farClipPlane);
    }
}
