using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAnim : MonoBehaviour
{
    public Transform centerTarget;
    public float orbitSpeed;
    Vector3 offset;


    private void Start()
    {
        offset = transform.position - centerTarget.position;

    }
    private void Update()
    {
        transform.position = centerTarget.position + offset;
        transform.RotateAround(centerTarget.position, Vector3.up, orbitSpeed*Time.deltaTime);
        offset = transform.position - centerTarget.position;
    }


}
