using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D _rigidBody;
    Animator _animator;


    private float _horizontal;
    [SerializeField]
    private float _speed, _jumpSpeed;
    private bool _jumpControl=true;
    [SerializeField] int _heal;

    [SerializeField] Text _healText;

    private Vector3 _cameraFirstPos,_cameraLastPos;

    private GameObject _camera;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator=GetComponent<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraFirstPos = _camera.transform.position-transform.position;
        _heal = 100;
        _healText.text = $"HEAL {_heal}";
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
        _heal -= damage;
        _healText.text = $"HEAL {_heal}";
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

}
