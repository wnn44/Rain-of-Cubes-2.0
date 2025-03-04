using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private int _initialPoolSize = 5;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private GenericSpawner<Bomb> _bombSpawner;

    private void Awake()
    {
        Transform parent = new GameObject("Bomb").transform;

        _bombSpawner = new GenericSpawner<Bomb>(_bombPrefab, parent, 10, 10);
    }

    private void OnEnable()
    {
        _cubeSpawner.CubeEnded += TakeFromPool;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeEnded -= TakeFromPool;
    }

    private void TakeFromPool(Cube cube)
    {
        Bomb bomb = _bombSpawner.Spawn();

        bomb.transform.position = cube.transform.position;

        bomb.EndedLifeBomb += OnRelease;
    }

    private void OnRelease(Bomb bomb)
    {
        _bombSpawner.Despawn(bomb);

        bomb.EndedLifeBomb -= OnRelease;
    }
}
