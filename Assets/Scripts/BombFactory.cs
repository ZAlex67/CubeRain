using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombFactory : SpawnerGeneric<Bomb>
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private float _coefficient;

    private Vector3 _bombPosition;
    private int _bombCount;
    private int _bombActiveCount;

    public event Action<int> BombNumberChanged;
    public event Action<int> BombActiveChanged;

    private IEnumerator TimeExplosion(Bomb bomb)
    {
        float minTime = 2f;
        float maxTime = 5f;
        float alphaStop = 0f;
        float alpha = 1f;

        float currentTime = Random.Range(minTime, maxTime);

        while (currentTime > float.Epsilon)
        {
            bomb.SetAlpha(Mathf.MoveTowards(bomb.Renderer.material.color.a, alphaStop, _coefficient * Time.deltaTime));
            currentTime -= Time.deltaTime;
            yield return null;
        }

        if (bomb.isActiveAndEnabled)
        {
            bomb.Explosion.Explode();
            Pool.Release(bomb);
            bomb.SetAlpha(alpha);
        }
    }

    public void SetPosition(Cube cube)
    {
        _bombPosition = cube.transform.position;
    }

    protected override Bomb Init()
    {
        _bombCount++;
        BombNumberChanged?.Invoke(_bombCount);

        return Instantiate(_bomb);
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.transform.position = _bombPosition;
        bomb.Rigidbody.velocity = Vector3.zero;
        bomb.gameObject.SetActive(true);

        StartCoroutine(TimeExplosion(bomb));

        _bombActiveCount = Pool.CountActive;
        BombActiveChanged?.Invoke(_bombActiveCount);
    }
}