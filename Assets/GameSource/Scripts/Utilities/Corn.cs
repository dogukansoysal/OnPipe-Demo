using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour
{
    public GameObject SoloCornPrefab;
    public float explosionMagnitude;
    public float explosionRadius;
    
    /// <summary>
    /// Corn explosion effect handler
    /// TODO: Spawning in runtime decrease performance a lot. predefinition and activating at runtime should increase the performance.
    /// </summary>
    public void Explode()
    {
        transform.GetComponent<Renderer>().enabled = false;

        var soloCornParent = Instantiate(SoloCornPrefab);    // All corn part's parent GameObject
        soloCornParent.transform.position = transform.position;
        var explosionPos = transform.position;
        
        // Loop through all the corn parts to color them before make them visible
        foreach (Transform child in soloCornParent.transform)
        {
            child.transform.GetComponent<Renderer>().material.color =
                LevelManager.Instance.currentLevel.LevelSettings.CornColor;
            
            var rb = child.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionMagnitude, explosionPos, explosionRadius, 3.0F);
        }
        // Destroy them as a garbage collection after 3 seconds from init.
        Destroy(gameObject,3f);
    }
}
