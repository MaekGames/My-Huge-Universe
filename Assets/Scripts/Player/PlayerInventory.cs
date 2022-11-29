using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Transform> spawnPoint;
    //[SerializeField] int _blueSphere;
    //[SerializeField] int _redSphere;
    //[SerializeField] int _greenSphere;
    [SerializeField] public List<int> spheres;
    [SerializeField] public List<GameObject> spherePrefabs;
    public bool allPointsFealed = false;
    //public int Spheres { get => spheres[id]; set => _redSphere = value; }
    private void Start()
    {
        PoolManager.WarmPool(spherePrefabs[0], 10);
        PoolManager.WarmPool(spherePrefabs[1], 10);
    }
    public void AddSphere(int val, int Id)
    {
        spheres[Id] += val;
    }
    public void RemoveSphere(int val, int Id)
    {
        spheres[Id] -= val;
        DropFlow(Id);
    }

    private void DropFlow(int dropId)
    {
        PoolManager.SpawnObject(spherePrefabs[dropId], spawnPoint[0].position, Quaternion.identity); //Random.Range(0, spawnPoint.Count)

    }

}
