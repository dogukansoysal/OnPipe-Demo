using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    public List<GameObject> RoadPrefabs;
    
    public Queue<GameObject> Roads = new Queue<GameObject>();
    public Transform RoadParent;

    private const int c_RoadHeight = 20; // Road height as constant in world scale. TO DO : Make it as Generic size acceptable. 
    
    [Header("References")]
    public PlayerController PlayerController;

    public int Score = 0;
    public int ScorePerHit = 100;
    public int Multiplier = 1;
    public override void Awake()
    {
        base.Awake();
        LevelManager.Instance.levelLoad.AddListener(InitStage);
    }

    private void InitStage()
    {
        PlayerController.PlayerModel.GetComponent<Renderer>().material.color = LevelManager.Instance.currentLevel.LevelSettings.PlayerColor;
        InitRoads();
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == GameState.Lose || GameManager.Instance.gameState == GameState.Pause)
            return;
        
        MoveRoads();
        if (RoadParent.position.y <= -c_RoadHeight)
        {
            var lastPosition = DestroyLastRoad();
            SpawnNewRoad(lastPosition + (c_RoadHeight * (Roads.Count + 1)));
            ResetRoadParentPosition();
        }
    }

    public void AddScore()
    {
        Score += ScorePerHit * Multiplier;
        UIManager.Instance.UpdateScore(Score);
    }
    
    
#region Road

    private void InitRoads()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnNewRoad(i * c_RoadHeight);
        }
    }

    private void MoveRoads()
    {
        var roadParentPosition = RoadParent.position;
        roadParentPosition.y -= LevelManager.Instance.currentLevel.LevelSettings.MovementSpeed * Time.deltaTime;
        RoadParent.position = roadParentPosition;
    }

    private void ResetRoadParentPosition()
    {
        // Save all road blocks to a temp var.
        var children = new List<Transform>();
        while(RoadParent.childCount > 0)
        {
            children.Add(RoadParent.GetChild(0));
            RoadParent.GetChild(0).parent = null;
        }

        RoadParent.transform.position = Vector3.zero;    // Reset to Spawn position

        // Add all road parts to the RoadParent
        for (int i = 0; i < children.Count; ++i)
        {
            children[i].parent = RoadParent;
        }
    }
    
    private void SpawnNewRoad(float i_SpawnPosition)
    {
        var newRoad = Instantiate(RoadPrefabs[Random.Range(0,RoadPrefabs.Count)], RoadParent, true);
        newRoad.transform.position = new Vector3(0, i_SpawnPosition, 0);

        foreach(Transform child in newRoad.transform)
        {
            if (child.CompareTag("Corn"))
                child.transform.GetComponent<Renderer>().material.color =
                    LevelManager.Instance.currentLevel.LevelSettings.CornColor;
        }
        
        
        Roads.Enqueue(newRoad);
    }
    
    /// <summary>
    /// Destroys last road block.
    /// </summary>
    /// <returns>Y position of destroyed road.</returns>
    private float DestroyLastRoad()
    {
        var lastRoad = Roads.Dequeue();
        var lastRoadYPosition = lastRoad.transform.position.y;
        Destroy(lastRoad);
        return lastRoadYPosition;
    }

#endregion

}
