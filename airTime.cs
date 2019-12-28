using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class airTime : MonoBehaviour
{
    float sayac = 0, combo = 0;
    public bool on_air = false;
    public GameObject araba_;
    public Text airText;
    public int airpoint;
    private void Update()
    {
        
        if (sayac>1f)
        {
            on_air = true;
            combo++;
            sayac = 0;
            Debug.Log("Havada"+ combo);
            airText.text = "Air: +"+airpoint*combo;
            
            araba_.GetComponent<carScript>().kaydet_altin += airpoint;
            if (araba_.GetComponent<carScript>().ivme==0)
            {
                airText.text = "";
            }
        }
        sayac += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player" || collision.gameObject.tag=="teker")
        {
            combo = 0;
            sayac = 0;
            on_air = false;
            airText.text = "";
        }
        if (collision.collider.tag=="Head") //head tagını algılamasında hata olmaması için collider.tag kullandım   
        {

            araba_.GetComponent<carScript>().hayattami = false;
            araba_.GetComponent<carScript>().Resim_Cek();
            
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player" || collision.gameObject.tag=="teker")
        {
            combo = 0;
            sayac = 0;
            on_air = false;
            airText.text = "";
        }
    }
}
