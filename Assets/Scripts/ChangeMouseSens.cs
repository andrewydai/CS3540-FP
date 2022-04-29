using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeMouseSens : MonoBehaviour
{
    public TMP_InputField sensInput;
    public Slider sensSlider;
    public float defaultSens = 5f;

    private float mouseSens;
    private float maxSens;


    void Awake()
    {
        mouseSens = PlayerPrefs.GetFloat("mouseSens", defaultSens);
        sensInput.text = mouseSens.ToString("F2");
        sensSlider.value = mouseSens;
        maxSens = 2 * defaultSens;
    }

    public void SliderUpdate()
    {
        float newSens = sensSlider.value;
        sensInput.text = newSens.ToString("F2");
        mouseSens = Mathf.Clamp(newSens, 0, maxSens);
        SaveSens();
    }

    public void TextUpdate()
    {
        if (sensInput.text != "")
        {
            float newSens = float.Parse(sensInput.text);
            sensSlider.value = Mathf.Clamp(newSens, 0, maxSens);
            mouseSens = Mathf.Clamp(newSens, 0, maxSens);
            SaveSens();
        }
        else 
        {   // reset to default
            sensInput.text = defaultSens.ToString();
            TextUpdate();
        }
    }

    void SaveSens()
    {
        PlayerPrefs.SetFloat("mouseSens", mouseSens);
        PlayerPrefs.Save();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateMouseSens();
    }
}
