using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class carScript : MonoBehaviour
{
    public WheelJoint2D arka_teker,on_teker; //ön-arka teker ataması için public türünde,WheelJoint cinsinden değişkenler tanımlandı (erişim için)
    public float hiz; //public türünde float cinsinden hiz tanımlandı, arabanın hiz kontrolü için
    private float hareket; //float türünden hareket tanımlandı, hiz ve input girişini tutması için
    private float rotilk, mevrot, sonrot; //float türünden ilk mevcut son rotasyon değişkenleri tanımlandı
    private float rotilk_t, mevrot_t, sonrot_t; //float türünden ters ilk mevcut son rotasyon değişkenleri tanımlandı
    public int top_altin; //int türünden toplam altin değişkeni
    public Text altin_text; //text türünden altıntext değişkeni
    public Image gass_ımage;
    public float gass_speed = 2f;
    public float top_gass = 142;
    public Text fliptext;
    public int flipPoint;
    public float sayac = 1f;
    public GameObject panel;
    public Image gosterilen_resim;
    public Sprite spritemiz;
    public Texture2D texture_resim;
    public bool hayattami = true;
    float mevcut_gass;
    public Text flip_text, backflip_text, gold_miktari;
    private int flipsayisi=0,backflipsayisi=0;
    bool gaz,fren;
    public float ivme;
    public int kaydet_altin;
    public AudioClip goldsound;
    private void Start()
    {
        mevrot = transform.rotation.eulerAngles.z; //z açısını eulerangles ile mevcut rotasyon yapıyoruz
        rotilk = mevrot; //ilk rotasyon eşittir mevcut rotasyona
        sonrot = mevrot; //son rotasyon eşittir mevcut rotasyona
        mevrot_t = transform.rotation.eulerAngles.z;//z açısını eulerangles ile mevcut rotasyon yapıyoruz
        rotilk_t = mevrot_t; //ilk ters rotasyon eşittir mevcut ters rotasyona
        sonrot_t = mevrot_t; //son ters rotasyon eşittir mevcut ters rotasyona

        ivme = 0;
        kaydet_altin = PlayerPrefs.GetInt("altin");
    }
    private void Update()
    {
        if (fliptext.text!="")
        {
            sayac -= Time.deltaTime;

        }
        if (sayac<=0)
        {
            fliptext.text = "";
            sayac = 1f;
        }
        if (!hayattami)
        {
            return;
        }


        /*float yatay=Input.GetAxis("Horizontal"); //input girişi sağlandı
        hareket = hiz * yatay; //hareket değeri atandı*/

        if (gaz)
        {
            ivme+=0.009f;
            if (ivme>1f)
            {
                ivme = 1f;
            }
        }
        if (fren)
        {
            ivme -= 0.009f;
            if (ivme<-1f)
            {
                ivme = -1f;
            }
        }
        if (gaz==false && fren==false)
        {
            ivme = 0;
        }
        hareket = hiz * ivme;
        RectTransform asd = gass_ımage.GetComponent<RectTransform>();
        mevcut_gass = asd.sizeDelta.x - Time.deltaTime * gass_speed;
        asd.sizeDelta = new Vector2(mevcut_gass, asd.sizeDelta.y);
        altin_text.text = kaydet_altin.ToString();

        if (mevcut_gass<=0)
        {
            hayattami = false;
            Resim_Cek();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
    }

    private void FixedUpdate()
    {
        if (hareket==0) //hareket sıfıra eşit ise
        {
            arka_teker.useMotor = false; //arka tekerin motorunu kapat
            on_teker.useMotor = false; //ön tekerin motorunu kapat
        }
        else
        {
            arka_teker.useMotor = true; //arka tekerin motorunu aç
            on_teker.useMotor = true; //ön tekerin motorunu aç
            JointMotor2D motore = new JointMotor2D(); //motor cinsinden değişken tanımlandı
            motore.motorSpeed = hareket; //değişkenin hizi hareket kadar olacak
            motore.maxMotorTorque = 10000; //değişkenin maximum motor hizi 10000 olacak

            arka_teker.motor = motore; //arka tekerin motorunu aktifleştir
            on_teker.motor = motore; //ön tekerin motorunu aktifleştir
        }
        Flip(); //takla metodu çalışsın
        tersFlip(); //ters takla metodu çalışsın
    }

    void Flip()
    {
        mevrot = transform.rotation.eulerAngles.z;  //z açısını eulerangles ile düzenliyoruz
        if (sonrot<mevrot) //son rotasyon küçük ise mevcut rotasyondan
        {
            rotilk = mevrot; //ilk rotasyon eşittir mevcut rotasyona
        }
        else if (sonrot>mevrot && sonrot-mevrot>100) //son rotasyon büyük ise mevcuttan ve son rotasyonla mevcut rotasyonun çıkarımı büyük ise 100 den
        {
            rotilk = mevrot; //ozaman ilk rotasyon eşittir mevcuta
        }
        if (rotilk-mevrot>300) //ilk rotasyon eksi mevcut rotasyon sonucu büyük ise 300 den
        {
            rotilk = mevrot; //ilk rotasyonu mevcuta eşitle
            fliptext.text = "Flip: +"+flipPoint;
            kaydet_altin += flipPoint;
            flipsayisi++;
        }
        sonrot = mevrot; //son rotasyon eşittir mevcuta


    }
    void tersFlip()
    {
        mevrot_t = transform.rotation.eulerAngles.z;  //z açısını eulerangles ile düzenliyoruz
        if (sonrot_t > mevrot_t) //son rotasyon küçük ise mevcut rotasyondan
        {
            rotilk_t = mevrot_t; //ilk rotasyon eşittir mevcut rotasyona
        }
        else if (sonrot_t < mevrot_t && mevrot_t - sonrot_t > 100) //son rotasyon büyük ise mevcuttan ve son rotasyonla mevcut rotasyonun çıkarımı büyük ise 100 den
        {
            rotilk_t = mevrot_t; //ozaman ilk rotasyon eşittir mevcuta
        }
        if (mevrot_t - rotilk_t > 300) //ilk rotasyon eksi mevcut rotasyon sonucu büyük ise 300 den
        {
            rotilk_t = mevrot_t; //ilk ters rotasyon eşittir mevcut ters rotasyona
            fliptext.text = "BackFlip: +" + flipPoint;
            kaydet_altin += flipPoint;
            backflipsayisi++;
        }
        sonrot_t = mevrot_t; //son rotasyon eşittir mevcuta

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Gold") //objeye değen diğer objenin tag ı gold ise
        {
            GetComponent<AudioSource>().PlayOneShot(goldsound);
            int miktar = collision.GetComponent<goldScript>().miktar; //goldscriptteki miktar değerini çekiyoruz
            kaydet_altin += miktar; //toplam altın miktara göre artacak
            GameObject.Destroy(collision.gameObject); //değen objeyi yok et
            altin_text.text = top_altin.ToString(); //altın textine toplam altını string türünden yazdır
        }
        if (collision.gameObject.tag=="Fuel")
        {
            RectTransform asd = gass_ımage.GetComponent<RectTransform>();
            asd.sizeDelta = new Vector2(top_gass, asd.sizeDelta.y);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag=="Finish")
        {
            PlayerPrefs.SetInt("level", int.Parse(SceneManager.GetActiveScene().name));
            SceneManager.LoadScene("2");
            
        }
        if (collision.gameObject.tag == "Finish2")
        {
            
            SceneManager.LoadScene("levels");

        }
    }
    public void Resim_Cek()
    {

        if (hayattami==false)
        {
            kaydet_altin += top_altin;
            PlayerPrefs.SetInt("altin", kaydet_altin);
            Texture2D text = new Texture2D(Screen.width / 2, Screen.height / 2, TextureFormat.RGB24, false);
            texture_resim = new Texture2D(Screen.width / 2, Screen.height / 2);
            text.ReadPixels(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2), 0, 0);
            text.Apply();
            texture_resim = text;
            text.Compress(false);
            spritemiz = Sprite.Create(texture_resim, new Rect(0, 0, texture_resim.width, texture_resim.height), new Vector2(0, 0));
            
            panel.SetActive(true);
            gosterilen_resim.sprite = spritemiz;
            backflip_text.text = "BackFlip: " + backflipsayisi;
            flip_text.text = "Flip: " + flipsayisi;
            gold_miktari.text = "Gold: " + kaydet_altin;
            return;
        }
       


        
    }
    public void tryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    
    
    public void gaz_down()
    {
        gaz = true;
    }
    public void gaz_up()
    {
        gaz = false;
    }
    public void fren_down()
    {
        fren = true;
    }
    public void fren_up()
    {
        fren = false;
    }
}
