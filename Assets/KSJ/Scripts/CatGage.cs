using UnityEngine;
using UnityEngine.UI;

public class CatGage : MonoBehaviour
{
	public Slider catGageSlider; // ScrollRect 컴포넌트 참조
	

	private void Start()
	{
		InvokeRepeating("UpdateScrollPosition", 0f, 1f);
	}

	// catGage 값을 변경하면 스크롤 위치를 업데이트
	

	// 스크롤 위치 업데이트
	private void UpdateScrollPosition()
	{
		float sliderValue = DataManager.Instance.catGage / 7f; // 0~1 사이의 값을 계산
		catGageSlider.value = sliderValue; // 스크롤 위치 설정 (1이 맨 위, 0이 맨 아래)
	}
}