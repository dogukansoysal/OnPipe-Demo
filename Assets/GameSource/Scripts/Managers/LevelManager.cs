using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    public override void Awake()
    {
        base.Awake();
    }
    public bool isSceneReloading;
    public List<Level> levels;
    [ReadOnly]
    public int currentLevelIndex;
    [ReadOnly]
    public int levelIndex;
    [HideInInspector]
    public Level currentLevel;

    public int minRandomStartIndex;

    public GameObject levelObj;
    public GameObject GarbageGO;
    
    [HideInInspector]
    public UnityEvent levelLoad = new UnityEvent();
    
    
    void Start()
    {
        LoadStart();
    }
    public void LoadStart()
    {
        if (!isSceneReloading)
        {
            if (levelObj)
            {
                Destroy(levelObj);
            }
            LoadLevel();
            GameManager.Instance.gameState = GameState.Start;
        }
        else
        {
            LoadLevel();
            GameManager.Instance.gameState = GameState.Start;
        }
    }
    public void LoadNewLevel()
    {
        if (!isSceneReloading)
        {
            if (levelObj)
            {
                Destroy(levelObj);
                Destroy(GarbageGO);
            }
            LoadLevel();
            GameManager.Instance.gameState = GameState.Start;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void LoadLevel()
    {
        currentLevelIndex = SaveManager.GetSaveDataInt("LevelIndex");
        levelIndex = SaveManager.GetSaveDataInt("LevelIndex") + 1;
        if (currentLevelIndex > levels.Count - 1)
        {
            if (SaveManager.GetSaveDataInt("RandomLevelIndex") == -1)
            {
                currentLevelIndex = Random.Range(minRandomStartIndex, levels.Count);
                SaveManager.Save("RandomLevelIndex", currentLevelIndex);
            }
            else
            {
                currentLevelIndex = SaveManager.GetSaveDataInt("RandomLevelIndex");
            }
        }
        currentLevel = levels[currentLevelIndex];
        levelObj = currentLevel.Init();
        GarbageGO = new GameObject("Garbage");
        levelLoad.Invoke();
    }
}
