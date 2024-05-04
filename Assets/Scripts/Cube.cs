using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour 
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;   

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}