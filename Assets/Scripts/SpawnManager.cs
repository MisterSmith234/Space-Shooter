using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _isEnemySpawning = true;
    [SerializeField]
    private GameObject[] powerups;
    private bool _isPowerupSpawning = true;
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(this.SpawnEnemyRoutine());
        StartCoroutine(this.SpawnPowerupRoutine());
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-9.5f, 9.5f), 8f);
    }
    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_isEnemySpawning)
        {
           GameObject newEnemy =  Instantiate(_enemy,this.RandomPosition(),Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            float randomSeconds = Random.Range(2.5f, 5f);
            yield return new WaitForSeconds(randomSeconds);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_isPowerupSpawning)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(this.powerups[randomPowerup], this.RandomPosition(), Quaternion.identity);
            float randomSeconds = Random.Range(3f, 7f);
            yield return new WaitForSeconds(randomSeconds);
        }
    }

    public void OnPlayerDeath()
    {
        this._isEnemySpawning = false;
        this._isPowerupSpawning = false;
    }
}
