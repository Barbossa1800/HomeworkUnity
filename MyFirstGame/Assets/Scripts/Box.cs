using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour
{
    [SerializeField] private Sprite _avtiveSprite;
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
            _spriteRenderer.sprite = _avtiveSprite;
            //_activated = true; - не нада!
            Debug.Log("Activated box");
        }
    }
}
