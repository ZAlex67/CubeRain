using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Explosion))]
public class Bomb : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Explosion _explosion;

    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;
    public Explosion Explosion => _explosion;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _explosion = GetComponent<Explosion>();
    }

    public void SetAlpha(float color)
    {
        Color colorNew = _renderer.material.color;
        colorNew.a = color;
        _renderer.material.color = colorNew;
    }
}