using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using System; // TimeSpan

public class Post : MonoBehaviour
{
	[SerializeField] private Sprite[] spriteItemIcons; // 우편에 포함된 아이템 아이콘에 출력할 이미지 배열
	[SerializeField] private Image imageItemIcon; // 우편에 포함된 아이템 아이콘 출력
	[SerializeField] private TextMeshProUGUI textItemCount; // 우편에 포함된 아이템의 개수
	[SerializeField] private TextMeshProUGUI textTitle; // 우편 제목
	[SerializeField] private TextMeshProUGUI textContent; // 우편 내용
	[SerializeField] private TextMeshProUGUI textExpirationDate; // 우편 만료까지 남은 시간 출력
    [SerializeField] private Button buttonReceive; // 우편 "수령" 버튼 처리

	private BackendPostSystem backendPostSystem;
	private PopUpPostBox popUpPostBox;
	private PostData postData;

	public void Setup(BackendPostSystem postSystem, PopUpPostBox postBox, PostData postData)
	{
		buttonReceive.onClick.AddListener(OnClickPostReceive);

		backendPostSystem = postSystem;
		popUpPostBox = postBox;
		this.postData = postData;

		textTitle.text = postData.title;
		textContent.text = postData.content;

		foreach (string itemKey in postData.postReward.Keys)
		{
			if (itemKey.Equals("sushi")) imageItemIcon.sprite = spriteItemIcons[0];
			else if (itemKey.Equals("cannedFood")) imageItemIcon.sprite = spriteItemIcons[1];
			else if (itemKey.Equals("silverKey")) imageItemIcon.sprite = spriteItemIcons[2];
			else if (itemKey.Equals("goldKey")) imageItemIcon.sprite = spriteItemIcons[3];
			else if (itemKey.Equals("money")) imageItemIcon.sprite = spriteItemIcons[4];
			else if (itemKey.Equals("brokenBlue")) imageItemIcon.sprite = spriteItemIcons[5];
			else if (itemKey.Equals("brokenRed")) imageItemIcon.sprite = spriteItemIcons[6];
			else if (itemKey.Equals("brokenGreen")) imageItemIcon.sprite = spriteItemIcons[7];

			else if (itemKey.Equals("resurrection")) imageItemIcon.sprite = spriteItemIcons[8];
			else if (itemKey.Equals("WizardHuntBackground")) imageItemIcon.sprite = spriteItemIcons[9];
			else if (itemKey.Equals("wizardHuntEffect")) imageItemIcon.sprite = spriteItemIcons[10];
			else if (itemKey.Equals("wizardHuntSkin")) imageItemIcon.sprite = spriteItemIcons[11];
			else if (itemKey.Equals("nabinyangBackground")) imageItemIcon.sprite = spriteItemIcons[12];
			else if (itemKey.Equals("nabinyangEffect")) imageItemIcon.sprite = spriteItemIcons[13];
			else if (itemKey.Equals("nabinyangSkin")) imageItemIcon.sprite = spriteItemIcons[14];

			textItemCount.text = postData.postReward[itemKey].ToString();

			break;
		}

		Backend.Utils.GetServerTime(callback =>
		{
			if (!callback.IsSuccess())
			{
				Debug.LogError($"서버 시간 불러오기에 실패했습니다. : {callback}");
				return;
			}

			try
			{
				// 현재 서버 시간
				string serverTime = callback.GetFlattenJSON()["utcTime"].ToString();

				// 우편 만료까지 남은 시간 = 우편 만료 시간 - 현재 서버 시간
				TimeSpan timeSpan = DateTime.Parse(postData.expirationDate) - DateTime.Parse(serverTime);

				// timeSpan.TotalHours로 남은 기간을 시(hour)로 표현
				textExpirationDate.text = $"{timeSpan.TotalHours:F0}시간 후 만료";
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		});
	}

	private void OnClickPostReceive()
	{
		popUpPostBox.DestroyPost(gameObject);

		backendPostSystem.PostReceive(PostType.Admin, postData.inDate);
	}
}
