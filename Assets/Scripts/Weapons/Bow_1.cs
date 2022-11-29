using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_1 : MonoBehaviour
{
    [SerializeField] private List<Damage> dmg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(dmg);
    }
}
