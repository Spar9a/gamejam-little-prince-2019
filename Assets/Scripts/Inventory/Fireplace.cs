using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireplace : MonoBehaviour
{
    public float StartHotness;
    public float Hotness;
    public float LifeTime;
    public float FadeTime;
    public Slider StatusBar;

    public bool InZone;
    public GameObject UsePanel;
    public Text UseText;
    public string Line;

    public Inventory Inventory_;
    public SeasonsManager Manager;
    public Material WoodMaterial;

    void Start()
    {
        InZone = false;
        UsePanel.SetActive(false);
        WoodMaterial.SetColor("_Color",Manager.CurrentColors[5]);

        StartCoroutine(Burn());
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"&&Inventory_.CurrentItem==Item.Wood)
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
        StartHotness = Hotness + 20;
        StartCoroutine(Burn());
        //if (Hotness > 100) Hotness = 100;

    }
    IEnumerator Burn()
    {
        
        for (float t = 0.01f; t < LifeTime; t += 0.1f)
        {
            Hotness = Mathf.Lerp(StartHotness,0,t/LifeTime);
            StatusBar.value = Hotness;
            yield return null;
        }
        for (float t = 0.01f; t < FadeTime; t += 0.1f)
        {
            WoodMaterial.SetColor("_Color", Color.Lerp(Manager.CurrentColors[5], Manager.CurrentColors[6], t / FadeTime));
        }

        }
}
