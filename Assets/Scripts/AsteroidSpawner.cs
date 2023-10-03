using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _asteroidsPrefabs;
    [SerializeField] private float _secondsBetweenAsteroids = 0.5f;
    [SerializeField] private Vector2 _forceRange;

    private Camera _mainCamera;
    private float _timer;
    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <=0)
        {
            SpawnAsteroids();
            _timer += _secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroids()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0:
            //left
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
            //right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
            //bottom
                spawnPoint.y = 0;
                spawnPoint.x = Random.value;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
            //top
                spawnPoint.y = 1;
                spawnPoint.x = Random.value;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = _mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

        GameObject selectedAsteroid = _asteroidsPrefabs[Random.Range(0, _asteroidsPrefabs.Length)];

        GameObject asteroidInstance = Instantiate(selectedAsteroid, worldSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * Random.Range(_forceRange.x, _forceRange.y);

    }
}
