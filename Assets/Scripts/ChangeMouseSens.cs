using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeMouseSens : MonoBehaviour
{
    public TMP_InputField sensInput;
    public Slider sensSlider;
    public int defaultSens;
    public PersistentData settings;
    public GameObject dataPrefab;

    private int maxSens;


    void Awake()
    {
        settings = PersistentData.Instance;
        // if debugging without going through menu, create new instance
        if (settings == null)
        {
            Instantiate(dataPrefab);
            settings = GameObject.FindGameObjectWithTag("Data").GetComponent<PersistentData>();
        }

        sensInput.text = settings.mouseSens.ToString();
        sensSlider.value = settings.mouseSens;
        maxSens = 2 * defaultSens;
    }

    public void SliderUpdate()
    {
        int newSensInt = (int)sensSlider.value;
        sensInput.text = newSensInt.ToString();
        settings.mouseSens = Mathf.Clamp(newSensInt, 0, maxSens);
    }

    public void TextUpdate()
    {
        if (sensInput.text != "")
        {
            int newSensInt = int.Parse(sensInput.text);
            sensSlider.value = Mathf.Clamp(newSensInt, 0, maxSens);
            settings.mouseSens = Mathf.Clamp(newSensInt, 0, maxSens);
        }
        else 
        { // reset to default
            sensInput.text = defaultSens.ToString();
            TextUpdate();
        }
    }
}
