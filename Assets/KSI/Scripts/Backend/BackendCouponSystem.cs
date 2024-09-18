using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackendCouponSystem : MonoBehaviour
{
	[SerializeField] private TMP_InputField inputFieldCode;
	[SerializeField] private FadeEffect_TMP textResult;

	public void ReceiveCoupon()
	{
		string couponCode = inputFieldCode.text;

		if (couponCode.Trim().Equals(""))
		{
			textResult.FadeOut("쿠폰 코드를 입력해주세요.");
			return;
		}

		inputFieldCode.text = "";

		ReceiveCoupon(couponCode);
	}

	public void ReceiveCoupon(string couponCode)
	{
		Backend.Coupon.UseCoupon(couponCode, callback =>
		{
			if (!callback.IsSuccess())
			{
				FailedToReceive(callback);
				return;
			}
			try
			{
				LitJson.JsonData jsonData = callback.GetFlattenJSON()["itemObject"];

				if (jsonData.Count <= 0)
				{
					Debug.LogWarning("쿠폰에 아이템이 없습니다.");
					return;
				}
				SaveToLocal(jsonData);
			}
			catch (System.Exception e)
			{
				Debug.LogError(e);
			}
		});
	}

	private void FailedToReceive(BackendReturnObject callback)
	{
		if (callback.GetMessage().Contains("전부 사용된"))
		{
			textResult.FadeOut("쿠폰 발행 개수가 소진되었거나 기간이 만료된 쿠폰입니다.");
		}
		else if (callback.GetMessage().Contains("이미 사용하신 쿠폰"))
		{
			textResult.FadeOut("해당 쿠폰은 이미 사용하셨습니다.");
		}
		else
		{
			textResult.FadeOut("쿠폰 코드가 잘못되었거나 이미 사용한 쿠폰입니다.");
		}

		Debug.LogError($"쿠폰 사용 중 에러가 발생했습니다 : {callback}");
	}

	private void SaveToLocal(LitJson.JsonData items)
	{
		try
		{
			string getItems = string.Empty;

			foreach (LitJson.JsonData item in items)
			{
				int itemId = int.Parse(item["item"]["itemId"].ToString());
				string itemName = item["item"]["itemName"].ToString();
				string itemInfo = item["item"]["itemInfo"].ToString();
				int itemCount = int.Parse(item["itemCount"].ToString());

				if (itemName.Equals("sushi")) BackendGameData.Instance.UserGameData.sushi += itemCount;
				else if (itemName.Equals("cannedFood")) BackendGameData.Instance.UserGameData.cannedFood += itemCount;
				else if (itemName.Equals("silverKey")) BackendGameData.Instance.UserGameData.silverKey += itemCount;
				else if (itemName.Equals("goldKey")) BackendGameData.Instance.UserGameData.goldKey += itemCount;
				else if (itemName.Equals("money")) BackendGameData.Instance.UserGameData.money += itemCount;
				else if (itemName.Equals("brokenBlue")) BackendGameData.Instance.UserGameData.brokenBlue += itemCount;
				else if (itemName.Equals("brokenRed")) BackendGameData.Instance.UserGameData.brokenRed += itemCount;
				else if (itemName.Equals("brokenGreen")) BackendGameData.Instance.UserGameData.brokenGreen += itemCount;

				else if (itemName.Equals("resurrection")) BackendGameData.Instance.UserGameData.resurrection += itemCount;
				else if (itemName.Equals("wizardHuntBackground")) BackendGameData.Instance.UserGameData.wizardHuntBackground += itemCount;
				else if (itemName.Equals("wizardHuntEffect")) BackendGameData.Instance.UserGameData.wizardHuntEffect += itemCount;
				else if (itemName.Equals("wizardHuntSkin")) BackendGameData.Instance.UserGameData.wizardHuntSkin += itemCount;
				else if (itemName.Equals("nabinyangBackground")) BackendGameData.Instance.UserGameData.nabinyangBackground += itemCount;
				else if (itemName.Equals("nabinyangEffect")) BackendGameData.Instance.UserGameData.nabinyangEffect += itemCount;
				else if (itemName.Equals("nabinyangSkin")) BackendGameData.Instance.UserGameData.nabinyangSkin += itemCount;

				getItems += $"[{itemName}:{itemCount}]";
			}

			textResult.FadeOut($"쿠폰 사용으로 아이템 {getItems}을 획득했습니다.");

			BackendGameData.Instance.GameDataUpdate();
		}
		catch (System.Exception e)
		{
			Debug.LogError(e);
		}
	}
}
