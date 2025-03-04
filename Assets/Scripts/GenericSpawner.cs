using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> where T : MonoBehaviour
{
    private ObjectPool<T> _pool;
    private T _prefab;
    private Transform _parent;

    public GenericSpawner(T prefab, Transform parent = null, int defaultCapacity = 10, int maxSize = 100)
    {
        _prefab = prefab;
        _parent = parent;

        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public T Spawn()
    {
        Debug.Log(_pool + "   " + _pool.CountAll + "  взяли");
        return _pool.Get();
    }

    public void Despawn(T obj)
    {
        _pool.Release(obj);
    }

    private T CreateObject()
    {
        T newObject = Object.Instantiate(_prefab, _parent);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyObject(T obj)
    {
        Object.Destroy(obj.gameObject);
    }
}
