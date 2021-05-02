using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private int _pointWorth;
    private Animator _animator;
    private AudioSource _explosionSound;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Enemy: -> Player is NULL");
        }
        _pointWorth = Random.Range(5, 13);

        _animator = this.gameObject.GetComponent<Animator>();

        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("Enemy: -> ExplosionSound is NULL");
        }
    }
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-9.5f, 9.5f), 8f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        //if out of screen
        if(transform.position.y < -6f)
        {
            //respawn on top with random x position
            transform.position = this.RandomPosition();
        }
    }


    private IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("TriggerEnter2D Player");
            this.OnPlayerHit(other);
        }
        if(other.tag == "Laser")
        {
            this.OnLaserHit(other);
        }
    }

    private void OnPlayerHit(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if(player != null)
        {
            player.Damage();
        }
        _animator.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _explosionSound.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(this.gameObject, 2.64f);
    }

    private void OnLaserHit(Collider2D other)
    {

        Destroy(other.gameObject);
        if (_player != null)
        {
            _player.AddScore(_pointWorth);
        }
        _animator.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _explosionSound.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(this.gameObject, 2.64f);
    }
}
