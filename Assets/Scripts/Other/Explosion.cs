using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _power;

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,
                                                        _radius);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
                rigidbody.AddExplosionForce(_power, transform.position, _radius);
        }
    }
}