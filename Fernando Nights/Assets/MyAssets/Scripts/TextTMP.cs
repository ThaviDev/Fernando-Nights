using UnityEngine;
using TMPro;

public class TextTMP : MonoBehaviour
{
    private TMP_Text myText;
    private void Awake()
    {
        myText = GetComponent<TMP_Text>();
    }
    public void UpdateText(string newText)
    {
        myText.text = newText;
    }
}
