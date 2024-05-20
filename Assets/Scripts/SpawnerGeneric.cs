using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerGeneric<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    protected ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => Init(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public virtual void GetPrefab()
    {
        Pool.Get();
    }

    protected abstract T Init();
    protected abstract void ActionOnGet(T prefab);
}