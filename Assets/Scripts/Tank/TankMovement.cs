using UnityEngine;

public class TankMovement : MonoBehaviour
{
   // public int m_PlayerNumber = 1;
    public float m_Speed = 12f;                 // 移动速度
    public float m_TurnSpeed = 180f;            // 转弯速度
    public AudioSource m_MovementAudio;         // 播放坦克引擎声音的AudioSource
    public AudioClip m_EngineIdling;            // 坦克静止时播放的AudioClip
    public AudioClip m_EngineDriving;           // 坦克运动时播放的AudioClip
    public float m_PitchRange = 0.2f;           // 引擎声音播放速度.

    private string m_MovementAxisName;          // 前后移动轴的名字
    private string m_TurnAxisName;              // 转弯轴的名字
    private Rigidbody m_Rigidbody;              // 用来移动tank
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;              // 场景开始时的AudioSource
    private ParticleSystem[] m_particleSystems; // 用来储存tank产生的所有粒子系统

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;

        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Play();
        }
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;

        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Stop();
        }
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal" ;

        m_OriginalPitch = m_MovementAudio.pitch;
    }
   

    private void Update()
    {
       //获取用户的移动方向的按键输入

        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();

    }


    private void EngineAudio()
    {
        // tank静止播放EngineIdling
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
           
            if (m_MovementAudio.clip == m_EngineDriving)
            {
              
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            //tank运动播放EngineDriving
            if (m_MovementAudio.clip == m_EngineIdling)
            {
               
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }

    }


    private void FixedUpdate()
    {
        //移动并旋转tank
        Move();
        Turn();
    }


    private void Move()
    {
       

        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

       
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

     
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

       
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}