using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : Laser
{
    
    private void Start()
    {
        _direction = Vector3.down;
        _speed = 12f;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                Destroy(this.gameObject);
                break;
        }
    }
}
