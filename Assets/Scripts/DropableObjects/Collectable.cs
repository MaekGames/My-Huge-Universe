using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Collectable 
{
    GameObject prefab { set; }
    int Value { get; set; }
    void Color(Color newColor);
}
