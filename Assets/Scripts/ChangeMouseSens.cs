using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeMouseSens : MonoBehaviour
{
    public TMP_InputField sensInput;
    public Slider sensSlider;
    public PersistentData settings;

    void Awake()
    {
        settings = PersistentData.Instance;
        sensInput.text = settings.mouseSens.ToString();
        sensSlider.value = settings.mouseSens;
    }

    public void SliderUpdate()
    {
        int newSensInt = (int)sensSlider.value;
        sensInput.text = newSensInt.ToString();
        settings.mouseSens = Mathf.Clamp(newSensInt, 0, 200);
    }

    public void TextUpdate()
    {
        if (sensInput.text != "")
        {
            int newSensInt = int.Parse(sensInput.text);
            sensSlider.value = Mathf.Clamp(newSensInt, 0, 200);
            settings.mouseSens = Mathf.Clamp(newSensInt, 0, 200);
        }
        else 
        { // reset to 100
            sensInput.text = "100";
            TextUpdate();
        }
    }
}
