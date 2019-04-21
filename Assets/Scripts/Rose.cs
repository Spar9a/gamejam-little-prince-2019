using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rose : MonoBehaviour
{
    public float StartHealth;
    public float CurrentHealth;

    public float TempDamage;
    public float WindDamage;

    public float RoseTemp;
    public float RoseWind;

    public SeasonsManager Manager;
    public Fireplace Fire;
    public Protector Protect;

    public HealthBar _hp;

    public Text RoseTempText;
    public Text RoseWindText;

    public AudioSource Source;
    public AudioClip HitSound;
    public Animator Anim;

    public GameObject LoseScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Live());
    }

    // Update is called once per frame
    void Update()
    {
        RoseTemp = Manager.CurrentTemp + Fire._hp.GetCurrentHP() / 20;
        if (Protect.Activated)
        {
            RoseWind = Manager.CurrentWind - Protect.WindProtect;
            if (RoseWind < 0) RoseWind = 0;
        }
        else RoseWind = Manager.CurrentWind;
        RoseTempText.text = "Температура Розы: " + Mathf.CeilToInt(RoseTemp).ToString() + "°C";
        RoseWindText.text = "Влияние ветра на Розу: " + Mathf.CeilToInt(RoseWind).ToString() + "м/c";


    }
    IEnumerator Live()
    {
        while (CurrentHealth>0)
        {
            //yield return new WaitUntil(() => (RoseTemp < -10)||Manager.CurrentWind>10);
            yield return new WaitForSeconds(5);
            
            if (RoseTemp < -10)
            {
                Source.PlayOneShot(HitSound);
                Anim.SetBool("Hit", true);
                yield return new WaitForSeconds(0.01f);
                Anim.SetBool("Hit", false);
                CurrentHealth = CurrentHealth - TempDamage;
            }
                
            if (Manager.CurrentWind > 10)
            {
                Source.PlayOneShot(HitSound);
                Anim.SetBool("Hit", true);
                yield return new WaitForSeconds(0.01f);
                Anim.SetBool("Hit", false);
                CurrentHealth = CurrentHealth - WindDamage;
            }
            
            _hp.SetHealth(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                LoseScreen.SetActive(true);
            }
        }
        //End;
    }
}
