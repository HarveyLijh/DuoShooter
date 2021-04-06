using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] Transform transTarget;

    // Update is called once per frame
    void Update()
    {
        if (transTarget != null)
            transform.position = new Vector3(transform.position.x, transform.position.y, transTarget.position.z);
    }
}
