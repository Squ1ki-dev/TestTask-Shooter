using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _deathParticles;

    public void PlayRandom()
    {
        _deathParticles[Random.Range(0, _deathParticles.Length)].Play();
    }
}
