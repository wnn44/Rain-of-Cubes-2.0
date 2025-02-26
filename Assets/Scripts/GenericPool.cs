using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;

    public GenericPool(T prefab, int initialSize)
    {
        _prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            T obj = GameObject.Instantiate(_prefab);

            return obj;
        }
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);

        _pool.Enqueue(obj);
    }
}
