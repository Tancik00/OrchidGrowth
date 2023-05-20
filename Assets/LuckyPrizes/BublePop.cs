using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BublePop : MonoBehaviour
{

    public RectTransform mianBublle;

    public RectTransform[] bublles;

    private Image[] bubleImage ;

    float rectTransformScale = 1.04f;
    float colorTransparacyScale = 0.95f;
    int iterationTimes = 20;

    private void Awake()
    {
        bubleImage = new Image[bublles.Length];
    }

    void Start()
    {
        
        for (int i = 0; i<bublles.Length; i++)
        {
            bubleImage[i] = bublles[i].GetComponent<Image>();
        }
        StartCoroutine(BublleAnimation());
    }

    IEnumerator BublleAnimation()
    {
        Debug.Log("HERE");
        for (int i = 0; i < iterationTimes; i++)
        {
            for (int j = 0; j < bublles.Length; j++)
            {
                bublles[j].anchoredPosition *= rectTransformScale;
                bubleImage[j].color *= new Color(1, 1, 1, colorTransparacyScale);
            }
            mianBublle.localScale *= 1.01f;
            mianBublle.GetComponent<Image>().color *= new Color(1, 1, 1, colorTransparacyScale);
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }

}
