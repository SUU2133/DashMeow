using System.Collections.Generic;
using UnityEngine;

public class EffectPoolManager : MonoBehaviour
{
	public static EffectPoolManager Instance { get; private set; }

	public GameObject[] jumpEffects;  // 프리팹을 보관하는 변수
	public GameObject[] glideEffects;
	public GameObject[] destroyObjectEffects;
	public GameObject[] boosterEffects;
	public GameObject[] shieldEffects;
	public GameObject[] healigEffects;
	public GameObject[] invincibleEffects;
	public GameObject[] fireflyEffects;

	private Dictionary<string, List<GameObject>> pools; // 풀 담당을 하는 딕셔너리

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // 씬 전환 시 객체 유지
		}
		else
		{
			Destroy(gameObject); // 중복된 인스턴스 제거
			return;
		}

		// Initialize pools dictionary
		pools = new Dictionary<string, List<GameObject>>();

		// Create pools for each type
		InitializePools(jumpEffects);
		InitializePools(glideEffects);
		InitializePools(destroyObjectEffects);
		InitializePools(boosterEffects);
		InitializePools(shieldEffects);
		InitializePools(healigEffects);
		InitializePools(invincibleEffects);
		InitializePools(fireflyEffects);
	}

	private void InitializePools(GameObject[] effects)
	{
		foreach (GameObject effect in effects)
		{
			string effectName = effect.name;
			if (!pools.ContainsKey(effectName))
			{
				pools[effectName] = new List<GameObject>();
			}
		}
	}

	public GameObject Get(int index, int type) // 비어있는 오브젝트를 반환하는 함수
	{
		GameObject select = null;
		string effectName = GetEffectName(index, type);

		// 해당 풀의 리스트를 탐색하여 비활성화된 오브젝트를 찾기
		if (pools.ContainsKey(effectName))
		{
			foreach (GameObject item in pools[effectName])
			{
				if (!item.activeSelf)
				{
					select = item;
					select.SetActive(true); // 활성화
					return select;
				}
			}
		}

		// 모두 사용 중이면 새로운 오브젝트를 인스턴스화
		if (select == null)
		{
			GameObject prefab = GetPrefab(index, type);
			if (prefab != null)
			{
				select = Instantiate(prefab, transform); // 오브젝트를 복사하는 함수. 원본 , 자기 자신에게 넣음
				select.name = effectName; // 이름 설정
				pools[effectName].Add(select); // pools에 등록
			}
		}

		return select;
	}

	private string GetEffectName(int index, int type)
	{
		switch (type)
		{
			case 0: return jumpEffects[index].name;
			case 1: return glideEffects[index].name;
			case 2: return destroyObjectEffects[index].name;
			case 4: return shieldEffects[index].name;
			case 5: return boosterEffects[index].name;
			case 6: return healigEffects[index].name;
			case 7: return invincibleEffects[index].name;
			case 8: return fireflyEffects[index].name;
			default: return null;
		}
	}

	private GameObject GetPrefab(int index, int type)
	{
		switch (type)
		{
			case 0: return jumpEffects[index];
			case 1: return glideEffects[index];
			case 2: return destroyObjectEffects[index];
			case 4: return shieldEffects[index];
			case 5: return boosterEffects[index];
			case 6: return healigEffects[index];
			case 7: return invincibleEffects[index];
			case 8: return fireflyEffects[index];
			default: return null;
		}
	}
}