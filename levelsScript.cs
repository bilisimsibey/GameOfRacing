using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levelsScript : MonoBehaviour
{
    public Button[] butonlar;
    
    
    public void levels(string levelid)
    {

        SceneManager.LoadScene(levelid);
    }
    public void homebutton()
    {
        SceneManager.LoadScene("anamenu");
    }
    
    public void kilitsistemi()
    {
       int bolums = PlayerPrefs.GetInt("level");

        for (int i = 0; i < butonlar.Length; i++)
        {
            if (bolums+1>=int.Parse(butonlar[i].name))
            {
                butonlar[i].interactable = true;
            }
            else
            {
                butonlar[i].interactable = false;
            }
        }
    }
}
