using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    private float _minTime = 2.0f;
    private float _maxTime = 5.0f;
    private Renderer _renderer;
    private Color _color;

    public event Action<Bomb> EndedLifeBomb;

    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();

        float delay = UnityEngine.Random.Range(_minTime, _maxTime);

        StartCoroutine(Delay(delay));
    }

    private IEnumerator Delay(float delay)
    {
        float elapsedTime = 0f;

        _color = _renderer.material.color;

        while (elapsedTime <= delay)
        {
            float normalizedTime = elapsedTime / delay;
            elapsedTime += Time.deltaTime;
            _color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            _renderer.material.color = _color;

            yield return null;
        }

        EndedLifeBomb?.Invoke(this);
    }
}
