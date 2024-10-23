using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
	[SerializeField] private Exploder _exploder;

	public Rigidbody _cubeRigidbody;
	private MeshRenderer _renderer;
	private float _maxChanceCreate = 100f;

	public event Action<Cube> Dividing;
	public event Action<Cube> CubeRemoved;
	
	public float CurrentChanceCreate { get; private set; } = 100f;

	public void Init(float chanceCreate) => CurrentChanceCreate = chanceCreate;

	private void Start() => _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

	private void Awake()
	{
		_renderer = GetComponent<MeshRenderer>();
		_cubeRigidbody = GetComponent<Rigidbody>();
	}

	private void OnMouseDown() => Explode();

	private void Explode()
	{
		if (CanDivide())
		{
			_exploder.Explode(_rigidbody);
			Dividing?.Invoke(this);
		}
		else
		{
			float cubeScale = _renderer.bounds.size.y;
			_exploder.Explode(_rigidbody, cubeScale);
		}

		CubeRemoved?.Invoke(this);
		Destroy(gameObject);
	}

	private bool CanDivide() => UnityEngine.Random.Range(0, _maxChanceCreate) < CurrentChanceCreate;
}