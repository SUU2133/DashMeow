using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PopUpPostBox : MonoBehaviour
{
	[SerializeField] private BackendPostSystem backendPostSystem;
	[SerializeField] private GameObject postPrefab; // 우편 UI 프리팹
	[SerializeField] private Transform parentContent; // 우편 UI가 배치되는 ScrollView의 Content
	[SerializeField] private GameObject alertText; // "우편함이 비어있습니다" 텍스트 오브젝트

	private List<GameObject> postList;

	private void Awake()
	{
		postList = new List<GameObject>();
	}

	private void OnDisable()
	{
		DestroyPostAll();
	}

	public void SpawnPostAll(List<PostData> postDataList)
	{
		for (int i = 0; i < postDataList.Count; ++i)
		{
			GameObject clone = Instantiate(postPrefab, parentContent);
			clone.GetComponent<Post>().Setup(backendPostSystem, this, postDataList[i]);

			postList.Add(clone);
		}

		alertText.SetActive(false);
	}

	public void DestroyPostAll()
	{
		foreach (GameObject post in postList)
		{
			if (post != null) Destroy(post);
		}

		postList.Clear();

		alertText.SetActive(true);
	}

	public void DestroyPost(GameObject post)
	{
		Destroy(post);
		postList.Remove(post);

		if (postList.Count == 0)
		{
			alertText.SetActive(true);
		}
	}
}
