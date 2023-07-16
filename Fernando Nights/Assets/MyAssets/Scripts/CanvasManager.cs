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
    [SerializeField] private TextTMP guardInfoText;
    public Slider baterySlider;
    public Slider puppetSlider;
    public Slider foxySlider;
    [SerializeField] private TextTMP hourTimeLeft;

    [SerializeField] private float timeLeft;
    public string guardInfoString;

    [SerializeField]
    IntSCOB remnantAmount;
    [SerializeField]
    FloatSCOB bateryAmount;
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
    [SerializeField]
    GameObject loseObj;
    void Start()
    {
        musicBoxObj.SetActive(false);
        foxyPatienceObj.SetActive(false);
        StartCoroutine(LoopUpdateText());
        guardInfoString = "is relaxing";
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
        timeLeft = timeLeft - Time.deltaTime;
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
        baterySlider.value = (int)bateryAmount.Value;
        guardInfoText.UpdateText("Guard " + guardInfoString);
        if (timeLeft >= 420)
        {
            hourTimeLeft.UpdateText("12 pm");
        } else if (timeLeft >= 360)
        {
            hourTimeLeft.UpdateText("1 pm");
        } else if (timeLeft >= 300)
        {
            hourTimeLeft.UpdateText("2 pm");
        } else if (timeLeft >= 240)
        {
            hourTimeLeft.UpdateText("3 pm");
        } else if (timeLeft >= 180)
        {
            hourTimeLeft.UpdateText("4 pm");
        } else if (timeLeft >= 120)
        {
            hourTimeLeft.UpdateText("5 pm");
        } else if (timeLeft >= 0)
        {
            LostGame();
        }
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

    public void LostGame()
    {
        loseObj.SetActive(true);
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
