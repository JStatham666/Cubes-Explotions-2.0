using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode(Rigidbody cube)
    {
        cube.AddExplosionForce(_explosionForce, cube.position, _explosionRadius);
    }

    public void Explode(Rigidbody cube, float cubeScale)
    {
        List<Rigidbody> explodableObjects = GetExplodableObjects(cube);

        foreach (Rigidbody explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce / cubeScale, cube.position, _explosionRadius / cubeScale);
    }

    private List<Rigidbody> GetExplodableObjects(Rigidbody cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}