using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public enum GameState
{
    Start,
    Pause,
    Play,
    Win,
    Lose
}
public class GameManager : Singleton<GameManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    [ReadOnly]
    [SerializeField]
    private GameState _gameState;
    public GameState gameState
    {
        get { return _gameState; }
        set
        {
            if(_gameState != value)
            {
                _gameState = value;
                switch (_gameState)
                {
                    case GameState.Start:

                        //---------------------------------------------------------------------------Start

                        //---------------------------------------------------------------------------End
                        break;
                    case GameState.Pause:
                        //---------------------------------------------------------------------------Start

                        //---------------------------------------------------------------------------End
                        break;
                    case GameState.Play:
                        //---------------------------------------------------------------------------Start

                        //---------------------------------------------------------------------------End
                        break;
                    case GameState.Win:
                        SaveManager.Save("RandomLevelIndex", -1);
                        SaveManager.Save("LevelIndex", SaveManager.GetSaveDataInt("LevelIndex") + 1);
                        SoundManager.Instance.PlayWinSound();
                        UIManager.Instance.ShowWinPanel();
                        //---------------------------------------------------------------------------Start
                        
                        //---------------------------------------------------------------------------End
                        break;
                    case GameState.Lose:
                        UIManager.Instance.ShowLosePanel();
                        SoundManager.Instance.PlayLoseSound();
                        //---------------------------------------------------------------------------Start

                        //---------------------------------------------------------------------------End
                        break;
                }
            }
        }
    }
}
