using UnityEngine;

public class BombSpawner : GenericSpawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeEnded += TakeFromPool;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeEnded -= TakeFromPool;
    }

    private void LateUpdate()
    {
        Debug.Log("Созжано всего бомб" + TotalCreated + "   заспавнено всего " + TotalSpawned + "  " + ActiveObjacts);
    }

    private void TakeFromPool(Cube cube)
    {
        Bomb bomb = Spawn();

        bomb.transform.position = cube.transform.position;

        bomb.EndedLifeBomb += OnRelease;
    }

    private void OnRelease(Bomb bomb)
    {
        Despawn(bomb);

        bomb.EndedLifeBomb -= OnRelease;
    }
}
