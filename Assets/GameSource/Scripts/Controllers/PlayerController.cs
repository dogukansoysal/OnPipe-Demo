using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerModel;
    public float scaleDownFactor;
    public float scaleUpFactor;

    public bool isHolding;

    public float minScaleValue = 0;
    public void Awake()
    {
        InputManager.Instance.hold += (holdTime) => { isHolding = true; };
        InputManager.Instance.swipe  += (deltaPos) => { isHolding = false; };

    }

    public void LateUpdate()
    {
        if(GameManager.Instance.gameState == GameState.Lose || GameManager.Instance.gameState == GameState.Pause)
            return;
        
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
    
    private void ScaleDown()
    {
        transform.localScale = 
            new Vector3(
                Mathf.Max(minScaleValue, transform.localScale.x - (scaleDownFactor * Time.deltaTime)), 
                transform.localScale.y, 
                Mathf.Max(minScaleValue, transform.localScale.z - (scaleDownFactor * Time.deltaTime)));
    }

    private void ScaleUp()
    {
        transform.localScale = 
            new Vector3(
                Mathf.Min(1f,transform.localScale.x + (scaleDownFactor * Time.deltaTime)), 
                transform.localScale.y, 
                Mathf.Min(1f, transform.localScale.z + (scaleDownFactor * Time.deltaTime)));
    }

    private void Die()
    {
        // TODO: Explode
        GameManager.Instance.gameState = GameState.Lose;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.gameState != GameState.Play)
            return;
        
        if (other.CompareTag("Road"))
        {
            minScaleValue = other.transform.lossyScale.x / 2f;
        }
        if (other.transform.CompareTag("Corn"))
        {
            StageManager.Instance.AddScore();
            Destroy(other.gameObject);
        }
        if (other.transform.CompareTag("Obstacle"))
        {
            GameManager.Instance.gameState = GameState.Lose;
        }
        if (other.transform.CompareTag("Finish"))
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
