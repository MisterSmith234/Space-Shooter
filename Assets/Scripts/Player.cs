using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 5f;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField]
    private GameObject _laserObject;
    [SerializeField]
    private GameObject _tripleShotObject;
    private readonly Vector3 _laserOffset = new Vector3(0, 1f);
    private readonly Vector3 _shieldOffset = new Vector3(0, -0.25f);
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _speedBoost = 2f;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private GameObject _rightEngineDamage;
    [SerializeField]
    private GameObject _leftEngineDamage;
    [SerializeField]
    private AudioSource _shotAudio;
    [SerializeField]
    private AudioSource _explosionSound;


    void Start()
    {
        //Take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UI_Manager").GetComponent<UIManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Player: -> Spawn Manager is NULL");
        }
        if(_uiManager == null)
        {
            Debug.LogError("Player: -> UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.PlayerBounds();
        this.PlayerMovement();
        this.FireLaser();
    }

    private void PlayerMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(_horizontalInput,_verticalInput) * _speed * Time.deltaTime);
    }

    private void PlayerBounds()
    {
        //Top, Bottom bounds
        //Performanter als if else
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0));


        //left, right bounds
        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y);
        }
        else if(transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y);
        }
    }

    private void FireLaser()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive)
            {
                Instantiate(this._tripleShotObject, transform.position, Quaternion.identity);
            }
            else
            {                
                Instantiate(this._laserObject, transform.position + this._laserOffset, Quaternion.identity);
            }
            _shotAudio.Play();
        }
    }

    public void TripleShotActive()
    {
        this._isTripleShotActive = true;
        StartCoroutine(this.TripleShotPowerDownRoutine());
    }


    public void ShieldPowerupActive()
    {
        if (!this._shield.activeSelf)
        {
            this._shield.SetActive(true);
        }
    }
    public void SpeedPowerupActive()
    {
        this._speed += this._speedBoost;
        StartCoroutine(this.SpeedPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        this._isTripleShotActive = false;
    }
    private IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        this._speed -= this._speedBoost;
    }

    public void AddScore(int points)
    {
        this._score += points;
        _uiManager.UpdateScore(this._score);
    }
    public void Damage()
    {
        if (this._shield.activeSelf)
        {
            this._shield.SetActive(false);
            return;
        }

        

        this._lives--;

        switch(_lives)
        {
            case 2:
                _rightEngineDamage.SetActive(true);
                break;
            case 1:
                _leftEngineDamage.SetActive(true);
                break;
        }

        _uiManager.UpdateLives(this._lives);
        if (this._lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            _uiManager.OnPlayerDeath();
            _explosionSound.Play();
            Destroy(this.gameObject);
        }
    }
}
