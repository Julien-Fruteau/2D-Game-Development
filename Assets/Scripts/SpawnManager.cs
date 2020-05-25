﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private GameObject _enemyContainer = null;

    private bool _stopSpawning = false;
    
    void Start()
    {
        if (_enemyPrefab == null)
        {
            throw new System.Exception("Please assign an enemy prefab to the corresponding field in Unity Editor");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnEnemies()
    {
        while (!_stopSpawning)
        {
            if (_enemyPrefab != null && _enemyContainer != null)
            {
                Vector3 spawnLocation = new Vector3(UnityEngine.Random.Range(EnemyConst.xMin, EnemyConst.xMax), EnemyConst.ySpawn, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, spawnLocation, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(5f);
            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
