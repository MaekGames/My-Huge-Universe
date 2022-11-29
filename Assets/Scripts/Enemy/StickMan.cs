using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : Enemy, IDamageable
{
    public GameObject _droppablePrefab;
    public int _droppableValue;
    public int Health { get => _health; set => _health = value; }

    public void Damage(List<Damage> dmg)
    {
        foreach (var d in dmg)
        {
            Health -= d.amount;
            Debug.Log("Giant hit with " + d.type + " type attack");
        }
    }
    protected override void Kill()
    {
        this.DropDiamonds();
        //_enemy.Audio.StopAttackAudio();
       // _enemy.Audio.PlayDeathAudio();
        //_enemy.Animator.SetTrigger("onDeath");
        GetComponent<Collider2D>().enabled = false;
    }

    protected override void DropDiamonds()
    {
        if (_droppableValue > 0)
        {
            GameObject diamond = Instantiate(_droppablePrefab, transform.position, Quaternion.identity, null);
            diamond.GetComponent<Collectable>().Value = _droppableValue;
        }
    }
}
