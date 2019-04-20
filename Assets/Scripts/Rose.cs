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

    public SeasonsManager Manager;
    public Fireplace Fire;

    public Slider HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Live());
    }

    // Update is called once per frame
    void Update()
    {
        RoseTemp = Manager.CurrentTemp + Fire._hp.GetCurrentHP() / 20;

    }
    IEnumerator Live()
    {
        while (CurrentHealth>0)
        {
            Debug.Log("in");
            
            //yield return new WaitUntil(() => (RoseTemp < -10)||Manager.CurrentWind>10);
            yield return new WaitForSeconds(5);
            if (RoseTemp < -10)
                CurrentHealth = CurrentHealth-TempDamage;
            if (Manager.CurrentWind > 10)
                CurrentHealth =CurrentHealth - WindDamage;
            HealthBar.value = CurrentHealth;
        }
    }
}
