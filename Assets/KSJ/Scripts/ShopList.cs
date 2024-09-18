using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
	public static ShopList Instance { get; private set; }
	public int resetCost;

	[Header("Item Prefabs")]
	public List<GameObject> itemPrefabs; // 여러 아이템 프리팹 리스트

	[Header("Spawn Points")]
	public List<Transform> spawnPoints; // 아이템을 표시할 위치 리스트

	private List<GameObject> spawnedItems = new List<GameObject>(); // 생성된 아이템을 추적하기 위한 리스트

	
	private void Update()
	{
		
	}

	public void DisplayRandomItems()
	{
		// 부모 오브젝트의 localScale을 일시적으로 Vector3(1,1,1)로 설정
		Vector3 originalScale = transform.parent.localScale;
		transform.parent.localScale = new Vector3(1, 1, 1);

		// 기존에 생성된 아이템이 있다면 제거
		foreach (var item in spawnedItems)
		{
			Destroy(item);
		}
		spawnedItems.Clear();

		// 아이템 프리팹 중 랜덤하게 2개 선택
		List<int> chosenIndices = DataManager.Instance.resetItemID;

		// 선택한 아이템을 특정 위치에 생성
		for (int i = 0; i < chosenIndices.Count; i++)
		{
			int index = chosenIndices[i];
			GameObject itemPrefab = itemPrefabs[index];

			Transform spawnPoint = spawnPoints[i];
			
			GameObject spawnedItem = Instantiate(itemPrefab, transform);

			// localPosition을 사용하여 정확한 위치 설정
			spawnedItem.transform.localPosition = spawnPoint.localPosition;
			spawnedItem.SetActive(true);
			spawnedItems.Add(spawnedItem);
		}

		// 원래의 localScale로 되돌림
		transform.parent.localScale = originalScale;
	}

	public void OnClickIngameReset(int value)
	{
		if (value==0)
		{
			if (DataManager.Instance.cannedFood - 500 < 0 || DataManager.Instance.resetCannedNum == DataManager.Instance.resetCannedNumMax)
			{
				KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
				return;

				//3번째에도 안되게 해야함
				//시간이 12시일때 
			}
			PickRandomItems(value);
			DataManager.Instance.cannedFood -= 500;
		}
		else if (value == 1)
		{
			if (DataManager.Instance.sushi - resetCost < 0 || DataManager.Instance.resetNum == DataManager.Instance.resetMaxNum)
			{
				KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
				return;

				//3번째에도 안되게 해야함
				//시간이 12시일때 
			}
			PickRandomItems(value);
			DataManager.Instance.sushi -= resetCost;
		}
		
		DisplayRandomItems();
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
	}

	public void PickRandomItems(int value)//데이터매니져에 리셋 아이템 리스트를 랜덤하게 바꿈
	{
		if (DataManager.Instance.resetCannedNum == 3 && value == 0)
			return;
		if (DataManager.Instance.resetNum == 3 && value ==1)
			return;
		DataManager.Instance.resetItemID.Clear();
		
		while (DataManager.Instance.resetItemID.Count!=2)
		{
			int randomIndex = Random.Range(0, itemPrefabs.Count);
			if (!DataManager.Instance.resetItemID.Contains(randomIndex))
			{
				DataManager.Instance.resetItemID.Add(randomIndex);
			}
		}
		if (value == 0)
			DataManager.Instance.resetCannedNum++;
		else
			DataManager.Instance.resetNum++;
		DataManager.Instance.SaveDataToJson();

	}

	public void OnClickContent()
	{
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
		transform.parent.localScale = new Vector3(1, 1, 1);
		DisplayRandomItems();
	}

	
}