using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
	public Image background; // 배경 이미지
	public Button[] buttons; // 배경을 선택하는 버튼 배열
	private int selectedButtonIndex = -1; // 현재 선택된 버튼의 인덱스

	private void Start()
	{
		LoadState(); // 이전에 저장된 상태를 불러오기

		for (int i = 0; i < buttons.Length; i++)
		{
			int index = i; // 루프 변수 캡처
			buttons[i].onClick.AddListener(() => OnButtonClick(index)); // 각 버튼에 클릭 이벤트 연결
		}
	}

	private void OnButtonClick(int index)
	{
		if (selectedButtonIndex != index) // 버튼이 이미 선택된 상태가 아닌 경우
		{
			// 모든 버튼을 다시 활성화 상태로 설정
			foreach (var button in buttons)
			{
				button.interactable = true;
			}

			// 클릭된 버튼을 비활성화 상태(회색)로 설정
			buttons[index].interactable = false;

			// 해당 버튼의 이름으로 프리팹을 로드하여 배경 이미지 설정
			string prefabPath = "Wallpapers/" + buttons[index].name; // 버튼 이름이 프리팹 이름과 일치한다고 가정
			GameObject loadedPrefab = Resources.Load<GameObject>(prefabPath);
			if (loadedPrefab != null)
			{
				Image loadedImage = loadedPrefab.GetComponent<Image>();
				if (loadedImage != null)
				{
					background.sprite = loadedImage.sprite; // 배경 이미지 변경
				}
				else
				{
					Debug.LogWarning($"프리팹 '{prefabPath}'에 Image 컴포넌트가 없습니다.");
				}
			}
			else
			{
				Debug.LogWarning($"프리팹 경로: {prefabPath} 로드 실패");
			}

			selectedButtonIndex = index; // 선택된 버튼 인덱스 업데이트
			SaveState(); // 상태 저장
		}
	}

	private void SaveState()
	{
		PlayerPrefs.SetInt(gameObject.name + "_SelectedButtonIndex", selectedButtonIndex); // 선택된 버튼 인덱스 저장

		// 선택된 버튼의 이름을 저장
		if (selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
		{
			string buttonName = buttons[selectedButtonIndex].name;
			PlayerPrefs.SetString(gameObject.name + "_BackgroundButtonName", buttonName);
		}

		PlayerPrefs.Save(); // PlayerPrefs 저장
	}

	private void LoadState()
	{
		selectedButtonIndex = PlayerPrefs.GetInt(gameObject.name + "_SelectedButtonIndex", -1); // 저장된 버튼 인덱스 불러오기

		// 마지막으로 선택된 버튼 이름으로 프리팹 로드
		string buttonName = PlayerPrefs.GetString(gameObject.name + "_BackgroundButtonName", "");
		if (!string.IsNullOrEmpty(buttonName))
		{
			string prefabPath = "Wallpapers/" + buttonName;
			GameObject loadedPrefab = Resources.Load<GameObject>(prefabPath);
			if (loadedPrefab != null)
			{
				Image loadedImage = loadedPrefab.GetComponent<Image>();
				if (loadedImage != null)
				{
					background.sprite = loadedImage.sprite; // 배경 이미지 설정
				}
				else
				{
					Debug.LogWarning($"프리팹 '{prefabPath}'에 Image 컴포넌트가 없습니다.");
				}
			}
			else
			{
				Debug.LogWarning($"프리팹 경로: {prefabPath} 로드 실패");
			}
		}

		// 저장된 상태에 따라 버튼의 상호작용 설정
		if (selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
		{
			buttons[selectedButtonIndex].interactable = false; // 선택된 버튼을 비활성화 상태로 설정
		}
	}
}