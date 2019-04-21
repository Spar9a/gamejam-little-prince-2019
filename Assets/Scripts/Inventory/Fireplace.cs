using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireplace : MonoBehaviour
{
    public float LifeTime;

    [SerializeField] public HealthBar _hp;
    [SerializeField] bool InZone;
    public GameObject UsePanel;
    public Text UseText;
    public string Line;
    public float Temp = 20f;
    
    public Inventory Inventory_;

    void Start()
    {
        InZone = false;
        UsePanel.SetActive(false);
        StartCoroutine(Burn());
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && Inventory_.CurrentItem==Item.Wood)
        {
            InZone = true;
            UseText.text = Line;
            UsePanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Inventory_.RemoveItem(Item.Wood);
                UsePanel.SetActive(false);
                AddWood();
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            InZone = false;
            UsePanel.SetActive(false);
        }
    }

    void AddWood()
    {
        Debug.Log("added");
        StopAllCoroutines();
        _hp.GetHealth(20);
        StartCoroutine(Burn());
        //if (Hotness > 100) Hotness = 100;

    }

    public float GetCurrentTemp()
    {
        if (_hp.GetCurrentHP() <= 0) return Temp;
        return 0;
    }            
    
    IEnumerator Burn()
    {

        for (float t = 0.01f; t < LifeTime; t += 0.1f)
        {
            _hp.SetHealth(Mathf.Lerp(_hp.GetMaxHP(),0,t/LifeTime));
            yield return null;
        }
    }
}
