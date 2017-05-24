using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public Text m_MessageText;


    public GameObject m_EnemyPrefab;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
 
    private GameObject m_Tank ;
    private Vector3 m_TankOriPos;
    private Quaternion m_TankOriRot;
    private GameObject[] m_Enemies = new GameObject[10];


    private TankMovement m_TankMove;
    private TankShooting m_TankShoot;

    public float m_TimeLeft = 30.0F;
    public  int m_Round = 0;
 
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;
    public ShellExplosion shellExplosion;

 
    private bool m_GameOver = false;
    private bool m_GameWin = false;
    private bool m_RoundWin = false;

    private void Start()
    {
        m_Round = 0;

        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);


        ///Find Tank
        m_Tank = GameObject.FindGameObjectWithTag("tank");
        m_TankOriPos = m_Tank.transform.position;
        m_TankOriRot = m_Tank.transform.rotation;
        m_TankMove = m_Tank.GetComponent<TankMovement>();
        m_TankShoot = m_Tank.GetComponent<TankShooting>();

        StartCoroutine(GameLoop());
        
    }

    void Update()
    {
        if (m_TimeLeft > 0)
        {
            m_TimeLeft -= Time.deltaTime;
        }
        else
        {
            m_TimeLeft = 0;
        }
    }

    private void SpawnEnemies()
    {
        
        for (int i = 0; i < m_Enemies.Length; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            m_Enemies[i] = Instantiate(m_EnemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        }
    }
    

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GameOver())
        {
            //SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }
    private bool GameOver()
    {
        if(m_RoundWin && m_Round == 3)
        {
            m_GameWin = true;
            m_GameOver = true;
        }
        if (!m_RoundWin)
        {
            m_GameWin = false;
            m_GameOver = true;
        }
       
        return m_GameOver;
    }

    private IEnumerator RoundStarting()
    {
        ResetTankAndEnemies();
        DisableTankControl();

        m_TimeLeft = 30;
        m_Round++;
        m_MessageText.text = "ROUND" + m_Round;
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();

        m_MessageText.text = "";

        while (m_TimeLeft > 0)
        {

            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();
        string message = "";
        switch (m_Round)
        {
            case 1:
                if (shellExplosion.getScore() >= 100)
                {
                    message = "Next Round!";
                    m_RoundWin = true;
                }                
                else
                {
                    message = "You Lose!";
                    m_RoundWin = false;
                }                 
                break;
            case 2:
                if (shellExplosion.getScore() >= 240)
                {
                    message = "Next Round!";
                    m_RoundWin = true;
                }
                else
                {
                    message = "You Lose!";
                    m_RoundWin = false;
                }

                break;
            case 3:
                if (shellExplosion.getScore() >= 400)
                {
                    message = "You Win!";
                    m_RoundWin = true;
                }
                else
                {
                    message = "You Lose!";
                    m_RoundWin = false;
                }

                break;
        }
        m_MessageText.text = message;
        Debug.Log(m_Round);
      
        yield return m_EndWait;
    }


    
    private void ResetTankAndEnemies()
    {
        m_Tank.transform.position = m_TankOriPos;
        m_Tank.transform.rotation = m_TankOriRot;
        m_Tank.SetActive(false);
        m_Tank.SetActive(true);


        for (int i = 0; i < m_Enemies.Length; i++)
        {
            if(m_Enemies[i] != null)
                Destroy(m_Enemies[i]);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            m_Enemies[i] = Instantiate(m_EnemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
         
        }
    }


    private void EnableTankControl()
    {

        m_TankMove.enabled = true;
        m_TankShoot.enabled = true;
  

    }


    private void DisableTankControl()
    {
        m_TankMove.enabled = false;
        m_TankShoot.enabled = false;

    }
}
