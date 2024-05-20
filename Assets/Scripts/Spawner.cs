using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;
    private Color[] _colors = new Color[] { Color.green, Color.black, Color.blue, Color.red };

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
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
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0f, _repeatRate);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube) && cube.Renderer.material.color == Color.white)
        {
            cube.SetColor(_colors[Random.Range(0, _colors.Length)]);
        }

        StartCoroutine(LifeTime(cube));
    }

    private IEnumerator LifeTime(Cube cube)
    {
        float minTime = 2f;
        float maxTime = 5f;

        WaitForSeconds waitRelease = new WaitForSeconds(Random.Range(minTime, maxTime));

        yield return waitRelease;

        if (cube.isActiveAndEnabled)
        {
            _pool.Release(cube);
            cube.SetColor(Color.white);
        }
    }
}