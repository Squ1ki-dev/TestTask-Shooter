using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class Enemy
{
    [SerializeField] private float _deathPushForce;
    [Range(0, 5)]
    [SerializeField] private float _yPush;

    [SerializeField] private ParticleSystem _deathParticle;
    private UltimateCharacterLocomotion _locomotion;

    private Transform _character;
    private NavMeshAgent _agent;

    private void EnemyLocomotion()
    {
        _character = Singleton.Instance.Transform;
        _agent = GetComponent<NavMeshAgent>();

        _locomotion = GetComponent<UltimateCharacterLocomotion>();
        _locomotion.GetAbility<Ragdoll>().StartOnDeath = true;
        _locomotion.GetAbility<Ragdoll>().UseGravity = Ability.AbilityBoolOverride.True;
    }

    private void OnDeath(Vector3 deathPosition)
    {
        Vector3 pushDirection = Vector3.Normalize(deathPosition - _character.position);
        pushDirection.y = _yPush;
        _locomotion.AddForce(pushDirection * _deathPushForce);
        _deathParticle.Play();
    }
    
    private void OnEnable() => DiedEvent += OnDeath;

    private void OnDisable() => DiedEvent -= OnDeath;
}