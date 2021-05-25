using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    [Header("References")]
    public List<GameObject> RoadPrefabs;
    public GameObject FinishPrefab;

    [Header("Runtime Variables")]
    public Queue<GameObject> Roads = new Queue<GameObject>();    // Queue data structure is very suitable for that need in order to pop and push road parts like a queue. 
    public Transform RoadParent;    // Is an empty/holder gameobject to hold the road parts.
    private const int c_RoadHeight = 20; // Road height as constant in world scale. TO DO : Make it as Generic size acceptable. 
    
    /// <summary>
    /// Player Reference
    /// </summary>
    public PlayerController PlayerController;

    [Header("Settings and Data")]
    public int Score = 0;    // Current score
    public int ScorePerHit = 100;    // Earned score per hit to the corn circle.
    public int Multiplier = 1;    // TODO: Score multipler will increase in the future. Perfect corn destruction will increase this multiple +1 up to 5x.
    public int SpawnedRoadCount = 0;    // Total spawned road count after start of the game.
    
    /// <summary>
    /// Initialize a listener to level loading event.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        LevelManager.Instance.levelLoad.AddListener(InitStage);
    }

    /// <summary>
    /// Initialize the main stage while scene load.
    /// </summary>
    private void InitStage()
    {
        PlayerController.PlayerModel.GetComponent<Renderer>().material.color = LevelManager.Instance.currentLevel.LevelSettings.PlayerColor;
        InitRoads();
    }

    /// <summary>
    /// Update each frame
    /// </summary>
    private void Update()
    {
        if(GameManager.Instance.gameState == GameState.Lose ||
           GameManager.Instance.gameState == GameState.Pause ||
           GameManager.Instance.gameState == GameState.Win)
            return;
        
        MoveRoads();    // Move roads to downside instead of moving the ring upwards. Which is significantly important on such that games to make things easier for multiple reasons.
        // If a road is came to it's end and not visible by the camera, it will die and spawn another one to the top.
        if (RoadParent.position.y <= -c_RoadHeight)
        {
            var lastPosition = DestroyLastRoad();
            SpawnNewRoad(lastPosition + (c_RoadHeight * (Roads.Count + 1)));
            ResetRoadParentPosition();
        }
    }

    public void AddScore()
    {
        Score += ScorePerHit * Multiplier;    // Increase current score
        UIManager.Instance.UpdateScore(Score); // put that score change to the UI
    }
    
    
#region Road
    /// <summary>
    /// Initial function when level loaded to initialize environment.
    /// </summary>
    private void InitRoads()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnNewRoad(i * c_RoadHeight);
        }
    }

    /// <summary>
    /// Function that been called to move the environment parts as hole. (pipes, corns, obstacles etc.)
    /// </summary>
    private void MoveRoads()
    {
        var roadParentPosition = RoadParent.position;
        roadParentPosition.y -= LevelManager.Instance.currentLevel.LevelSettings.MovementSpeed * Time.deltaTime;
        RoadParent.position = roadParentPosition;
    }

    /// <summary>
    /// Reset the parents position to avoid the disappear in the bottom.
    /// </summary>
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
    
    /// <summary>
    /// Main function to spawn a new road part to the game
    /// </summary>
    /// <param name="i_SpawnPosition">Spawn Y position of that road part</param>
    private void SpawnNewRoad(float i_SpawnPosition)
    {
        GameObject newRoad = null;
        if (SpawnedRoadCount < LevelManager.Instance.currentLevel.TotalRoadSpawnCount)
        {
            newRoad = Instantiate(RoadPrefabs[Random.Range(0, RoadPrefabs.Count)], RoadParent, true);
        }
        else
        {
            newRoad = Instantiate(FinishPrefab, RoadParent, true);

        }

        newRoad.transform.position = new Vector3(0, i_SpawnPosition, 0);

        // Loop through all the children object and color them if needed.
        // TODO: make it before the level init. Running this in runtime will cause performance decrease.
        foreach(Transform child in newRoad.transform)
        {
            if (child.CompareTag("Corn"))
                child.transform.GetComponent<Renderer>().material.color =
                    LevelManager.Instance.currentLevel.LevelSettings.CornColor;
            if (child.CompareTag("Obstacle"))
                child.transform.GetComponent<Renderer>().material.color =
                    LevelManager.Instance.currentLevel.LevelSettings.ObstacleColor;
        }

        // If game is playing, increase total spawned road count
        if (GameManager.Instance.gameState == GameState.Play)
        {
            SpawnedRoadCount++;
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
