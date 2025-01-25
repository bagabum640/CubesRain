using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<TObject> : MonoBehaviour where TObject : MonoBehaviour, IObject<TObject>
{
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int PoolMaxSize;

    [SerializeField] private TObject _prefab;

    protected ObjectPool<TObject> Pool;

    public event Action<int,int,int> AmountChanged;

    public int CreatedAllTime { get; private set; } = 0;
    public int CountAll => Pool.CountAll;
    public int CountActive => Pool.CountActive;

    private void Awake()
    {
        Pool = new ObjectPool<TObject>(
            createFunc: () => Instantiate(_prefab, transform, true),
            actionOnGet: (@object) => SetUp(@object),
            actionOnRelease: (@object) => ResetObject(@object),
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaxSize);
    }

    public void Spawn(Vector3 spawnPosition)
    {
        TObject @object = Pool.Get();    
        @object.transform.position = spawnPosition;
        CreatedAllTime++;
    }

    protected virtual void SetUp(TObject @object)
    {
        @object.IsDestroyed += Release;
        @object.gameObject.SetActive(true);
    }

    protected void ResetObject(TObject @object)
    {
        @object.IsDestroyed -= Release;
        @object.gameObject.SetActive(false);
        @object.Reset();
    }

    protected virtual void Release(TObject @object) => Pool.Release(@object);
}