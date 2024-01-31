using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CharacterState
{
    Spawn,
    Ready,
    Idle,
    Walking,
    Attacking,
    GetHit,
    Die
}

public class Character : MonoBehaviour
{
    public Character target;

    [Header("Character profile")]
    public CharacterProfile profile;


    [Header("Skills")]
    public List<Skill> skillList;
    private Dictionary<string, float> _skillsCooldownMap = new Dictionary<string, float>();

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

    private float _currentHealth = 0;

    public Action OnCharacterDie { get;set; }

    public float CurrentHP
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
            _characterUI.UpdateHealthBar(_currentHealth / profile.baseHP);
        }
    }

    public float DamageReceived { get; private set; }


    public Weapon Weapon { get; private set; }
    public CharacterState CurrentState { get; private set; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterUI = GetComponent<CharacterUI>();
        _controller = GetComponent<CharacterController>();
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (CurrentState == CharacterState.Spawn) return;

        UpdateSkillsCoodown();
        profile.rootAction.ProcessUpdate(this);
    }

    public void Reset()
    {
        _currentHealth = profile.baseHP;
        SetCharacterState(CharacterState.Spawn);
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

    public void PlayAnimState(string name)
    {
        _animator.CrossFadeInFixedTime(name, 0.2f, 0);
        _animator.CrossFadeInFixedTime(name, 0.2f, 1);
    }

    // Use for atk anims
    public void OnAnimEnd(string name)
    {
        SetCharacterState(CharacterState.Idle, "Idle");
        AttackDirection = null;
        DamageReceived = 0;
    }

    public void GetHit(float damage)
    {
        Debug.Log($"{gameObject.name} get hit -{damage} hp");
        CurrentHP -= damage;
        DamageReceived = damage;
    }

    public void SetCharacterState(CharacterState newState, string animState = "")
    {
        if (newState == CurrentState) return;

        CurrentState = newState;

        if (string.IsNullOrEmpty(animState) == false)
        {
            PlayAnimState(animState);
        }

        if(CurrentState == CharacterState.Die)
        {
            OnCharacterDie?.Invoke();
        }
    }

    public bool SkillInCooldown(string skillName)
    {
        if(_skillsCooldownMap.ContainsKey(skillName))
        {
            return _skillsCooldownMap[skillName] > 0;
        }
        return false;
    }

    public void RegisterSkillCooldown(string skillName, float cooldown)
    {
        _skillsCooldownMap[skillName] = cooldown;
    }

    private void UpdateSkillsCoodown()
    {
       if(_skillsCooldownMap.Count == 0) return;

        for(int i = 0; i < _skillsCooldownMap.Count; i++)
        {
            var key = _skillsCooldownMap.ElementAt(i).Key;
            if (_skillsCooldownMap[key] > 0) _skillsCooldownMap[key] -= Time.deltaTime;
        }
    }
}
