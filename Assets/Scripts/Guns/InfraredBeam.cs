using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraredBeam : MonoBehaviour
{
    private LineRenderer beam;
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {
        beam = GetComponent<LineRenderer>();
        beam.sortingOrder = 1;
        beam.material = new Material(Shader.Find("Sprites/Default"));
        beam.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void FixedUpdate()
    {
        //RaycastHit hit;

        beam.SetPosition(1, new Vector3(0, 0, range));
        //if (Physics.Raycast(transform.position, transform.forward, out hit))
        //{
        //    beam.SetPosition(1, new Vector3(0, 0, hit.distance));
        //    //Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 0, hit.distance), Color.blue);
        //}
    }
}
