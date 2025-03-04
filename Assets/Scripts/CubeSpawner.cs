using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private SpawnPoint _spawnPoint;
    [SerializeField] private float _repeatRate = 0.3f;
    [SerializeField] private int _initialPoolSize = 5;

    private float _spawnPointMin = -10.0f;
    private float _spawnPointMax = 10.0f;
    private GenericSpawner<Cube> _cubeSpawner;

    public event Action<Cube> CubeEnded;

    private void Awake()
    {
        Transform parent = new GameObject("Cube").transform;

        _cubeSpawner = new GenericSpawner<Cube>(_cubePrefab, parent, 10, 100);

        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            TakeFromPool();

            yield return waitForSeconds;
        }
    }

    private void TakeFromPool()
    {
        Cube cube = _cubeSpawner.Spawn();

        ActionOnGet(cube);

        cube.EndedLife += OnRelease;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Init(StartPoint());
    }

    private void OnRelease(Cube cube)
    {
        Vector3 position = cube.transform.position;

        _cubeSpawner.Despawn(cube);

        cube.EndedLife -= OnRelease;

        CubeEnded?.Invoke(cube);
    }

    private Vector3 StartPoint()
    {
        float y = _spawnPoint.transform.position.y;
        float x = UnityEngine.Random.Range(_spawnPointMin, _spawnPointMax);
        float z = UnityEngine.Random.Range(_spawnPointMin, _spawnPointMax);

        return new Vector3(x, y, z);
    }
}
