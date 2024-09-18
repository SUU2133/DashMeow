using UnityEngine;

public class UnlockNonbuy : MonoBehaviour
{
	public string objectName; // 객체 이름

	private void Start()
	{
		//초기화
		//ResetUnlockStatus();
		// 이전에 저장된 상태를 불러와서 비활성화된 상태면 비활성화 시킴
		/*
		if (PlayerPrefs.GetInt(objectName + "_unlocked", 0) == 1)
		{
			gameObject.SetActive(false);
		}
		else
		{
			InvokeRepeating("Unlock", 0f, 1f);
		}
		*/
		InvokeRepeating("Unlock", 0f, 1f);
	}

	public void Unlock()
	{
		if (objectName == "magiccatskin")
		{
			if (DataManager.Instance.magicCatSkin == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		}
		else if(objectName == "magiccatwallpaper")
		{
			if (DataManager.Instance.magicCatWallpaper == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		}
		else if (objectName == "magiccatbackeffect")
		{
			if (DataManager.Instance.magicCatEffect == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		}


		else if(objectName == "butterflycatskin")
		{

			if (DataManager.Instance.butterflyCatSkin == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		
		}
		else if (objectName == "butterflycatwallpaper")
		{

			if (DataManager.Instance.butterflyCatWallpaper == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}

		}
		else if (objectName == "butterflycateffect")
		{

			if (DataManager.Instance.butterflyCatEffect == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}

		}

		else if (objectName == "catgage1")
		{
			if (DataManager.Instance.catGage == 1)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "catgage2")
		{
			if (DataManager.Instance.catGage == 3)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "catgage3")
		{
			if (DataManager.Instance.catGage == 5)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "catgage4")
		{
			if (DataManager.Instance.catGage == 7)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "catgage5")
		{
			if (DataManager.Instance.catGage == 10)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "gagecatskin")
		{
			if (DataManager.Instance.gageCatSkin == 1)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "gagecatwallpaper")
		{
			if (DataManager.Instance.gageCatWallpaper == 1)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "gagecateffect")
		{
			if (DataManager.Instance.gageCatEffect == 1)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}

		else if (objectName == "nerocatskin")
		{
			if (DataManager.Instance.neroCatSkin == 1)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}
		else if (objectName == "nerocatwallpaper")
		{
			if (DataManager.Instance.neroCatWallpaper == 1)
			{
				gameObject.SetActive(false);
				//PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				//PlayerPrefs.Save();
			}
		}

	}


	public void ResetUnlockStatus()
	{
		// 저장된 비활성화 상태를 리셋
		PlayerPrefs.DeleteKey(objectName + "_unlocked");
		PlayerPrefs.Save();

		// 오브젝트를 다시 활성화
		gameObject.SetActive(true);
	}
}