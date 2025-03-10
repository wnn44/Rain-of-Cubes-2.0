using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private bool _hasCollided = false;
    private float _minTime = 2.0f;
    private float _maxTime = 5.0f;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Color _initialColor = Color.red;

    public event Action<Cube> CubeDisappeared;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 position)
    {
        _hasCollided = false;
        _renderer.material.color = _initialColor;
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            _hasCollided = true;

            _renderer.material.color = UnityEngine.Random.ColorHSV();

            float delay = UnityEngine.Random.Range(_minTime, _maxTime);
            StartCoroutine(Delay(delay));
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        CubeDisappeared?.Invoke(this);
    }
}
