using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
	private void Awake()
	{
		BackendSetup();
	}

	private void BackendSetup()
	{
		var bro = Backend.Initialize(true);

		if (bro.IsSuccess())
		{
			Debug.Log($"초기화 성공 : {bro}");
		}
		else
		{
			Debug.LogError($"초기화 실패 : {bro}");
		}
	}
}


