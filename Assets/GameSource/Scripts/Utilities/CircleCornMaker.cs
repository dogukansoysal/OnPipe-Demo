using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
/// <summary>
/// This class has been used as an one time Editor script to create a perfect circle of corn parts.
/// Can be used on different purposes in the future work.
/// </summary>
public class CircleCornMaker : MonoBehaviour
{
    public Transform CornPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCorn();
    }

    public void SpawnCorn()
    {
        for (int i = 0; i < 18; i++)
        {
            var corn = Instantiate(CornPrefab);
            corn.position = Vector3.zero + (Vector3.forward * 0.5f);
            corn.RotateAround(Vector3.zero, Vector3.up, (360/18) * i);
        }
    }
}
