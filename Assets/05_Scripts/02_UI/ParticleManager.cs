using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleManager : MonoBehaviour
{
    static ParticleManager instance;
    public static ParticleManager Instance => instance;
    
    [SerializeField] DamageParticle damagePrefab;
    [SerializeField] ParticleSystem particlePrefab;
    [SerializeField] float createRange;
    [SerializeField] float lifeTime;

    ObjectPool<DamageParticle> damagePool;
    ObjectPool<ParticleSystem> particlePool;
    WaitForSeconds wait;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        wait = new WaitForSeconds(lifeTime);

        damagePool = new ObjectPool<DamageParticle>(
            () => Instantiate(damagePrefab, transform),
            (textDamage) => textDamage.gameObject.SetActive(true),
            (textDamage) => textDamage.gameObject.SetActive(false),
            (textDamage) => Destroy(textDamage.gameObject),
            false,
            10,
            30
            );

        particlePool = new ObjectPool<ParticleSystem>(
            () => Instantiate(particlePrefab, transform),
            (particle) => particle.gameObject.SetActive(true),
            (particle) => particle.gameObject.SetActive(false),
            (particle) => Destroy(particle.gameObject),
            false,
            10,
            30
            );
    }

    public void CreateDamageParticle(Vector3 position, string damage, Color color, bool isCri = false)
    {
        // ������ ������ġ ����
        Vector3 vecRand = Random.insideUnitCircle * createRange;

        // ������ �ؽ�Ʈ ����
        DamageParticle textDamage = damagePool.Get();
        textDamage.transform.position = position + vecRand;
        textDamage.Init(damage, color, isCri);

        // ����Ʈ ��ƼŬ ����
        ParticleSystem particle = particlePool.Get();
        particle.transform.position = position + vecRand;
        ParticleSystem.MainModule main = particle.main;
        main.startColor = color;
        particle.Play();
        
        StartCoroutine(ReleaseDamageParticle(textDamage, particle));
    }

    public IEnumerator ReleaseDamageParticle(DamageParticle textDamage, ParticleSystem particle)
    {
        yield return wait;
        damagePool.Release(textDamage);
        particlePool.Release(particle);
    }
}
