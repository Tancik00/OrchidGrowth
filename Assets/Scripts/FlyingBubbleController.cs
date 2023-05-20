using System;
using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBubbleController : MonoBehaviour
{
   public Image prizeImage;
   public GameObject bubblesEffect;
   public List<Transform> points;
   private bool _isCanMoving = true;
   private RectTransform _rectTransf;

   private int i = 1;

   private void Start()
   {
      points = FlyingPuzzle.Instance.points;
      //FlyingPuzzle.Instance.SetBubblePuzzleValues();
      FlyingPuzzle.Instance.SetFlyingBubbleValues();
      _rectTransf = GetComponent<RectTransform>();
      //prizeImage.sprite = FlyingPuzzle.Instance.prizeSprite;
      prizeImage.sprite = FlyingPuzzle.Instance.flyingPrizeSprite;
   }

   private void Update()
   {
      if (i < points.Count)
      {
         if (_isCanMoving)
         {
            _rectTransf.position =
               Vector3.Lerp(_rectTransf.position, points[i].position, 0.1f * Time.deltaTime);
         }

         if (Vector3.Distance(_rectTransf.position, points[i].position) <= 0.4f)
         {
            i++;
         }

         if (Vector3.Distance(_rectTransf.position, points[points.Count - 1].position) <= 0.4f)
         {
            FlyingPuzzle.Instance.timeInSec = 90f;
            _isCanMoving = false;
            Destroy(gameObject);
         }
      }
   }          

   public void ButtonClick()
   {
      //FlyingPuzzle.Instance.mysteryBubblePrizePanel.SetActive(true);
      FlyingPuzzle.Instance.flyingBubblePrizePanel.SetActive(true);
      //FlyingPuzzle.Instance.ShowNoThanksButton();
      FlyingPuzzle.Instance.ShowFlyingBubblePanelNoThanksButton();
      for (int i = 0; i < transform.childCount; i++)
      {
         transform.GetChild(i).gameObject.SetActive(false);
      }

      Instantiate(bubblesEffect, transform);
      StartCoroutine(DestroyObject());
   }

   private IEnumerator DestroyObject()
   {
      yield return new WaitForSeconds(2f);
      DestroyObject(gameObject);
   }
}