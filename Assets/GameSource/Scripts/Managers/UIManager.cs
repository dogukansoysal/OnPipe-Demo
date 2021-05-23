using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    [BoxGroup("Panels")]
    public GameObject startPanel,losePanel,winPanel,gameplayPanel;

    public Image perfectImg;
    public List<Sprite> perfectSprites;

    public GameObject RestartButtonImage;
    public TextMeshProUGUI LevelTextInGame;
    public TextMeshProUGUI ScoreTextInGame;

    private void Start()
    {
        LevelManager.Instance.levelLoad.AddListener(SetStart);
        LevelManager.Instance.levelLoad.AddListener(() =>
            {
                if(LevelTextInGame != null)
                    LevelTextInGame.text = "Level " + (SaveManager.GetSaveDataInt("LevelIndex") + 1);
                //if(ScoreTextInGame != null)
                //    ScoreTextInGame.text = "0 / " + LevelManager.Instance.currentLevel.requiredScore;
            });
    }
    public void SetStart()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        GameManager.Instance.gameState = GameState.Play;
    }
    public void ContinueGame()
    {
        //HideRestartButton();
        //losePanel.SetActive(false);
        //gameplayPanel.SetActive(true);
    }
    public void OpenCloseShop(bool isEnabled)
    {
        if (isEnabled)
        {
            startPanel.SetActive(false);
        }
        else
        {
            startPanel.SetActive(true);
        }
    }
    public void ShowWinPanel()
    {
        gameplayPanel.SetActive(false);
        winPanel.SetActive(true);
    }
    public void ShowLosePanel()
    {
        gameplayPanel.SetActive(false);
        losePanel.SetActive(true);
        Invoke(nameof(ShowRestartButton), 3f);
    }
    public void RestartGame()
    {
        LevelManager.Instance.LoadNewLevel();
    }
    public void PauseGame()
    {

    }
    public void NextLevel()
    {
        LevelManager.Instance.LoadNewLevel();
    }
    public void ShowRestartButton()
    {
        if (losePanel.gameObject.activeInHierarchy)
        {
            RestartButtonImage.SetActive(true);
        }
    }
    public void HideRestartButton()
    {
        RestartButtonImage.SetActive(false);
    }
}
