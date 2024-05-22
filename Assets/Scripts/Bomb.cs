using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Explosion))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _coefficient;
    [SerializeField] private BombFactory _bombFactory;

    private Renderer _renderer;
    private Explosion _explosion;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _explosion = GetComponent<Explosion>();
    }

    public void SetBombFactory(BombFactory bombFactory)
    {
        _bombFactory = bombFactory;
    }

    public IEnumerator TimeExplosion()
    {
        float minTime = 2f;
        float maxTime = 5f;
        float alphaStop = 0f;
        float alpha = 1f;

        float currentTime = Random.Range(minTime, maxTime);

        while (currentTime > float.Epsilon)
        {
            SetAlpha(Mathf.MoveTowards(_renderer.material.color.a, alphaStop, _coefficient * Time.deltaTime));
            currentTime -= Time.deltaTime;
            yield return null;
        }

        if (isActiveAndEnabled)
        {
            _explosion.Explode();
            _bombFactory.ReleaseObject(this);
            SetAlpha(alpha);
        }
    }

    private void SetAlpha(float color)
    {
        Color colorNew = _renderer.material.color;
        colorNew.a = color;
        _renderer.material.color = colorNew;
    }
}