using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),
                  typeof(Explosion),
                  typeof(TransparencyChanger))]
public class Bomb : MonoBehaviour, IObject<Bomb>
{
    private Rigidbody _rigidbody;
    private Explosion _explosion;
    private TransparencyChanger _transparencyChanger;
 
    public event Action<Bomb> Destroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _explosion = GetComponent<Explosion>();
        _transparencyChanger = GetComponent<TransparencyChanger>();
    }

    public void OnEnable() =>
        StartCoroutine(TriggerExplosion());

    private IEnumerator TriggerExplosion()
    {
        int _minLifeTime = 2;
        int _maxLifeTime = 6;
        int delay = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        float timeRemaining = delay;

        _transparencyChanger.SetDelay(delay);

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            yield return null;
        }

        _explosion.Explode();
        Destroyed?.Invoke(this);
    }

    public void ResetToDefault()
    {
        _transparencyChanger.ResetAlpha();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}