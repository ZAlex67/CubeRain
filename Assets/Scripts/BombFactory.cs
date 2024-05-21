using System;
using UnityEngine;

public class BombFactory : SpawnerGeneric<Bomb>
{
    [SerializeField] private Bomb _bomb;

    private Vector3 _bombPosition;
    private int _bombCount;
    private int _bombActiveCount;

    public event Action<int> BombNumberChanged;
    public event Action<int> BombActiveChanged;

    public override void ObjectRelease(Bomb bomb)
    {
        Pool.Release(bomb);
    }

    public void SetPosition(Cube cube)
    {
        _bombPosition = cube.transform.position;
    }

    protected override Bomb Init()
    {
        _bombCount++;
        BombNumberChanged?.Invoke(_bombCount);
        _bomb.SetBombFactory(this);

        return Instantiate(_bomb);
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.transform.position = _bombPosition;
        bomb.Rigidbody.velocity = Vector3.zero;
        bomb.gameObject.SetActive(true);

        StartCoroutine(bomb.TimeExplosion());

        _bombActiveCount = Pool.CountActive;
        BombActiveChanged?.Invoke(_bombActiveCount);
    }
}