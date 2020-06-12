using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Vector3 _direction = Vector3.down;
    private Player _player = null;
    private Animator _animator;
    private bool _isBeingDestroyed = false;
    private BoxCollider2D _collider;
    private AudioManager _audioManager;
    [SerializeField] GameObject _laser;
    [SerializeField] private Vector3 _spawnLaserOffset = new Vector3(0f, -1.75f, 0f);
    [SerializeField] private AudioClip _laserShotAudioClip;
    private AudioSource _laserShotAudioSrc;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)    
        {
            Debug.LogError("Player is null");
        }   
        
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component not found");
        }

        _collider = GetComponent<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("Collider component not found");
        }
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Unable to find game object audio manager");
        }
        if (_laserShotAudioClip == null)
        {
            Debug.LogError("Please assign Laser Audio Clip on the Enemy in the corresponding field in Unity Editor");
        }
        else
        {
            _laserShotAudioSrc = GetComponent<AudioSource>();
            _laserShotAudioSrc.clip = _laserShotAudioClip;
        }
        StartCoroutine(FireCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    
        if (!_isBeingDestroyed && transform.position.y < SpawnObjConst.yMin)
        {
            transform.position = new Vector3(Random.Range(SpawnObjConst.xMin, SpawnObjConst.xMax), SpawnObjConst.yMax, 0);
        }    
    }

    private void Fire()
    {
        Instantiate(_laser, transform.position + _spawnLaserOffset, Quaternion.identity);
        _laserShotAudioSrc.Play();
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield  return new WaitForSeconds(UnityEngine.Random.Range(.5f, 5f));
            Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                // if (_player != null)
                //     _player.Damage();
                DestroyEnemy();
                break;
            case "Laser":
                // Destroy(other.gameObject);
                if (_player != null)
                    _player.AddScore(10);
                DestroyEnemy();
                break;
        }
    }

    private void DestroyEnemy()
    {
        _isBeingDestroyed = true;
        _collider.enabled = false;
        _animator.SetTrigger("OnEnemyDeath");
        _audioManager.Play(AudioManager.ObjectAudio.Explosion);
        Destroy(this.gameObject, 2.8f);
    }
}
