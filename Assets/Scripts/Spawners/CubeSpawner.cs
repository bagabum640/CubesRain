using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private Vector3 _minPosition;
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private float _repeatRate;

    private void Start() => StartCoroutine(CubeSpawn());

    private IEnumerator CubeSpawn()
    {
        while (enabled)
        {
            Spawn(GetSpawnPosition());
            yield return new WaitForSeconds(_repeatRate);
        }
    }

    protected override void SetUp(Cube cube)
    {
        cube.Destroyed += Release;
        cube.gameObject.SetActive(true);
    }

    protected override void Release(Cube cube)
    {
        base.Release(cube);
        _bombSpawner.Spawn(cube.transform.position);
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(_minPosition.x, _maxPosition.x),
                           Random.Range(_minPosition.y, _maxPosition.y),
                           Random.Range(_minPosition.z, _maxPosition.z));
    }
}