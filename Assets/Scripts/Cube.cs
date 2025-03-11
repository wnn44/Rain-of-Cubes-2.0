using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private bool _hasCollided = false;
    private float _minTime = 2.0f;
    private float _maxTime = 5.0f;
    private Rigidbody _rigidbody;
    private ColorChanger _colorChanger;

    public event Action<Cube> CubeDisappeared;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger= GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            _hasCollided = true;

            _colorChanger.RandomColor();

            float delay = UnityEngine.Random.Range(_minTime, _maxTime);
            StartCoroutine(Delay(delay));
        }
    }

    public void Init(Vector3 position)
    {
        _hasCollided = false;
        _colorChanger.InitColor();
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        CubeDisappeared?.Invoke(this);
    }
}
