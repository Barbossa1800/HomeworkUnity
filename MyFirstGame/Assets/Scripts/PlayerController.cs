using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))] //закреплени,  для удержания от изменений в юнити в Rigidbody (защита от дурака)

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _spead; //
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsCell;
    
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Collider2D _headColider;
    [SerializeField] private float _headCheckerRadius;
    [SerializeField] private Transform _headChecker;
    
    [SerializeField] private int _maxHp; 
    private int _currentHp;
    
    [Header(("Animation"))] 
    [SerializeField] private Animator _animator;
    
    [Header("UI")] 
    [SerializeField] private TMP_Text _coinAmountText;

    [SerializeField] private Slider _hpBar;
    
    //[SerializeField] private Slider _hpBar;
    
    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;
    
    private float _horizontalRoute;
    private float _verticalRoute;
    private bool _jump;
    private bool _craw;
    
    private int _coinsAmount;
  
    
    public bool CanClimb { private  get; set; }
    
   
    public int CoinsAmount
    {
        get => _coinsAmount;
        set
        {
            _coinsAmount = value;
            _coinAmountText.text = value.ToString();
        }
    } 
    private int CurrentHp
    {
        get => _currentHp;
        set
        {
            if (value > _maxHp)
            {
                value = _maxHp;
            }
            _currentHp = value;
            _hpBar.value = value;
        }
    }
    private void Start()
    {
        CoinsAmount = 0;
        _hpBar.maxValue = _maxHp;
        CurrentHp = _maxHp;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _horizontalRoute = Input.GetAxisRaw("Horizontal"); // -1 - 0, если А или <-, 0 -1, D or -> + геймпапд так же.
        _verticalRoute = Input.GetAxisRaw("Vertical");
         _animator.SetFloat(_runAnimatorKey, Mathf.Abs(_horizontalRoute));
         
         if (Input.GetKeyDown(KeyCode.Space)  )
         {
             _jump = true;
            
         }
         // два способа офнуть колайдер перса, для норм прыжков (1 так се, 2 топ)
        if (_horizontalRoute > 0 && _spriteRenderer.flipX ) //flipX = bool
        {
            _spriteRenderer.flipX = false;
        }
        else if(_horizontalRoute < 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
        _craw = Input.GetKey(KeyCode.C);
    }

    private void FixedUpdate() //Uчерез фикс. промж. времен. (proj/ settings/Times/Fixed TimeStep
    {
        _rigidbody.velocity = new Vector2(_horizontalRoute * _spead, _rigidbody.velocity.y);

        if (CanClimb)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _verticalRoute * _spead);
            _rigidbody.gravityScale = 0;
        }
        else
        {
            _rigidbody.gravityScale = 1;
        }
        
        bool canJump = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround); //2 способо, ласт парам. -> _WhatIsGround
        bool canStand = !Physics2D.OverlapCircle(_headChecker.position, _headCheckerRadius, _whatIsCell); //??????????? !Ph..

        _headColider.enabled = !_craw && canStand;
        if (_jump && canJump)
        {
            _rigidbody.AddForce(Vector2.up* _jumpForce);
            _jump = false;
        }
        
        _animator.SetBool(_jumpAnimatorKey, !canJump);
        _animator.SetBool(_crouchAnimatorKey, !_headColider.enabled);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_headChecker.position, _headCheckerRadius);
    }

    public void AddHp(int hpPoints)
    {
        
        int missingHp = _maxHp - CurrentHp;
        int pointToAdd = missingHp > hpPoints ? hpPoints : missingHp;
        StartCoroutine(RestoreHp(pointToAdd));
    }

    private IEnumerator RestoreHp(int pointToAdd)
    {
        
        while (pointToAdd != 0)
        {
            pointToAdd--;
            CurrentHp++;
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if (_currentHp <= 0)
        {
            Debug.Log("Died!!!");
            gameObject.SetActive(false);
            Invoke(nameof(ReloadScene), 1f);
        }
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}