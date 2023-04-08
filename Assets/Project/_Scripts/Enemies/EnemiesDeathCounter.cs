using UnityEngine;
using TMPro;

public class EnemiesDeathCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TMP_Text _deathCountText;

    private int _deathCount;

    private void Start()
    {
        _deathCount = 0;
        _deathCountText.text = _deathCount.ToString();
    }

    private void OnEnemyDie()
    {
        _deathCount++;
        _deathCountText.text = _deathCount.ToString();
    }

    private void OnEnable()
    {
        _enemySpawner.EnemyDeadEvent += OnEnemyDie;
    }

    private void OnDisable()
    {
        _enemySpawner.EnemyDeadEvent -= OnEnemyDie;
    }
}
