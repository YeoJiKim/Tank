using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public bool m_Dead;
 
    private void OnEnable()
    {        
        m_Dead = false;       
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        gameObject.SetActive(false);
    }
}