using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinEquipButton : MonoBehaviour
{
	// Start is called before the first frame update
	public List<Button> buttons = new List<Button>();
	public List<GameObject> skinPrefabs; // 스킨 프리팹 리스트
	public Transform contentTransform; // 스킨 프리팹을 배치할 부모 Transform

	public enum Type
	{
		Skin,
		Effect
	}

	public Type type;

	void Start()
	{ 
		if(type == Type.Skin)
			CollectAllSkinButtons();
		else if(type == Type.Effect)
			CollectAllEffectButtons();
		//ArrangeSkins();
		
		ButtonSetting(type);
	}



	void CollectAllSkinButtons()
	{
		// 현재 게임 오브젝트와 모든 자식 오브젝트에서 버튼 컴포넌트를 찾아서 리스트에 추가
		Button[] foundButtons = GetComponentsInChildren<Button>(true);
		
		for (int i = 0; i < foundButtons.Length; i++)
		{


			int index = i;
				foundButtons[i].onClick.AddListener(() => OnClickSkinEquip(index));
				buttons.Add(foundButtons[i]);
			
			
		}
	}
	void CollectAllEffectButtons()
	{
		// 현재 게임 오브젝트와 모든 자식 오브젝트에서 버튼 컴포넌트를 찾아서 리스트에 추가
		Button[] foundButtons = GetComponentsInChildren<Button>(true);

		for (int i = 0; i < foundButtons.Length; i++)
		{
			int index = i; // 캡처하기 위해 새 변수 생성
			foundButtons[i].onClick.AddListener(() => OnClickEffectEquip(index));
			buttons.Add(foundButtons[i]);
		}
	}
	void ArrangeSkins()
	{
		List<GameObject> ownedHeroSkins = new List<GameObject>();
		List<GameObject> ownedNormalSkins = new List<GameObject>();
		List<GameObject> notOwnedSkins = new List<GameObject>();

		// 스킨을 가진지 여부에 따라 분류
		for (int i = 0; i < skinPrefabs.Count; i++)
		{
			if (DataManager.Instance.havePlayerSkin[i])
			{
				if (IsHeroSkin(i))
				{
					ownedHeroSkins.Add(skinPrefabs[i]);
				}
				else
				{
					ownedNormalSkins.Add(skinPrefabs[i]);
				}
			}
			else
			{
				notOwnedSkins.Add(skinPrefabs[i]);
			}
		}

		// 영웅 등급 스킨을 먼저 배치
		foreach (var skin in ownedHeroSkins)
		{
			InstantiateSkin(skin, true);
		}

		// 일반 등급 스킨을 배치
		foreach (var skin in ownedNormalSkins)
		{
			InstantiateSkin(skin, true);
		}

		// 가지지 않은 스킨을 뒤에 배치하고 회색으로 변경, 버튼 비활성화
		foreach (var skin in notOwnedSkins)
		{
			InstantiateSkin(skin, false);
		}
	}

	bool IsHeroSkin(int index)
	{
		// 영웅 등급 스킨의 인덱스를 0과 1로 가정
		return index == 0 || index == 1;
	}

	void InstantiateSkin(GameObject skinPrefab, bool isOwned)
	{
		GameObject skinInstance = Instantiate(skinPrefab, contentTransform);
		Button button = skinInstance.GetComponentInChildren<Button>();

		if (!isOwned)
		{
			button.interactable = false;
			Image[] images = skinInstance.GetComponentsInChildren<Image>();
			foreach (Image img in images)
			{
				img.color = Color.gray;
			}
		}

		// 버튼 리스트에 추가
		buttons.Add(button);
	}

	public void OnClickSkinEquip(int value)
	{
		
		DataManager.Instance.skinID = value;

		foreach (Button button in buttons)
		{
			button.interactable = true;
		}

		buttons[value].interactable = false;
		DataManager.Instance.SaveDataToJson();
	}

	public void OnClickEffectEquip(int value)
	{

		DataManager.Instance.effectID = value;

		foreach (Button button in buttons)
		{
			button.interactable = true;
		}

		buttons[value].interactable = false;
		DataManager.Instance.SaveDataToJson();
	}

	public void ButtonSetting(Type type)
	{
		if (type == Type.Skin)
		{
			for (int index = 0; index < buttons.Count; index++)
			{
				if (DataManager.Instance.skinID == index)
				{
					buttons[index].interactable = false;

				}

				else
					buttons[index].interactable = true;
			}
		}
		else if(type == Type.Effect)
		{
			{
				for (int index = 0; index < buttons.Count; index++)
				{
					if (DataManager.Instance.effectID == index)
					{
						buttons[index].interactable = false;

					}

					else
						buttons[index].interactable = true;
				}
			}
		}
	}

}