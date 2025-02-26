using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private SpawnPoint _startPoint;
    [SerializeField] private float _repeatRate = 0.3f;
    [SerializeField] private int _initialPoolSize = 5;

    private float _startPointMin = -10.0f;
    private float _startPointMax = 10.0f;
    private GenericSpawner<Cube> _cubeSpawner;

    private void Start()
    {
        _cubeSpawner = new GenericSpawner<Cube>(_cubePrefab, _initialPoolSize);

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

    private void ActionOnGet(Cube cube)
    {
        cube.Init();
    }

    private void TakeFromPool()
    {
        Cube cube = _cubeSpawner.Spawn(StartPoint(), transform.rotation);
        ActionOnGet(cube);

        cube.EndedLife += OnRelease;
    }

    private void OnRelease(Cube cube)
    {
        _cubeSpawner.Despawn(cube);

        cube.EndedLife -= OnRelease;
    }

    private Vector3 StartPoint()
    {
        float y = _startPoint.transform.position.y;
        float x = UnityEngine.Random.Range(_startPointMin, _startPointMax);
        float z = UnityEngine.Random.Range(_startPointMin, _startPointMax);

        return new Vector3(x, y, z);
    }
}
