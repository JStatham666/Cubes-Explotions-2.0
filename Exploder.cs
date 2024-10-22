using System;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(Rigidbody cube, Vector3 explodePosition, float explosionForce, float explosionRadius)
    {
        cube.AddExplosionForce(explosionForce, explodePosition, explosionRadius);
    }

    public void Explode(List<Rigidbody> explodableObjects, Vector3 explodePosition, float explosionForce, float explosionRadius, float cubeScale)
    {
        foreach (Rigidbody explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(explosionForce / cubeScale, explodePosition, explosionRadius / cubeScale);
    }
}