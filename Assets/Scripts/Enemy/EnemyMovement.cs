using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class EnemyMovement : MonoBehaviour
    {
        private GameObject target;
        TankHealth tankHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("tank");
            enemyHealth = GetComponent<EnemyHealth>();
            tankHealth = GetComponent<TankHealth>();
            nav = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!enemyHealth.m_Dead)
            {
                nav.SetDestination(target.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
    }



