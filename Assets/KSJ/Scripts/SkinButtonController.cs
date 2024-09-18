using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SkinButtonController : MonoBehaviour
{
	public Button skinOpen;
	
	private Scrollbar scrollbar;


	void Start()
	{
		scrollbar = GetComponentInChildren<Scrollbar>();
	}
	public void OnClickSkin()
	{
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
		transform.localScale = new Vector3(1f, 1f, 1f);
		//인벤토리 열고 닫을 때 스크롤이 가장 밑에 있는 문제 해결
		StartCoroutine(SetScrollbarValue());
	}

	public void OnClickDesire()
	{
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
		transform.localScale = new Vector3(0f, 0f, 0f);
	}
	private IEnumerator SetScrollbarValue()
	{
		// 1프레임 대기
		yield return null;

		// 1프레임 후에 scrollbar.value를 1로 설정
		scrollbar.value = 1;
	}

}
