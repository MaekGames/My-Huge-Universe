using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveObjects : MonoBehaviour
{
    [SerializeField] List<int> absorbenttogive;
    [SerializeField] List<int> absorbed;
    [SerializeField] List<GameObject> absorbentPrefab;
    PlayerInventory _playerInventory;
    [SerializeField] Vector2 MinMaxSpawnRadius;
    [SerializeField] List<Transform> spawnPoint;
    [SerializeField] float timeToNextObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            for (int i = 0; i < absorbenttogive.Count; i++)
                StartCoroutine(StartGiveAbsorbing(absorbenttogive[i], i, timeToNextObj));
            //if (Absorbed()) { OpenLocation(); }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StopAllCoroutines();
        }
    }
    private bool Absorbed()
    {
        if (absorbenttogive[0] == absorbed[0] && absorbenttogive[0] == absorbed[0])
            return true;
        else return false;
    }

    IEnumerator StartGiveAbsorbing(int palyerObj, int absorbId, float timeoffset)
    {

        while (palyerObj > 0)
        {
            //if (absorbentCountNeed[absorbId] <= absorbed[absorbId]) break;
            yield return timeoffset;
            palyerObj--;
            //_playerInventory.AddSphere(1, absorbId);
            DropFlow(absorbId);
            //absorbed[absorbId]++;
        }
    }
    private void DropFlow(int dropId)
    {
        absorbentPrefab[dropId].GetComponent<ObjectMovingLogic>().target_2 = _playerInventory.transform;
        PoolManager.SpawnObject(absorbentPrefab[dropId], spawnPoint[0].position + new Vector3(Random.Range(MinMaxSpawnRadius.x, MinMaxSpawnRadius.y),
                                                                                              Random.Range(MinMaxSpawnRadius.x, MinMaxSpawnRadius.y),
                                                                                              Random.Range(MinMaxSpawnRadius.x, MinMaxSpawnRadius.y)), Quaternion.identity);
    }
    private void OpenLocation()
    {
        Debug.Log("Absorbed finished");
    }
}
