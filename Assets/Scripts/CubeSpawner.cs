using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : GenericSpawner<Cube>
{
    [SerializeField] private SpawnPoint _spawnPoint;
    [SerializeField] private float _repeatRate = 0.3f;

    private float _spawnPointMin = -10.0f;
    private float _spawnPointMax = 10.0f;

    public event Action<Cube> CubeEnded;

    private void Awake()
    {
        StartCoroutine(SpawnCubes());
    }

    private void LateUpdate()
    {
        Debug.Log("������� ����� ����� " + CreateAll + "   ���������� ����� " + SpawnAll + "  " + Live);
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
        Cube cube = Spawn();

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

        Despawn(cube);

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
