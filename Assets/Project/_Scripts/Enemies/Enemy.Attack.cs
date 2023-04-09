using System.Collections;
using UnityEngine;

public partial class Enemy
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDistance;

    [Header("In seconds")] 
    [SerializeField] private float _damageCooldown;

    private Transform _characterTransform;
    private Transform _transform;

    private IDamageable _characterDamageable;

    private bool TargetReached => Vector3.Distance(_transform.position,
        _characterTransform.position) <= _damageDistance;
    
    private bool _canDamage = true;

    private IEnumerator DealDamage(IDamageable damageable)
    {
        // Damage player
        _canDamage = false;

        damageable.DealDamage(_damage);
        
        yield return new WaitForSeconds(_damageCooldown);
        _canDamage = true;
    }
}
