using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public TextTMP remnantText;
    public TextTMP bateryText;
    public TextTMP keysText;
    public Slider baterySlider;
    public Slider puppetSlider;
    public Slider foxySlider;

    [SerializeField]
    IntSCOB remnantAmount;
    [SerializeField]
    FloatSCOB bateryAmount;
    [SerializeField]
    IntSCOB keysAmount;
    [SerializeField]
    FloatSCOB musicBox;
    [SerializeField]
    FloatSCOB foxyPatience;


    [SerializeField]
    GameObject musicBoxObj;
    [SerializeField]
    GameObject foxyPatienceObj;
    [SerializeField]
    GameObject pauseObj;
    [SerializeField]
    GameObject winObj;
    void Start()
    {
        musicBoxObj.SetActive(false);
        foxyPatienceObj.SetActive(false);
        StartCoroutine(LoopUpdateText());
    }

    IEnumerator LoopUpdateText()
    {
        while (true)
        {
            UpdateText();
            yield return new WaitForSeconds(3f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            pauseObj.SetActive(true);
            Time.timeScale = 0;
        }
        puppetSlider.value = musicBox.Value;
        foxySlider.value = foxyPatience.Value;
    }

    public void UpdateText()
    {
        remnantText.UpdateText("Remnant: " + remnantAmount.Value);
        bateryText.UpdateText("Batery: " + ((int)bateryAmount.Value) + "%");
        keysText.UpdateText("Keys: " + keysAmount.Value);
        baterySlider.value = (int)bateryAmount.Value;
    }

    public void ActivateUIFoxy()
    {
        foxyPatienceObj.SetActive(true);
    }
    public void ActivateUIPuppet()
    {
        musicBoxObj.SetActive(true);
    }

    public void WinGame()
    {
        winObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void Button(int ID)
    {
        if (ID == 1)
        {
            pauseObj.SetActive(false);
            Time.timeScale = 1;
        }
        if (ID == 2)
        {
            SceneManager.LoadScene("MenuScene");
            Time.timeScale = 1;
        }
    }
}
