using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    public int TotalCreated { get; private set; }
    public int TotalSpawned { get; private set; }
    public int ActiveObjacts { get; private set; }

    private ObjectPool<T> _pool;

    public GenericSpawner()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public T Spawn()
    {
        TotalSpawned++;

        return _pool.Get();
    }

    public void Despawn(T obj)
    {
        _pool.Release(obj);

        ActiveObjacts--;
    }

    private void Update()
    {
        TotalCreated = _pool.CountAll;
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

        ActiveObjacts++;
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
