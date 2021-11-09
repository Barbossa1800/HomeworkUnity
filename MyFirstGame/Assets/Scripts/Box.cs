using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour
{
    float lastInteractionTime;

    private bool _itteractive = true;

    [SerializeField] private int _coinsAmount;
    public bool Activated { private get; set; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_itteractive)
        {
            return;
        }
        if (!Activated)
        {
            return;
        }

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && Time.time - lastInteractionTime > 0.02f )
        {
            lastInteractionTime = Time.time;
            player.CoinsAmount += _coinsAmount;
            _itteractive = false;
        }
        
        
    }
}
