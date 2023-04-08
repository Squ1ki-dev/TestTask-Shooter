using System;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(UltimateCharacterLocomotion))]
public partial class Enemy : MonoBehaviour, IDamageable
{
    private CharacterHealth _health;
    
    public bool IsDead { get; private set; }
    
    public event Action<Vector3> DiedEvent;
    
    private void Start() 
    {
        InitializeHealth();
        InitializeCharacter();

        EnemyLocomotion();
    }

    private void InitializeHealth()
    {
        IsDead = false;
        _health = GetComponent<CharacterHealth>();
        _health.OnDeathEvent.AddListener(OnDeath);
    }

    private void InitializeCharacter()
    {
        _transform = GetComponent<Transform>();
        _characterTransform = Singleton.Instance.Transform;
        _characterDamageable = Singleton.Instance.GetComponent<IDamageable>();
    }

    private void Update() 
    {
        if (_canDamage && TargetReached && !IsDead)
            StartCoroutine(DealDamage(_characterDamageable));

        if (IsDead == false)
            _agent.SetDestination(_character.position);
    }

    private void OnDeath(Vector3 deathPosition, Vector3 force, GameObject attacker)
    {
        IsDead = true;
        DiedEvent?.Invoke(deathPosition);
    }

    public void DealDamage(float damage) => _health.Damage(damage);

    public void Kill() => DealDamage(_health.Value); 
}
