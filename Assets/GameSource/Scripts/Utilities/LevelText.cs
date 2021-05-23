using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelText : MonoBehaviour
{
    
    private void Start()
    {
        LevelManager.Instance.levelLoad.AddListener(LoadText);
    }
    private void LoadText()
    {
        if (GetComponent<TextMeshProUGUI>())
        {
            GetComponent<TextMeshProUGUI>().text = "Level " + LevelManager.Instance.levelIndex.ToString();
        }
        if (GetComponent<Text>())
        {
            GetComponent<Text>().text = "Level " + LevelManager.Instance.levelIndex.ToString();
        }
    }
}
