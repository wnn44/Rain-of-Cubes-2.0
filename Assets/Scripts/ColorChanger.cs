using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Color _initialColor = Color.red;
    private Color _color;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _color =_renderer.material.color;
    }

    public void RandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void InitColor()
    {
        _renderer.material.color = _initialColor;
    }

    public void ChangingTransparency(float normalizedTime)
    {
        _color.a = Mathf.Lerp(1f, 0f, normalizedTime);
        _renderer.material.color = _color;
    }
}
