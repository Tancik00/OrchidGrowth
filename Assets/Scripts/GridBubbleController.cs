using System;
using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;

public class GridBubbleController : MonoBehaviour
{
   //public Image prizeImage;
   public GameObject bubblesEffect;
   private bool _isCanMoving = true;

   private void Start()
   {
      FlyingPuzzle.Instance.SetBubblePuzzleValues();
      //prizeImage.sprite = FlyingPuzzle.Instance.prizeSprite;

      var bubbleScale = CustomGridLayout.instance.gridBubbleSize;
      transform.localScale = new Vector3(bubbleScale, bubbleScale, bubbleScale);
   }

   /*private void Update()
   {
      StartCoroutine(DestroyBubble());
   }

   private IEnumerator DestroyBubble()
   {
      yield return new WaitForSeconds(10f);
      FlyingPuzzle.Instance.timeInSecForGridBubble = 180f;
      Destroy(gameObject);
   }*/

   public void ButtonClick()
   {
      FlyingPuzzle.Instance.mysteryBubblePrizePanel.SetActive(true);
      FlyingPuzzle.Instance.ShowNoThanksButton();
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
