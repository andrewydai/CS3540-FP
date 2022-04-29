using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeMouseSens : MonoBehaviour
{
    public TMP_InputField sensInput;
    public Slider sensSlider;

    private float maxSens;


    void Awake()
    {
        sensInput.text = LevelManager.mouseSens.ToString("F2");
        sensSlider.value = LevelManager.mouseSens;
        maxSens = 10;
    }

    public void SliderUpdate()
    {
        float newSens = sensSlider.value;
        sensInput.text = newSens.ToString("F2");
        LevelManager.mouseSens = Mathf.Clamp(newSens, 1, maxSens);
        SaveSens();
    }

    public void TextUpdate()
    {
        if (sensInput.text != "")
        {
            float newSens = float.Parse(sensInput.text);
            sensSlider.value = Mathf.Clamp(newSens, 1, maxSens);
            LevelManager.mouseSens = Mathf.Clamp(newSens, 1, maxSens);
            SaveSens();
        }
        else 
        {   // reset to default
            sensInput.text = "5.00";
            TextUpdate();
        }
    }

    void SaveSens()
    {
        PlayerPrefs.SetFloat("mouseSens", LevelManager.mouseSens);
        PlayerPrefs.Save();
    }
}
