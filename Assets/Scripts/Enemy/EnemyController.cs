using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> enemys;
    public List<Transform> spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        CreateEnemy(spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
    }

    public void CreateEnemy(Vector3 position, Quaternion rotation)
    {
        var enemy = PoolManager.SpawnObject(enemys[0], position, rotation).GetComponent<Bullet>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
