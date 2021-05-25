using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Level", menuName = "Create Level")]
public class Level : ScriptableObject
{
    //[ShowAssetPreview]
    //public GameObject levelPrefab;
    public LevelSettings LevelSettings;
    
    public GameObject Init()
    {
        /*
        if (levelPrefab)
        {
            GameObject createdLevel = Instantiate(levelPrefab);
            return createdLevel;
        }
        */
        return null;
    }
}
