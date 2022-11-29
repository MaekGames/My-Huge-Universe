using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWithObjects : MonoBehaviour
{
    public List<int> absorbentCountNeed;
    public List<int> absorbed;
    //public int redvalue;
    [SerializeField] int redvalueAbsorbed;
    //public int bluevalue;
    //[SerializeField] int bluevalueAbsorbed;
    PlayerInventory _playerInventory;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")){
            _playerInventory = other.gameObject.GetComponent<PlayerInventory>();
            for(int i = 0; i < absorbentCountNeed.Count; i++)
                StartCoroutine(StartAbsorbing(_playerInventory.spheres[i], i, 0.0f));
            if (Absorbed()) { OpenLocation();}
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
        if (absorbentCountNeed[0] == absorbed[0] && absorbentCountNeed[0] == absorbed[0])
        return true;
        else return false;
    }

    IEnumerator StartAbsorbing(int palyerObj, int absorbId,float timeoffset)
    {

        while (palyerObj > 0)
        {
            //if (absorbentCountNeed[absorbId] <= absorbed[absorbId]) break;
            yield return 1.5f + timeoffset;
            palyerObj--;
            _playerInventory.RemoveSphere(1, absorbId);
            absorbed[absorbId]++;
        }
    }
    private void OpenLocation()
    {
        Debug.Log("Absorbed finished");
    }
}
