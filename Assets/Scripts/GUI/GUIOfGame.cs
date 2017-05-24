using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIOfGame : MonoBehaviour
{

    public ShellExplosion shellExplosion;
  
    public  GameManager m_GameManager;
    private string m_StrTimeLeft = "";
    private string m_StrScore = "";
    private string m_StrTarget = "";
    
    void OnGUI()
    {
        m_StrTimeLeft = "Time：" + m_GameManager.m_TimeLeft.ToString();
        m_StrScore = "Score：" + shellExplosion.getScore().ToString();
       
        if(m_GameManager.m_Round == 1)
        {
            m_StrTarget = "Target:100";
        }
        else if (m_GameManager.m_Round == 2)
        {
            m_StrTarget = "Target:240";
        }
        else if (m_GameManager.m_Round == 3)
        {
            m_StrTarget = "Target:400";
        }

        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        style.normal.textColor = new Color(0, 0, 0);

        GUI.Label(new Rect(10, 10, Screen.width / 6, Screen.height / 6), m_StrTimeLeft, style);
        GUI.Label(new Rect(Screen.width - 120, 10, Screen.width / 6, Screen.height / 6), m_StrScore, style);
        GUI.Label(new Rect(Screen.width/2 , 10, Screen.width / 6, Screen.height / 6), m_StrTarget, style);


    }

}

