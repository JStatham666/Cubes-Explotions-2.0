using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private int _minCreate = 2;
    [SerializeField] private int _maxCreate = 6;
    [SerializeField] private int _chanceDivider = 2;
    [SerializeField] private int _scaleDivider = 2;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
        {
            cube.Dividing += Create;
            cube.CubeRemoved += cube => _cubes.Remove(cube);
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            cube.Dividing -= Create;
            cube.CubeRemoved -= cube => _cubes.Remove(cube);
        }
    }

    private void Create(Cube explodedCube)
    {
        float chanceCreate = explodedCube.CurrentChanceCreate;
        explodedCube.transform.localScale /= _scaleDivider;

        explodedCube.Dividing -= Create;
        explodedCube.CubeRemoved -= cube => _cubes.Remove(cube);
        _cubes.Remove(explodedCube);

        int amountOf—ubes = UnityEngine.Random.Range(_minCreate, _maxCreate + 1);

        for (int i = 0; i < amountOf—ubes; i++)
        {
            Cube cube = Instantiate(explodedCube, explodedCube.transform.position, Quaternion.identity);
            cube.Dividing += Create;
            _cubes.Add(cube);

            cube.Init(chanceCreate / _chanceDivider);
        }
    }