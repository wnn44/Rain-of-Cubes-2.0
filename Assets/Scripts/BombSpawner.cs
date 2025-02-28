using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private int _initialPoolSize = 5;

    private GenericSpawner<Bomb> _bombSpawner;
    private CubeSpawner _cubeSpawner;

    private void Start()
    {
        _bombSpawner = new GenericSpawner<Bomb>(_bombPrefab, _initialPoolSize);
        _cubeSpawner = new CubeSpawner();

        //StartCoroutine(SpawnCubes());
    }

    private void OnEnable()
    {
        _cubeSpawner.CubeReturnedToPool += TakeFromPool;
        Debug.Log("+");

    }

    private void TakeFromPool(Vector3 spawnPoint)
    {
        Debug.Log("Spawn");
        Bomb bomb = _bombSpawner.Spawn(spawnPoint, Quaternion.identity);


    }

}
