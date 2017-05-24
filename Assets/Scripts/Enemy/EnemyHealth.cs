using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public GameObject m_ExplosionPrefab;

    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    public bool m_Dead;
    
    private void Awake()
    {
       
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        m_Dead = false;
    }


    public void TakeDamage(bool shoot)
    {
    
        if(shoot && !m_Dead)
        {
            OnDeath();
            
        }   

    }

    private void OnDeath()
    {
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        Destroy(gameObject);
    }
}
