using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public int numEnemies = 10;
    public GameObject prefab;
    public Transform playerTransform;
    public float spawnRadius = 200;
    public int xPos;
    public int zPos;
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
        
        Vector3 centerPosition = playerTransform != null ? playerTransform.position : Vector3.zero;
        float childX = centerPosition.x + Random.Range(0, spawnRadius);
        float childZ = centerPosition.z + Random.Range(0, spawnRadius);
        Vector3 spawnPosition = new Vector3(childX, 10f, childZ);
        GameObject newChild = Instantiate(prefab, spawnPosition, Quaternion.identity);
        newChild.transform.SetParent(transform);
        
    }

    void SpawnOnPlat()
    {

    }
}
