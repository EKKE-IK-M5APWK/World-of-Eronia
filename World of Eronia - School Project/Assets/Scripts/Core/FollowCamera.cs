using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    Transform target;
    // Start is called before the first frame update


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
    }
}
