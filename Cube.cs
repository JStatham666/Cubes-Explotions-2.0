using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
	[SerializeField] private Exploder _exploder;
	[SerializeField] private float _explosionRadius;
	[SerializeField] private float _explosionForce;

	private MeshRenderer _renderer;
	private float _maxChanceCreate = 100f;

	public event Action<Cube> Dividing;
	public event Action<Cube> CubeRemoved;

	public Rigidbody CubeRigidbody { get; private set; }
	public float CurrentChanceCreate { get; private set; } = 100f;

	public float ExplosionForce => _explosionForce;
	public float ExplosionRadius => _explosionRadius;

	public void Init(float chanceCreate) => CurrentChanceCreate = chanceCreate;

	private void Awake()
	{
		_renderer = GetComponent<MeshRenderer>();
		CubeRigidbody = GetComponent<Rigidbody>();
	}

	private void Start() => _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

	private void OnMouseDown() => Explode();

	private void Explode()
	{
		if (CanDivide())
		{
			_exploder.Explode(CubeRigidbody, transform.position, ExplosionForce, ExplosionRadius);
			Dividing?.Invoke(this);
		}
		else
		{
			float cubeScale = GetComponent<Renderer>().bounds.size.y;
			_exploder.Explode(GetExplodableObjects(), transform.position, ExplosionForce, ExplosionRadius, cubeScale);
		}

		CubeRemoved?.Invoke(this);
		Destroy(gameObject);

		Debug.Log(CurrentChanceCreate);
	}

	private List<Rigidbody> GetExplodableObjects()
	{
		Collider[] hits = Physics.OverlapSphere(transform.position, ExplosionRadius);

		List<Rigidbody> cubes = new();

		foreach (Collider hit in hits)
			if (hit.attachedRigidbody != null)
				cubes.Add(hit.attachedRigidbody);

		return cubes;
	}

	private bool CanDivide() => UnityEngine.Random.Range(0, _maxChanceCreate) < CurrentChanceCreate;
}