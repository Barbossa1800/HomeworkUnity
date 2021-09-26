using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
[RequireComponent(typeof(Rigidbody2D))] //закреплени,  для удержания от изменений в юнити в Rigidbody (защита от дурака)

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _spead; //
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Collider2D _headColider;
    [SerializeField] private float _headCheckerRadius;
    [SerializeField] private Transform _headChecker;

    private float _route;
    private bool _jump;
    private bool _craw;
    // Start is called before the first frame update
    private void Start()
    {
       // Debug.Log("Hello my Leader!");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() // работает при каждом обновлении кадра
    {
         _route = Input.GetAxisRaw("Horizontal"); // -1 - 0, если А или <-, 0 -1, D or -> + геймпапд так же.
        
         if (Input.GetKeyDown(KeyCode.Space)  ) //не Axic, бо 2 вариант. Оси можно добаваить
         {
             _jump = true;
            
         }
         // два способа офнуть колайдер перса, для норм прыжков (1 так се, 2 топ)
        if (_route > 0 && _spriteRenderer.flipX ) //flipX = bool
        {
            _spriteRenderer.flipX = false;
        }
        else if(_route < 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
       
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundChecker.position, _groundCheckerRadius); //1 способ, масив колайдеров всех
        if (colliders.Length>1)
        {
            cnaJump = true; //1
        }*/
        _craw = Input.GetKey(KeyCode.C);
    }

    private void FixedUpdate() //Uчерез фикс. промж. времен. (proj/ settings/Times/Fixed TimeStep
    {
        _rigidbody.velocity = new Vector2(_route * _spead, _rigidbody.velocity.y);
        bool canJump = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround); //2 способо, лась парам. -> _WhatIsGround
        bool canStand = Physics2D.OverlapCircle(_headChecker.position, _headCheckerRadius, _whatIsGround); //??????????? !Ph...
       // Debug.Log(  !_craw && canStand);
        _headColider.enabled = !_craw && canStand;
        if (_jump && canJump)
        {
            _rigidbody.AddForce(Vector2.up* _jumpForce);
            _jump = false;
        }
        

        
    }

    private void OnDrawGizmos() // будет возвр. рез. есть ли колайдеры в радиусе
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_headChecker.position, _headCheckerRadius);
    }
}
