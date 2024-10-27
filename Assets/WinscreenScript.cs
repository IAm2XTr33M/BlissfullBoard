using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinscreenScript : MonoBehaviour
{
    public static WinscreenScript instance;

    [SerializeField] TextMeshProUGUI congratsText;
    [SerializeField] TextMeshProUGUI smallText;
    [SerializeField] TextMeshProUGUI restartText;
    public TextMeshProUGUI timeText;
    [SerializeField] RawImage backgroundImg;
    [SerializeField] RawImage goldenImg;
    [SerializeField] Button button;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        congratsText.color = new Color(1, 1, 1, 0);
        smallText.color = new Color(1, 1, 1, 0);
        timeText.color = new Color(1, 1, 1, 0);
        timeText.color = new Color(1, 1, 1, 0);
        backgroundImg.color = new Color(1, 1, 1, 0);
        goldenImg.color = new Color(1, 0.7679746f, 0.2877358f, 0);//0.65
        button.gameObject.SetActive(false);
    }


    public void Win()
    {

        StartCoroutine(fadeText(congratsText, 2, 1));
        StartCoroutine(fadeText(smallText, 3, 1));
        StartCoroutine(fadeText(timeText, 2, 2));
        StartCoroutine(fadeText(restartText, 3, 2));

        StartCoroutine(fadeImg(backgroundImg, 3, 0.5f, 1));
        StartCoroutine(fadeImg(goldenImg, 1, 0, 0.65f));
        button.gameObject.SetActive(true);
    }

    IEnumerator fadeText(TextMeshProUGUI text, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(time / 20);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 0.05f);
        }
    }
    IEnumerator fadeImg(RawImage img, float time, float delay,float max)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(time / 20);
            if(img.color.a < max)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);
            }
        }
    }
}
