using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protector : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Activated;
    public bool Culled;
    public float WindProtect;
    public float LifeTime;
    public float LastHp;
    public HealthBar _hp;


    public string[] Lines;
    public Text UseText;
    public GameObject UsePanel;

    public AudioClip UseSound;
    public AudioClip UnUseSound;
    public AudioClip BreakSound;
    public AudioSource Source;

    public Transform Model;
    public Transform[] Positions;

    void Start()
    {
        Culled = false;
        LastHp = _hp.GetMaxHP();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"&&!Culled)
        {
            if (!Activated) UseText.text = Lines[0];
            else UseText.text = Lines[1];
            UsePanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!Activated) UseText.text = Lines[1];
                else UseText.text = Lines[0];
                Use();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            UsePanel.SetActive(false);
        }
    }

    void Use()
    {
        if (!Activated)
        {
            Activated = true;
            Model.position = Positions[1].position;
            Source.PlayOneShot(UseSound);
            StopAllCoroutines();
            StartCoroutine(Protecting());
        }
        else
        {
            Activated = false;
            Model.position = Positions[0].position;
            Source.PlayOneShot(UnUseSound);
            LastHp = _hp.GetCurrentHP();
            StopAllCoroutines();
            StartCoroutine(CullDown(false));
        }
    }
    IEnumerator Protecting()
    {
        for (float t = 0.01f; t < LifeTime; t += 0.1f)
        {
            _hp.SetHealth(Mathf.Lerp(LastHp, 0, t / LifeTime));
            yield return null;
        }
        LastHp = 0;
        StartCoroutine(CullDown(true));
    }
    IEnumerator CullDown(bool Forced)
    {
        Activated = false;
        if(Forced)Culled = true;
        Source.PlayOneShot(BreakSound);
        Model.position = Positions[0].position;

        for (float t = 0.01f; t < LifeTime; t += 0.1f)
        {
            _hp.SetHealth(Mathf.Lerp(LastHp, _hp.GetMaxHP(), t / LifeTime));
            yield return null;
        }
        if(Forced)Culled = false;
        LastHp = _hp.GetMaxHP();
    }
}
