using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple script that make things rotate around themselves.
/// </summary>
public class RotateAround : MonoBehaviour
{
    public float RotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, RotationSpeed * Time.deltaTime);
    }
}
