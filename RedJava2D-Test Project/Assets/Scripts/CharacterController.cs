using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D _rigidBody;
    Animator _animator;
    [SerializeField] Image _deathTransition;

    private float _horizontal;
    [SerializeField]
    private float _speed, _jumpSpeed;
    float _deathTransitionCount = 0;
    private bool _jumpControl=true;
    [SerializeField] int _health;

    [SerializeField] Text _healthText;

    private Vector3 _cameraFirstPos,_cameraLastPos;

    private GameObject _camera;

    void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("levelcount") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("levelcount", SceneManager.GetActiveScene().buildIndex);
        }
        
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator=GetComponent<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraFirstPos = _camera.transform.position-transform.position;
        _health = 100;
        UpdateHealthText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&_jumpControl)
        {
            _rigidBody.AddForce(new Vector2(0, _jumpSpeed));
            _jumpControl = false;
        }
    }
    private void LateUpdate()
    {
        CameraControl();
    }

    void FixedUpdate()
    {
        CharacterMovement();
        Animation();
        Death();
    }

    void CharacterMovement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 vec = new Vector3(_horizontal * _speed, _rigidBody.velocity.y, 0);

        _rigidBody.velocity = vec;
        if (_horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*_horizontal, transform.localScale.y, transform.localScale.z);
        }
        
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        UpdateHealthText();
    }

    void Death()
    {
        if (_health <= 0)
        {
            Time.timeScale = 0.5f;
            _deathTransitionCount += 0.03f;
            _healthText.gameObject.SetActive(false);
            _deathTransition.gameObject.SetActive(true);
            _deathTransition.color = new Color(0, 0, 0, _deathTransitionCount);
            if (_deathTransitionCount >= 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void Heal(int _healValue)
    {
        _health += _healValue;
        UpdateHealthText();
    }

    void CameraControl()
    {
        _cameraLastPos = _cameraFirstPos + transform.position;
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraLastPos, 0.03f);
    }

    void Animation()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_horizontal));
        _animator.SetBool("IsJumping", !_jumpControl);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _jumpControl = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Saw")
        {
            TakeDamage(100);
        }
        else if (collision.tag == "CubeEnemy")
        {
            TakeDamage(50);

        }
        else if (collision.tag == "Fire")
        {
            TakeDamage(20);
        }
        if (collision.tag == "LevelEnd")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.tag == "GameEnd")
        {
            SceneManager.LoadScene(0);
        }
        if (collision.tag == "HealBox")
        {
            Heal(20);
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Animator>().SetBool("IsOpening", true);
            Destroy(collision.gameObject,3);
        }
    }

    void UpdateHealthText()
    {
        _healthText.text = $"HEAL {_health}";
    }

}
