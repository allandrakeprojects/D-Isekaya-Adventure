﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    public int coins;

    //get handle to rigidbody
    private Rigidbody2D _rigidbody;
    //variable for jumpForce
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 2.5f;

    private bool _grounded = false;

    //handle to playerAnimation
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle to rigidbody
        _rigidbody = GetComponent<Rigidbody2D>();

        //assign handle to playerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();

        _playerSprite = GetComponentInChildren<SpriteRenderer>();

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //is left click && grounded attack
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        //horizontal input for left/right
        float move = Input.GetAxisRaw("Horizontal");

        _grounded = IsGrounded();

        //if move is greater than 0 facing right
        //if move is less than 0 facing left
        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        //if space key && grounded == true
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Debug.Log("Jump!");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());

            //tell animator to jump
            _playerAnim.Jump(true);
        }

        _rigidbody.velocity = new Vector2(move * _speed, _rigidbody.velocity.y);

        _playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
    
        if (hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                //set animator jump bool to false
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        Debug.Log("asd");
        //Health--;

        //if (Health < 1)
        //{
        //    anim.SetTrigger("Death");
        //}
    }
}