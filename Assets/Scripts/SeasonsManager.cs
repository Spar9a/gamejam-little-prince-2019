using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SeasonType
{
    Summer,
    Autumn,
    Winter,
    Spring
}

[System.Serializable]
public class Season
{
    public SeasonType Type;
    public float TempMultiplier;
    public float WindMulitiplier;
    public Color[] Colors;
    public Material ParticleMaterial;
    public GameObject Particles;
}

public class SeasonsManager : MonoBehaviour
{
    public Season[] SeasonCycle;
    public float SeasonTime;

    public float CurrentTemp;
    public float CurrentWind;
    public int NextSeason;
    public int CurrentSeason;
    public float FadeTime;
    public bool SummerPassed;
    public bool Transition;

    public Color[] CurrentColors;
    public Color FadeOutColor;
    public Color FadeInColor;

    public Material[] Materials;
    public Light Sun;
    public Camera Cam;

    public Text TempText;
    public Text WindText;
    public GameObject EndPanel;

    public Fireplace Fire;

    void Start()
    {
        SummerPassed = false;
        Transition = false;

        CurrentTemp = SeasonCycle[0].TempMultiplier+Fire.Hotness/20;
        CurrentWind = SeasonCycle[0].WindMulitiplier;

        TempText.text = "Температура: " + Mathf.CeilToInt(CurrentTemp).ToString() + "°C";
        WindText.text = "Скорость ветра: " + Mathf.CeilToInt(CurrentWind).ToString() + "м/c";

        for (int i = 0; i < 6; i++)
        {
            CurrentColors[i] = SeasonCycle[0].Colors[i];
            Materials[i].SetColor("_Color",SeasonCycle[0].Colors[i]);
        }
        Sun.color = SeasonCycle[0].Colors[0];
        Cam.backgroundColor = SeasonCycle[0].Colors[0];


        StartCoroutine(Cycle());
    }

    private void Update()
    {
        if (!Transition)
        {
            //TempText.text= "Температура: "
        }
    }
    IEnumerator Cycle()
    {
        
        while (CurrentSeason < 4)
        {
            yield return new WaitForSeconds(SeasonTime);

            Transition = true;
            SeasonCycle[NextSeason].Particles.SetActive(true);
            for (float t = 0.01f; t < FadeTime; t += 0.1f)
                {
                CurrentTemp = Mathf.Lerp(SeasonCycle[CurrentSeason].TempMultiplier,SeasonCycle[NextSeason].TempMultiplier,t/FadeTime) + Fire.Hotness/20;
                CurrentWind = Mathf.Lerp(SeasonCycle[CurrentSeason].WindMulitiplier, SeasonCycle[NextSeason].WindMulitiplier, t / FadeTime);
                TempText.text = "Температура: " + Mathf.CeilToInt(CurrentTemp).ToString() + "°C";
                WindText.text = "Скорость ветра: " + Mathf.CeilToInt(CurrentWind).ToString() + "м/c";
                Sun.color = Color.Lerp(SeasonCycle[CurrentSeason].Colors[0], SeasonCycle[NextSeason].Colors[0], t / FadeTime);
                Cam.backgroundColor= Color.Lerp(SeasonCycle[CurrentSeason].Colors[0], SeasonCycle[NextSeason].Colors[0], t / FadeTime);
                FadeOutColor.a = Mathf.Lerp(0, 1, t / FadeTime);
                FadeInColor.a = Mathf.Lerp(1, 0, t / FadeTime);

                SeasonCycle[CurrentSeason].ParticleMaterial.SetColor("MainColor", FadeOutColor);
                SeasonCycle[CurrentSeason].ParticleMaterial.SetColor("MainColor", FadeInColor);

                for (int i = 0; i < 6; i++)
                {
                    Materials[i].SetColor("_Color", Color.Lerp(SeasonCycle[CurrentSeason].Colors[i], SeasonCycle[NextSeason].Colors[i], t / FadeTime));
                }

                
                yield return null;
                }

            SeasonCycle[CurrentSeason].Particles.SetActive(false);
            

                
            Transition = false;
            if (CurrentSeason == 0)
            {
                Debug.Log("Summer Passed");
                if (!SummerPassed) SummerPassed = true;
                else End();
            }

            if (CurrentSeason < 3) CurrentSeason++;
            else CurrentSeason = 0;
            if (CurrentSeason < 3) NextSeason = CurrentSeason + 1;
            else NextSeason = 0;
        }
        End();
    }
    void End()
    {
        EndPanel.SetActive(true);
    }


}
