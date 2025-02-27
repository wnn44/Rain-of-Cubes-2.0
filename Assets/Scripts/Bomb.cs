using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Renderer _renderer;
    private Color _initialColor = Color.red;
    private float _minTime = 2.0f;
    private float _maxTime = 5.0f;
    private Rigidbody _rigidbody;

    public event Action<Bomb> EndedLife;

    public void Init(Vector3 position)
    {
        transform.position = position;
        LiveDeley();
    }

    private void LiveDeley()
    {
        //_renderer.material.color = UnityEngine.Random.ColorHSV();
        float delay = UnityEngine.Random.Range(_minTime, _maxTime);
        StartCoroutine(Delay(delay));
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        EndedLife?.Invoke(this);
    }

    private void Start()
    {  

    }


}
