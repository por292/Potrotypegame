using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public int numEnemies = 10;
    public GameObject prefab;
    public Transform playerTransform;
    public float spawnRadius = 20;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            makeAChild();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < numEnemies)
        {
            makeAChild();
        }
    }
    void makeAChild()
    {
        if (prefab == null)
        {
            UnityEngine.Debug.LogError("Enemy prefab is missing!");
            return;
        }

        Vector3 centerPosition = playerTransform != null ? playerTransform.position : Vector3.zero;
        float childX = centerPosition.x + UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        float childZ = centerPosition.z + UnityEngine.Random.Range(-spawnRadius, spawnRadius);

        Vector3 spawnPosition = new Vector3(childX, centerPosition.y, childZ);
        UnityEngine.AI.NavMeshHit hit;

        if (UnityEngine.AI.NavMesh.SamplePosition(spawnPosition, out hit, 20f, UnityEngine.AI.NavMesh.AllAreas))
        {
            GameObject newChild = Instantiate(prefab, hit.position, Quaternion.identity);
            newChild.transform.SetParent(transform);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No NavMesh found near spawn position!");
        }

    }
}
