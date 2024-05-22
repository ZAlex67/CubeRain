using UnityEngine;

public class BombFactory : SpawnerGeneric<Bomb>
{
    private Vector3 _bombPosition;

    public void SetPosition(Cube cube)
    {
        _bombPosition = cube.transform.position;
    }

    protected override void InitPrefab(Bomb bomb)
    {
        bomb.SetBombFactory(this);
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
        ActionGetPrefab(bomb, _bombPosition);
        bomb.StartCoroutine(bomb.TimeExplosion());
    }
}