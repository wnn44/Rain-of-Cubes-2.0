using UnityEngine;

public class BombSpawner : GenericSpawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeOnRelease += TakeFromPool;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeOnRelease -= TakeFromPool;
    }

    private void TakeFromPool(Cube cube)
    {
        Bomb bomb = Spawn();

        bomb.transform.position = cube.transform.position;

        bomb.LifeCycle();

        bomb.Exploded += OnRelease;
    }

    private void OnRelease(Bomb bomb)
    {
        Despawn(bomb);

        bomb.Exploded -= OnRelease;
    }
}
