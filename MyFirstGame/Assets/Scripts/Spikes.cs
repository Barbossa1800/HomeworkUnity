using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay = 1f;
    private float _lastDamageType;
    private PlayerController _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == _player)
        {
            _player = null;
        }
    }

    private void Update()
    {
        if (_player != null && Time.time - _lastDamageType > _damageDelay)
        {
            _lastDamageType = Time.time;
            _player.TakeDamage(_damage);
        }
    }
}