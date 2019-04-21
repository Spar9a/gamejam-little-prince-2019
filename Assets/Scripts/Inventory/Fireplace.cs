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

    public SeasonsManager Manager;
    public Inventory Inventory_;

    public Renderer Model;
    public ParticleSystem FireParticles;
    public AudioSource Source;
    public AudioClip[] WoodSounds;
    public AudioClip DeathSound;


    void Start()
    {
        InZone = false;
        UsePanel.SetActive(false);
        Model.sharedMaterials[1] = Manager.Materials[5];
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
        Source.PlayOneShot(WoodSounds[Random.Range(0, WoodSounds.Length)]);
        StopAllCoroutines();
        _hp.GetHealth(20);
        StartCoroutine(Burn());
        //if (Hotness > 100) Hotness = 100;

    }
    IEnumerator Burn()
    {
        FireParticles.Play();
        Source.Play();
        Model.sharedMaterials[1] = Manager.Materials[5];
        for (float t = 0.01f; t < LifeTime; t += 0.1f)
        {
            _hp.SetHealth(Mathf.Lerp(_hp.GetMaxHP(),0,t/LifeTime));
            yield return null;
        }
        if (_hp.GetCurrentHP() <= 0)
        {
            End();
        }
    }
    void End()
    {
        Source.PlayOneShot(DeathSound);
        FireParticles.Stop();
        Source.Stop();
        Model.sharedMaterials[1] = Manager.Materials[6];
    }
}
