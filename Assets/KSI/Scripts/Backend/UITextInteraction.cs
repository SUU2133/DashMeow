using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UITextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[System.Serializable]
	private class OnClickEvent : UnityEvent { }

	// Text UI를 클릭했을 때 호출하고 싶은 메소드 등록
	[SerializeField] private OnClickEvent onClickEvent;

	// 색상이 바뀌고, 터치가 되는 TextMeshProGUI
	private TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		text.fontStyle = FontStyles.Bold;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		text.fontStyle = FontStyles.Normal;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		onClickEvent?.Invoke();
	}
}
