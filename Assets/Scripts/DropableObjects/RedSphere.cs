using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSphere : MonoBehaviour, Collectable
{
    public Color _newColor ;
    public GameObject _newPrefab;
    private int _value;
    [SerializeField] private AudioSource _sound;
    public float startingHealth = 100f;
    float m_CurrentHealth;

    public GameObject prefab
    {
        set
        {
            prefab = _newPrefab;
        }
    }
    public int Value { get => _value; set => this._value = value; }
    void Start()
    {

    }
    public void Color(Color newColor)
    {
        newColor -= _newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*PlayerInventory invt = null;
            if (collision.TryGetComponent<PlayerInventory>(out invt))
            {
                invt.AddDiamonds(_value);
                if (_sound != null)
                    _sound.Play();

                Destroy(this.gameObject);
            }*/
        }
    }
}
