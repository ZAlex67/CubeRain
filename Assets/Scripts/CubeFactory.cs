using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeFactory : SpawnerGeneric<Cube>
{
    [SerializeField] private Cube _cube;
    [SerializeField] private BombFactory _bombFactory;
    [SerializeField] private float _repeatRate = 1f;

    private int _cubeCount;
    private int _cubeActiveCount;

    public event Action<int> CubeNumberChanged;
    public event Action<int> CubeActiveChanged;

    private void Start()
    {
        StartCoroutine(RepeatCube());
    }

    public override void ObjectRelease(Cube cube)
    {
        Pool.Release(cube);
    }

    protected override Cube Init()
    {
        _cubeCount++;
        CubeNumberChanged?.Invoke(_cubeCount);
        _cube.SetCubeFactory(this);
        _cube.SetBombFactory(_bombFactory);

        return Instantiate(_cube);
    }

    protected override void ActionOnGet(Cube cube)
    {
        float minX = -20f;
        float maxX = 20f;
        float y = 20f;
        float minZ = -20f;
        float maxZ = 20f;

        Vector3 newPosition = new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));

        cube.transform.position = newPosition;
        cube.Rigidbody.velocity = Vector3.zero;
        cube.gameObject.SetActive(true);

        _cubeActiveCount = Pool.CountActive;
        CubeActiveChanged?.Invoke(_cubeActiveCount);
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