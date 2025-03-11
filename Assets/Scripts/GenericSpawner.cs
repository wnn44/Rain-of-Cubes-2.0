using System;
using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 15;

    public int TotalCreated { get; private set; }
    public int TotalSpawned { get; private set; }
    public int ActiveObjects { get; private set; }

    private ObjectPool<T> _pool;

    public event Action DataChanged;

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

    private void Update()
    {
        TotalCreated = _pool.CountAll;
    }

    public T Spawn()
    {
        TotalSpawned++;

        return _pool.Get();
    }

    public void Despawn(T obj)
    {
        _pool.Release(obj);

        ActiveObjects--;
    }

    private T CreateObject()
    {
        T newObject = UnityEngine.Object.Instantiate(_prefab, _parent);
        newObject.gameObject.SetActive(false);

        return newObject;
    }

    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);

        ActiveObjects++;
        UpdateInfoPanel();
    }

    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyObject(T obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    private void UpdateInfoPanel()
    {
        DataChanged?.Invoke();
    }
}
