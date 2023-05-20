using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    public Image loadBar;

    private void Update()
    {
        if (loadBar.fillAmount <= 0.85f)
        {
            loadBar.fillAmount += 0.2f / 100f;
        }
        else if (loadBar.fillAmount <= 0.965f)
        {
            loadBar.fillAmount += 0.08f / 100f;
        }

        if (loadBar.fillAmount >= 0.965)
        {
            gameObject.SetActive(false);
        }
    }
}