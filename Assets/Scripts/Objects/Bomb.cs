using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),
                  typeof(Rigidbody),
                  typeof(Explosion))]
public class Bomb : MonoBehaviour, IObject<Bomb>
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Explosion _explosion;

    private readonly float _defaultAlpha = 1f;

    public event Action<Bomb> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _explosion = GetComponent<Explosion>();
    }

    public void OnEnable()
    {
        StartCoroutine(SmoothDisappearance());
    }

    private IEnumerator SmoothDisappearance()
    {
        int _minLifeTime = 2;
        int _maxLifeTime = 6;
        int delay = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        float timeRemaining = delay;

        while (timeRemaining > 0)
        {
            SetAlpha(timeRemaining / delay);

            yield return null;

            timeRemaining -= Time.deltaTime;
        }

        _explosion.Explode();
        Destroyed?.Invoke(this);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }

    public void ResetToDefault()
    {
        SetAlpha(_defaultAlpha);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}