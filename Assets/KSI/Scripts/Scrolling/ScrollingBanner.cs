using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBanner : MonoBehaviour
{
	[SerializeField] private RectTransform textRect; // 텍스트의 RectTransform
	[SerializeField] private RectTransform panelRect; // 패널의 RectTransform
	[SerializeField] private float scrollSpeed = 50f; // 텍스트가 스크롤되는 속도

	private float startPosX;
	private float panelWidth;
	private float textWidth;

	private void Start()
	{
		startPosX = textRect.anchoredPosition.x;
		panelWidth = panelRect.rect.width;
		textWidth = textRect.rect.width;
	}

	private void Update()
	{
		textRect.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

		if (textRect.anchoredPosition.x + textWidth < 0)
		{
			textRect.anchoredPosition = new Vector2(panelWidth, textRect.anchoredPosition.y);
		}
	}
}
