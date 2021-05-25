using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerModel;    // Player Model reference
    public float scaleDownFactor;    // Scale factor while on holding
    public float scaleUpFactor;    // Scale factor when release

    public bool isHolding;    // Bool value for touch
    public float minScaleValue = 0;    // Min scaleable value defined by pipe's size
    public ParticleSystem SparkVFX;    // similar Spark effect as in original game
    
    /// <summary>
    /// Initialize input system.
    /// </summary>
    public void Awake()
    {
        InputManager.Instance.hold += (holdTime) => { isHolding = true; };
        InputManager.Instance.swipe  += (deltaPos) => { isHolding = false; };

    }

    /// <summary>
    /// Late Update function in order to overcome the race condition on input manager.
    /// </summary>
    public void LateUpdate()
    {
        // Check if game is playable
        if(GameManager.Instance.gameState == GameState.Lose || GameManager.Instance.gameState == GameState.Pause)
            return;
        
        
        // Check the road size
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Road"))
            {
                minScaleValue = hitCollider.transform.lossyScale.x / 2f;
            }
        }

        // Die when hit a larger pipe then current ring's scale.
        if (transform.lossyScale.x < minScaleValue)
        {
            Die();
        }
        
        if (isHolding)
            ScaleDown();
        else
        {
            ScaleUp();
        }
        
    }
    
    /// <summary>
    /// Scale down function of ring
    /// </summary>
    private void ScaleDown()
    {
        transform.localScale = 
            new Vector3(
                Mathf.Max(minScaleValue, transform.localScale.x - (scaleDownFactor * Time.deltaTime)), 
                transform.localScale.y, 
                Mathf.Max(minScaleValue, transform.localScale.z - (scaleDownFactor * Time.deltaTime)));
        if (transform.localScale.x < minScaleValue * 1.2f)
        {
            SparkVFX.Play(true);
        }
        else
        {
            SparkVFX.Stop(true);
        }
    }

    /// <summary>
    /// Scale up when release the input to initial ring scale
    /// </summary>
    private void ScaleUp()
    {
        transform.localScale = 
            new Vector3(
                Mathf.Min(1f,transform.localScale.x + (scaleDownFactor * Time.deltaTime)), 
                transform.localScale.y, 
                Mathf.Min(1f, transform.localScale.z + (scaleDownFactor * Time.deltaTime)));
        SparkVFX.Stop(true);

    }

    /// <summary>
    /// Ring death function
    /// </summary>
    private void Die()
    {
        // TODO: Explode
        GameManager.Instance.gameState = GameState.Lose;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.gameState != GameState.Play)
            return;
        
        if (other.transform.CompareTag("Corn"))
        {
            StageManager.Instance.AddScore();    // Add that score to the game and UI
            other.GetComponent<Corn>().Explode();    // Make it explode 
        }
        if (other.transform.CompareTag("Obstacle"))
        {
            GameManager.Instance.gameState = GameState.Lose;
        }
        if (other.CompareTag("Finish"))
        {
            if (StageManager.Instance.Score >= LevelManager.Instance.currentLevel.RequiredScore)
            {
                GameManager.Instance.gameState = GameState.Win;
            }
            else
            {
                GameManager.Instance.gameState = GameState.Lose;
            }
        }
        
    }
}
