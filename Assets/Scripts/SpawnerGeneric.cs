using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerGeneric<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private int _count;
    private int _activeCount;

    private ObjectPool<T> _pool;

    public event Action<int> NumberChanged;
    public event Action<int> ActiveChanged;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Init(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public virtual void GetPrefab()
    {
        _pool.Get();
    }

    public virtual void ReleaseObject(T prefab)
    {
        _pool.Release(prefab); 
    }

    protected virtual T Init()
    {
        _count++;
        NumberChanged?.Invoke(_count);
        InitPrefab(_prefab);

        return Instantiate(_prefab);
    }

    protected virtual void ActionOnGet(T prefab)
    {
        _activeCount = _pool.CountActive;
        ActiveChanged?.Invoke(_activeCount);
    }

    protected virtual void ActionGetPrefab(T prefab, Vector3 position)
    {
        prefab.transform.position = position;
        prefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
        prefab.gameObject.SetActive(true);
    }

    protected abstract void InitPrefab(T prefab);
}