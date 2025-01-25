using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),
                  typeof(Rigidbody))]
public class Cube : MonoBehaviour, IObject<Cube>
{
    private MeshRenderer _cubeMesh;
    private Rigidbody _rigidbody;
    private bool _isActive;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _cubeMesh = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() =>
        ResetToDefault();

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive == false && collision.gameObject.TryGetComponent<ColorChanger>(out ColorChanger colorChanger))
        {
            _isActive = true;
            _cubeMesh.material.color = colorChanger.GetColor();
            StartCoroutine(WaitForRelease());
        }
    }

    private IEnumerator WaitForRelease()
    {
        int _minLifeTime = 2;
        int _maxLifeTime = 6;
        int delay = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(delay);

        Destroyed?.Invoke(this);
    }

    public void ResetToDefault()
    {
        _isActive = false;
        _cubeMesh.material.color = Color.white;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}