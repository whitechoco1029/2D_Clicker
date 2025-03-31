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
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wait = new WaitForSeconds(lifeTime);

        damagePool = new ObjectPool<DamageParticle>(
                createFunc: () => Instantiate(damagePrefab, transform),
            actionOnGet: (text) => text.gameObject.SetActive(true),
            actionOnRelease: (text) => text.gameObject.SetActive(false),
            actionOnDestroy: (text) => Destroy(text.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
            );

        particlePool = new ObjectPool<ParticleSystem>(
                createFunc: () => Instantiate(particlePrefab, transform),
            actionOnGet: (text) => text.gameObject.SetActive(true),
            actionOnRelease: (text) => text.gameObject.SetActive(false),
            actionOnDestroy: (text) => Destroy(text.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
            );
    }

    public void CreateDamageParticle(Vector3 position, string damage, Color color)
    {
        // 포지션 랜덤위치 생성
        Vector3 vecRand = Random.insideUnitCircle * createRange;

        // 데미지 텍스트 생성
        DamageParticle textDamage = damagePool.Get();
        textDamage.transform.position = position + vecRand;
        textDamage.Init(damage, color);

        // 이펙트 파티클 생성
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
