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

    public Color[] CurrentColors;
    public Material[] Materials;
    public Light Sun;
    public Camera Cam;

    public Text TempText;
    public Text WindText;

    Color lerpedColor;

    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            CurrentColors[i] = SeasonCycle[0].Colors[i];
            Materials[i].SetColor("_Color",SeasonCycle[0].Colors[i]);
        }
        Sun.color = SeasonCycle[0].Colors[0];
        Cam.backgroundColor = SeasonCycle[0].Colors[0];
        StartCoroutine(Cycle());
    }
    

    IEnumerator Cycle()
    {
        
        while (CurrentSeason < 4)
        {
            yield return new WaitForSeconds(SeasonTime);
            for (float t = 0.01f; t < FadeTime; t += 0.1f)
                {
                CurrentTemp = Mathf.Lerp(SeasonCycle[CurrentSeason].TempMultiplier,SeasonCycle[NextSeason].TempMultiplier,t/FadeTime);
                CurrentWind = Mathf.Lerp(SeasonCycle[CurrentSeason].WindMulitiplier, SeasonCycle[NextSeason].WindMulitiplier, t / FadeTime);
                TempText.text = "Температура: " + Mathf.CeilToInt(CurrentTemp).ToString() + "°C";
                WindText.text = "Скорость ветра: " + Mathf.CeilToInt(CurrentWind).ToString() + "м/c";
                Sun.color = Color.Lerp(SeasonCycle[CurrentSeason].Colors[0], SeasonCycle[NextSeason].Colors[0], t / FadeTime);
                Cam.backgroundColor= Color.Lerp(SeasonCycle[CurrentSeason].Colors[0], SeasonCycle[NextSeason].Colors[0], t / FadeTime);

                for (int i = 0; i < 6; i++)
                {
                    Materials[i].SetColor("_Color", Color.Lerp(SeasonCycle[CurrentSeason].Colors[i], SeasonCycle[NextSeason].Colors[i], t / FadeTime));
                }

                yield return null;
                }
            
            if (CurrentSeason < 3) CurrentSeason++;
            else CurrentSeason = 0;
            if (CurrentSeason < 3) NextSeason = CurrentSeason + 1;
            else NextSeason = 0;
        }
    }


}
