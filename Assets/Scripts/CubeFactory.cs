using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeFactory : SpawnerGeneric<Cube>
{
    [SerializeField] private Cube _cube;
    [SerializeField] private BombFactory _bombFactory;
    [SerializeField] private float _repeatRate = 1f;

    private Color[] _colors = new Color[] { Color.green, Color.black, Color.blue, Color.red };
    private int _cubeCount;
    private int _cubeActiveCount;

    public event Action<int> CubeNumberChanged;
    public event Action<int> CubeActiveChanged;

    private void Start()
    {
        InvokeRepeating(nameof(GetPrefab), 0f, _repeatRate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube) && cube.Renderer.material.color == Color.white)
        {
            cube.SetColor(_colors[Random.Range(0, _colors.Length)]);
            StartCoroutine(LifeTime(cube));
        }
    }

    private IEnumerator LifeTime(Cube cube)
    {
        float minTime = 2f;
        float maxTime = 5f;

        WaitForSeconds waitRelease = new WaitForSeconds(Random.Range(minTime, maxTime));

        yield return waitRelease;

        if (cube.isActiveAndEnabled)
        {
            Pool.Release(cube);
            cube.SetColor(Color.white);
            _bombFactory.SetPosition(cube);
            _bombFactory.GetPrefab();
        }
    }

    protected override Cube Init()
    {
        _cubeCount++;
        CubeNumberChanged?.Invoke(_cubeCount);
        
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
}