using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class RoadLamp : MonoBehaviour
{
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Box _box;
    
    private SpriteRenderer _spriteRenderer;
    private Sprite _inactiveSprite;

    private bool _activated;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inactiveSprite = _spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && !_activated)
        {
            _spriteRenderer.sprite = _activeSprite;
            _activated = true; //- не нада!
            _box.Activated = true;
            Debug.Log("Activated lamp");
        }
        else
        {
            _spriteRenderer.sprite = _inactiveSprite;
            _activated = false; //- не нада!
            Debug.Log("Deactivated lamp");
        }
    }
}
