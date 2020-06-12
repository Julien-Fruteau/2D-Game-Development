using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    protected Vector3 _direction = Vector3.up;
    [SerializeField]
    protected float _speed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.y > SpawnObjConst.yMax || transform.position.y < SpawnObjConst.yMin)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Destroy(this.gameObject);
                break;
        }
    }
}
