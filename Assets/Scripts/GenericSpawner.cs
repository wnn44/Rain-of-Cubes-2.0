using UnityEngine;

public class GenericSpawner<T> where T : MonoBehaviour
{
    private GenericPool<T> _pool;

    public GenericSpawner(T prefab, int initialPoolSize)
    {
        _pool = new GenericPool<T>(prefab, initialPoolSize);
    }

    public T Spawn(Vector3 position, Quaternion rotation)
    {
        T obj = _pool.Get();

        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    public void Despawn(T obj)
    {
        _pool.Return(obj);
    }
}
