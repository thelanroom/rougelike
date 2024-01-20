using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CharacterState
{
    Spawn,
    Idle,
    Walking,
    Attacking,
    GetHit,
    Die
}

public class Character : MonoBehaviour
{
    public Transform target;

    [Header("Character profile")]
    public CharacterProfile profile;


    [Header("Skills")]
    public List<Skill> skillList;
    private Dictionary<string, Skill> _skillMap;

    public Vector2 MoveInput { get; set; }
    public Vector3? AttackDirection { get; set; }

    //moving
    [Header("Movement config")]
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float rotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    [Range(10f, 100f)] public float speedChangeRate = 10.0f;
    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;

    private Animator _animator;
    private CharacterUI _characterUI;
    private CharacterController _controller;

    public string CurrentAnimState { get; private set; }

    private float _currentHealth = 0;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
            _characterUI.UpdateHealthBar(_currentHealth / profile.baseHealth);
        }
    }


    public Weapon Weapon { get; private set; }
    public CharacterState CurrentState { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterUI = GetComponent<CharacterUI>();
        _controller = GetComponent<CharacterController>();
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        CurrentHealth = profile.baseHealth;
    }

    private void Update()
    {
        profile.rootAction.ProcessUpdate(this);
    }

    public void Move()
    {
        float targetSpeed = profile.baseSpeed;
        if (MoveInput == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(MoveInput.x, 0.0f, MoveInput.y).normalized;

        if (MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime));
    }

    public void UseSkill(Skill skill, string anim)
    {
        transform.rotation = Quaternion.LookRotation((Vector3)AttackDirection);
        Weapon.EnableDealDamge(skill.Damage);
        SetCharacterState(CharacterState.Attacking, anim);
    }

    public void PlayAnimState(string name)
    {
        _animator.CrossFadeInFixedTime(name, 0.2f, 0);
        _animator.CrossFadeInFixedTime(name, 0.2f, 1);
    }

    // Use for atk anims
    public void OnAnimEnd(string name)
    {
        SetCharacterState(CharacterState.Idle, "");
        AttackDirection = null;
    }

    public void GetHit(float damage)
    {
        Debug.Log($"{gameObject.name} get hit -{damage} hp");
        CurrentHealth -= damage;
        /*  if(CurrentHealth <= 0)
          {
              SetCharacterState(CharacterState.Die);
          }
          else
          {
              SetCharacterState(CharacterState.GetHit);
          }*/
    }

    public void SetCharacterState(CharacterState newState, string animState = "")
    {
        if (newState == CurrentState) return;

        CurrentState = newState;

        if (string.IsNullOrEmpty(animState) == false)
        {
            PlayAnimState(animState);
        }
    }
}
