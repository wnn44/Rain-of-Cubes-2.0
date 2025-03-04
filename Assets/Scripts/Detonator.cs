using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _upwardModifier;

    public void Explode()
    {
        foreach (Rigidbody explodubleObject in GetExplodableObjects())
        {
            explodubleObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardModifier);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                objects.Add(hit.attachedRigidbody);
            }
        }
        return objects;
    }
}

