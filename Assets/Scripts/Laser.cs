using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update

    private float _speed;
    void Start()
    {
        this._speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        this.LaserMovement();
        this.DestroyLaser();
    }


    private void DestroyLaser()
    {
        if(transform.position.y > 8f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);    
            }
            Destroy(this.gameObject);
        }
    }
    private void LaserMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }
}
