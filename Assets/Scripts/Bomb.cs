using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _minTime = 2.0f;
    private float _maxTime = 5.0f;
    private Renderer _renderer;

    public event Action<Bomb> EndedLife;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        float delay = UnityEngine.Random.Range(_minTime, _maxTime);
        StartCoroutine(Delay(delay));
    }

    private IEnumerator Delay(float delay)
    {
        float elapsedTime = 0f;

        Color color = _renderer.material.color;

        while (elapsedTime <= delay)
        {
            float normalizedTime = elapsedTime / delay;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            _renderer.material.color = color;

            yield return null;
        }


        //yield return new WaitForSeconds(delay);
    
        EndedLife?.Invoke(this);
    }
}
