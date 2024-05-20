using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 500f;

    private List<Rigidbody> GetExplodableObject(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        List<Rigidbody> barrels = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                barrels.Add(hit.attachedRigidbody);
            }
        }

        return barrels;
    }

    public void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObject(_explosionRadius))
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}