using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 10.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.AsteroidMovement();
    }

    private void AsteroidMovement()
    {
        this.transform.Rotate(Vector3.back * Time.deltaTime * _rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Laser":
                this.OnLaserHit(other);
                break;
        }
    }

    private void OnLaserHit(Collider2D other)
    {
        Instantiate(this._explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        _spawnManager.StartSpawning();
        Destroy(this.gameObject, 0.25f);

    }
}
