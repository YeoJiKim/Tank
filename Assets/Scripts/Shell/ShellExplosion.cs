using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;   //发生在Player层
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;       //shell的生存时间           
    public float m_ExplosionRadius = 5f;    //shell的爆炸半径          

    private static int score = 0;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {

        //如果发生刚体碰撞（射中敌人）
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        bool shoot = false;
        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
            {
                continue;
            }
            //产生爆炸力
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth>();
            if (!targetHealth)
            {
                continue;
            }
             shoot = true;
            
            targetHealth.TakeDamage(shoot);
            score = score + 20;
        }
        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
        float damage = relativeDistance * m_MaxDamage;

        damage = Mathf.Max(0.0f, damage);

        return damage;

    }

    public int getScore()
    {
        return score;
    }


}