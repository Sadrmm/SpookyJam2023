using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    public static GameManager Instance;

    // Events
    public UnityAction<float> OnTimerUpdated;
    public UnityAction OnGameEnded;

    // Serialize Fields
    [SerializeField] AnimationCurve _upgradesCurve;
    public AnimationCurve UpgradesCurve => _upgradesCurve;
    [SerializeField] LayerMask _wallLayer;

    [Header("Prefabs")]
    [SerializeField] GameObject[] _enemies;

    [Header("Timer")]
    [SerializeField] float _maxTimer = 60.0f;

    [Header("Enemies Spawn")]
    [SerializeField] Transform _enemiesContainer;
    [SerializeField] AnimationCurve _enemySpawnAmountCurve;
    [SerializeField] float _timeBtwnWaves = 15.0f;
    [SerializeField] float _minRadiusSpawn = 5.0f;
    [SerializeField] float _maxRadiusSpawn = 10.0f;

    [Header("In game Gameobjects")]
    [SerializeField] PlayerController _playerController;

    [Space]

    public float percentageToWin = 0.8f;

    private float _currentTimer;
    private float _currentTimeBtwnWaves;
    private int _waveIndex;
    private GameState _currentGameState;

    private int _goldAchieved = 0;

    private List<IScareable> _scaredEnemies;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _currentTimer = _maxTimer;
        _currentTimeBtwnWaves = _timeBtwnWaves;
        _currentGameState = GameState.Playing;

        _scaredEnemies = new List<IScareable>();
    }

    private void Start()
    {
        PaintPercentageController.Instance.OnPercentageCalculated += PaintPercentageController_OnPercentageCalculated;
    }

    private void OnEnable()
    {
        _playerController.OnPlayerDead += PlayerDead;
        IDamageable.OnDead += EnemyDead;
        IScareable.OnScared += AddScared;
        IScareable.OnUnscared += RemoveScared;
    }

    private void OnDisable()
    {
        _playerController.OnPlayerDead -= PlayerDead;
        IDamageable.OnDead -= EnemyDead;
        IScareable.OnScared -= AddScared;
        IScareable.OnUnscared -= RemoveScared;
    }

    private void Update()
    {
        if (_currentGameState != GameState.Playing) {
            return;
        }

        HandleTimer();
        HandleWaves();
    }

    private void AddScared(IScareable enemyScared)
    {
        if (_scaredEnemies.Contains(enemyScared)) { 
            return;
        }
        _scaredEnemies.Add(enemyScared);
        //Debug.Log($"Enemigos asustados: {_scaredEnemies.Count}");
    }

    private void RemoveScared(IScareable enemyNotScared)
    {
        if (!_scaredEnemies.Contains(enemyNotScared)) {
            return;
        }
        _scaredEnemies.Remove(enemyNotScared);
        //Debug.Log($"Enemigos asustados: {_scaredEnemies.Count}");
    }

    private void HandleWaves()
    {
        _currentTimeBtwnWaves -= Time.deltaTime;

        if (_currentTimeBtwnWaves < 0.0f) {
            _waveIndex++;
            int spawnEnemyAmount = Mathf.RoundToInt(_enemySpawnAmountCurve.Evaluate(_waveIndex));

            StartCoroutine(SpawnWave(spawnEnemyAmount));

            _currentTimeBtwnWaves = _timeBtwnWaves;
        }
    }

    private IEnumerator SpawnWave(int spawnEnemyAmount)
    {
        float spaceBtwnAngles = 2 * Mathf.PI / spawnEnemyAmount;
        float angle = Random.Range(0f, 360f);

        if (_maxRadiusSpawn < _minRadiusSpawn) { 
            _maxRadiusSpawn = _minRadiusSpawn;
        }

        int i = 0;
        while (i < spawnEnemyAmount) {
            float radius = Random.Range(_minRadiusSpawn, _maxRadiusSpawn);

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 spawnPos = new Vector3(_playerController.transform.position.x + x,
                0f,
                _playerController.transform.position.z + y);

            if (!Physics.CheckSphere(spawnPos, 1, _wallLayer))
            {
                GameObject enemyGO = Instantiate(_enemies[Random.Range(0, _enemies.Length)], _enemiesContainer);
                enemyGO.transform.position = spawnPos;
                enemyGO.transform.LookAt(_playerController.transform);

                BaseEnemyAI enemyAI = enemyGO.GetComponent<BaseEnemyAI>();

                if (enemyAI == null) {
                    Debug.LogError($"{enemyGO} does not have EnemyAI script");
                }
                else {
                    enemyAI.Init(_playerController.transform);
                }
                angle += spaceBtwnAngles;
                i++;
            }

            yield return null;
        }
    }

    private void HandleTimer()
    {
        _currentTimer -= Time.deltaTime;
        OnTimerUpdated?.Invoke(_currentTimer);

        if (_currentTimer < 0.0f) {
            TimeIsUp();
        }
    }

    private void EnemyDead(IDamageable enemyDead)
    {
        _goldAchieved++;
    }

    #region EndGame
    private void PlayerDead()
    {
        _goldAchieved /= 2;
        EndGame(EndGameConditions.PlayerDead);
    }

    private void TimeIsUp()
    {
        _goldAchieved /= 2;
        EndGame(EndGameConditions.TimeIsUP);
    }

    private void EndGame(EndGameConditions endCondition)
    {
        Debug.Log($"Se acab� la partida por {endCondition}");
        _currentGameState = GameState.End;

        DisablePlayer();
        DisableEnemies();

        UpgradeStats.MoneyAmount += _goldAchieved;
        OnGameEnded?.Invoke();
    }

    private void DisablePlayer()
    {
        _playerController.DisablePlayer();
    }
    private void DisableEnemies()
    {
        foreach(Transform enemyTransform in _enemiesContainer) {
            Enemy enemy = enemyTransform.GetComponent<Enemy>();

            if (enemy == null) {
                Debug.Log($"Algo que no es {nameof(Enemy)} se ha colado en {_enemiesContainer}");
                continue;
            }

            enemy.DisableEnemy();
        }
    }
    #endregion

    private void PaintPercentageController_OnPercentageCalculated(float percentage)
    {
        if (percentage > percentageToWin)
        {
            EndGame(EndGameConditions.ConqueredMap);
            PaintPercentageController.Instance.OnPercentageCalculated -= PaintPercentageController_OnPercentageCalculated;
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
