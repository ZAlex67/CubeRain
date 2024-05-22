using System.Collections;
using UnityEngine;

public class CubeFactory : SpawnerGeneric<Cube>
{
    [SerializeField] private BombFactory _bombFactory;
    [SerializeField] private float _repeatRate = 1f;

    private void Start()
    {
        StartCoroutine(RepeatCube());
    }

    protected override void InitPrefab(Cube cube)
    {
        cube.SetBombFactory(_bombFactory);
        cube.SetCubeFactory(this);
    }

    protected override void ActionOnGet(Cube cube)
    {
        float minX = -20f;
        float maxX = 20f;
        float y = 20f;
        float minZ = -20f;
        float maxZ = 20f;

        Vector3 newPosition = new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));

        base.ActionOnGet(cube);
        ActionGetPrefab(cube, newPosition);
    }

    private IEnumerator RepeatCube()
    {
        WaitForSeconds wait = new WaitForSeconds(_repeatRate);

        while (true)
        {
            GetPrefab();
            yield return wait;
        }
    }
}