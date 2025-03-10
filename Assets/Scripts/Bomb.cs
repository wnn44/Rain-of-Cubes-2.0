using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(ColorChanger))]
public class Bomb : MonoBehaviour
{
    private Detonator _detonator;
    private ColorChanger _colorChanger;
    private float _minTime = 2.0f;
    private float _maxTime = 5.0f;

    public event Action<Bomb> Exploded;

    private void OnEnable()
    {
        ChangingTransparency();
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _detonator = GetComponent<Detonator>();
    }

    private void Explode()
    {
        _detonator.Explode();

        Exploded?.Invoke(this);
    }

    public void ChangingTransparency()
    {
        float delay = UnityEngine.Random.Range(_minTime, _maxTime);

        StartCoroutine(Delay(delay));
    }

    private IEnumerator Delay(float delay)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= delay)
        {
            float normalizedTime = elapsedTime / delay;
            elapsedTime += Time.deltaTime;
            _colorChanger.ChangingTransparency(normalizedTime);

            yield return null;
        }

        Explode();
    }
}
