using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    public AudioClip[] playerSound;

    public Image damageScreen;
    private bool damaged = false;
    Color damageColor = new Color(255f, 30f, 30f, 0.2f);
    private float smoothColor = 1f;

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

    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle to rigidbody
        _rigidbody = GetComponent<Rigidbody2D>();

        //assign handle to playerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();

        _playerSprite = GetComponentInChildren<SpriteRenderer>();

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 4;

        damaged = false;

        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
            Movement();
        }

        //is left click && grounded attack
        //if (Input.GetMouseButtonDown(0) && IsGrounded())
        if (CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded())
        {
            _playerAnim.Attack();

            if (PlayerPrefs.GetInt("Muted") == 0)
            {
                if (_swordArcSprite.enabled)
                {
                    AudioSource.PlayClipAtPoint(playerSound[5], Camera.main.transform.position);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(playerSound[1], Camera.main.transform.position);
                }
            }
        }

        if (damaged)
        {
            damageScreen.color = damageColor;

            if (PlayerPrefs.GetInt("Muted") == 0)
            {
                AudioSource.PlayClipAtPoint(playerSound[4], Camera.main.transform.position);
            }
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor * Time.deltaTime);
        }

        damaged = false;
    }

    void Movement()
    {
        //horizontal input for left/right
        float move = Input.GetAxisRaw("Horizontal");
        //float move = CrossPlatformInputManager.GetAxis("Horizontal");

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

        //if ((Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            Debug.Log("Jump!");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());

            //tell animator to jump
            _playerAnim.Jump(true);

            if (PlayerPrefs.GetInt("Muted") == 0)
            {
                AudioSource.PlayClipAtPoint(playerSound[0], Camera.main.transform.position);
            }
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
        if (Health < 1)
        {
            return;
        }

        Debug.Log("asd");
        Health--;
        //_playerAnim.Hit();
        UIManager.Instance.RemoveLives(Health);

        damaged = true;

        if (Health < 1)
        {
            PlayerPrefs.SetString("waitDate", System.DateTime.Now.AddHours(1).AddMinutes(30).ToString());
            PlayerPrefs.Save();

            _playerAnim.Death();

            if (PlayerPrefs.GetInt("Muted") == 0)
            {
                AudioSource.PlayClipAtPoint(playerSound[2], Camera.main.transform.position);
            }

            levelManager.LoadLoseMenuAfterDelay();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(playerSound[3], Camera.main.transform.position);
        }

        UIManager.Instance.UpdateCoinCount(coins);
    }

    public void AddHealth(int amount)
    {
        if (Health != 4)
        {
            Health += amount;
        }

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(playerSound[6], Camera.main.transform.position);
        }

        UIManager.Instance.AddLives(Health);
    }

    public void AddSpeed(int amount)
    {
        _speed = amount;
        _jumpForce = 7.5f;

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(playerSound[6], Camera.main.transform.position);
        }

        StartCoroutine(ResetPowerUps());
    }

    public void EnableFlame()
    {
        _swordArcSprite.enabled = true;

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(playerSound[6], Camera.main.transform.position);
        }

        StartCoroutine(ResetPowerUps());
    }

    IEnumerator ResetPowerUps()
    {
        yield return new WaitForSeconds(10.0f);
        _speed = 2.5f;
        _jumpForce = 7.0f;
        _swordArcSprite.enabled = false;
    }

}