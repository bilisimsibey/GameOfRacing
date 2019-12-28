using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    public GameObject pause_Panel;
    
    public void pausebutton()
    {
      
        pause_Panel.SetActive(true);

    }
    public void anamenuButton()
    {
        SceneManager.LoadScene("anamenu");
    }
    public void devamet()
    {
        pause_Panel.SetActive(false);

    }
}
