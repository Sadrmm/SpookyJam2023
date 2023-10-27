using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Playing,
        End
    }

    private enum EndGameConditions
    {
        ConqueredMap,
        PlayerDead,
        TimeIsUP
    }

    [Header("Level Values")]
    [SerializeField] float _maxTimer = 60.0f;

    [Header("In game Gameobjects")]
    [SerializeField] PlayerController _playerController;

    private float _currentTimer;
    private GameState _currentGameState;

    private void Start()
    {
        _currentTimer = _maxTimer;
        _currentGameState = GameState.Playing;
    }

    private void OnEnable()
    {
        _playerController.OnDead += PlayerDead;
    }

    private void OnDisable()
    {
        _playerController.OnDead -= PlayerDead;
    }

    private void Update()
    {
        HandleTimer();
    }

    private void HandleTimer()
    {
        if (_currentGameState != GameState.Playing) {
            return;
        }

        _currentTimer -= Time.deltaTime;

        if (_currentTimer <= 0.0f) {
            EndGame(EndGameConditions.TimeIsUP);
        }
    }

    #region EndGame
    private void PlayerDead()
    {
        EndGame(EndGameConditions.PlayerDead);
    }

    private void EndGame(EndGameConditions endCondition)
    {
        Debug.Log($"Se acabó la partida por {endCondition}");
        _currentGameState = GameState.End;
    }
    #endregion
}
