using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _chanceSecondSpawn = 50;
    [SerializeField] private float _distanseWithoutPositions = 3f;
    [SerializeField] private int _countCloneEnemy = 20;
    [SerializeField] private GameObject[] _enemyPrefabs;

    private SpeedChanger _speedChanger;
    private List<GameObject> _pullEntity = new List<GameObject>();
    private PlatformGenerator _platformGenerator;
    private Vector3[] _movePoints;
    private PullObject _pullObject;

    [Inject]
    private void Construct(PlatformGenerator platformGenerator, SpeedChanger platformSpeed)
    {
        _platformGenerator = platformGenerator;
        _speedChanger = platformSpeed;
 
    }

    private void Awake()
    {
        _pullObject = gameObject.AddComponent<PullObject>();
        _pullEntity = _pullObject.GeneratePullObject(_countCloneEnemy, _enemyPrefabs);
        foreach (GameObject e in _pullEntity) 
        {
            e.GetComponent<EnemyMover>().SetStartParameters(_speedChanger);
        }
    }

    private void Start()
    {
        _platformGenerator.OnPlatformActivate += RandomizeEntityPerLine;
        
        SetPositions();
    }

    private void OnDisable() => _platformGenerator.OnPlatformActivate -= RandomizeEntityPerLine;

    private void SetPositions()
    {
        _movePoints = new Vector3[3];
        _movePoints[1] = new Vector3(0, 0, 0);
        _movePoints[2] = new Vector3(_distanseWithoutPositions, 0, 0);
        _movePoints[0] = new Vector3(-_distanseWithoutPositions, 0, 0);
    }

    private void RandomizeEntityPerLine(GameObject platform)
    {
        int currentChanceSecondSpawn = Random.Range(0, 100);
        if (currentChanceSecondSpawn <= _chanceSecondSpawn)
        {
            RandomizePositionOnLine(platform, 2);
        }
        else
            RandomizePositionOnLine(platform, 1);
    }

    private void RandomizePositionOnLine(GameObject platform, int entityPerLine)
    {
        int namberPosition = Random.Range(0, 3);
        SpawnOnPosition(platform, namberPosition);
        if (entityPerLine == 2)
        {
            RandomizeSecondPosition(platform, namberPosition);
        }
    }

    private void RandomizeSecondPosition(GameObject platform, int namberPosition)
    {
        int namberSecondPosition = Random.Range(0, 3);
        if (namberSecondPosition == namberPosition)
        {
            RandomizeSecondPosition(platform, namberPosition);
        }
        else
            SpawnOnPosition(platform, namberSecondPosition);
    }

    private void SpawnOnPosition(GameObject platform, int namberPosition)
    {
        switch (namberPosition)
        {
            case 0:
                SpawnEntity(_movePoints[1], platform);
                break;
            case 1:
                SpawnEntity(_movePoints[2], platform);
                break;
            case 2:
                SpawnEntity(_movePoints[0], platform);
                break;
        }
    }


    private void SpawnEntity(Vector3 spawnPosition, GameObject currentPlatform)
    {
        GameObject entity = _pullEntity[Random.Range(0, _pullEntity.Count - 1)];
        if (!entity.activeSelf)
        {
            entity.SetActive(true);
            entity.transform.position = new Vector3(spawnPosition.x, 1, currentPlatform.GetComponent<Transform>().position.z);
        }
        else
            SpawnEntity(spawnPosition, currentPlatform);
    }
}
