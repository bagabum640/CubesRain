using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Vector3 _minPosition;
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _repeatRate;

    readonly private bool _isWorking = true;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab, transform, true),
            actionOnGet: (cube) => SetUp(cube),
            actionOnRelease: (cube) => ResetCube(cube),
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start() => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        while (_isWorking)
        {
            _pool.Get();
            yield return new WaitForSeconds(_repeatRate);
        }
    }

    private void Release(Cube cube) => _pool.Release(cube);   

    private void SetUp(Cube cube)
    {
        cube.OnTouched += Release;
        cube.transform.position = GetSpawnPosition();
        cube.gameObject.SetActive(true);
    }

    private void ResetCube(Cube cube)
    {
        cube.OnTouched -= Release;
        cube.gameObject.SetActive(false);
        cube.Reset();
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(_minPosition.x, _maxPosition.x),
                           Random.Range(_minPosition.y, _maxPosition.y),
                           Random.Range(_minPosition.z, _maxPosition.z));
    }
}