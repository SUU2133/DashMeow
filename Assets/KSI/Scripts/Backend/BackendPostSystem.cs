using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackendPostSystem : MonoBehaviour
{
	[System.Serializable]
	public class PostEvent : UnityEvent<List<PostData>> { }
	public PostEvent onGetPostListEvent = new PostEvent();

	private List<PostData> postList = new List<PostData>();

	public void PostListGet()
	{
		PostListGet(PostType.Admin);
	}

	public void PostReceive(PostType postType, string inDate)
	{
		PostReceive(postType, postList.FindIndex(item => item.inDate.Equals(inDate)));
	}

	public void PostReceiveAll()
	{
		PostReceiveAll(PostType.Admin);
	}

	public void PostListGet(PostType postType)
	{
		Backend.UPost.GetPostList(postType, callback =>
		{
			if (!callback.IsSuccess())
			{
				Debug.LogError($"우편 불러오기 중 에러가 발생했습니다. : {callback}");
				return;
			}

			Debug.Log($"우편 리스트 불러오기 요청에 성공했습니다. : {callback}");

			try
			{
				LitJson.JsonData jsonData = callback.GetFlattenJSON()["postList"];

				if (jsonData.Count <= 0)
				{
					Debug.LogWarning("우편함이 비어있습니다.");
					return;
				}

				postList.Clear();

				for (int i = 0; i < jsonData.Count; ++i)
				{
					PostData post = new PostData();

					post.title = jsonData[i]["title"].ToString();
					post.content = jsonData[i]["content"].ToString();
					post.inDate = jsonData[i]["inDate"].ToString();
					post.expirationDate = jsonData[i]["expirationDate"].ToString();

					foreach (LitJson.JsonData itemJson in jsonData[i]["items"])
					{
						if (itemJson["chartName"].ToString() == KSIConstants.GOODS_CHART_NAME)
						{
							string itemName = itemJson["item"]["itemName"].ToString();
							int itemCount = int.Parse(itemJson["itemCount"].ToString());

							if (post.postReward.ContainsKey(itemName))
							{
								post.postReward[itemName] += itemCount;
							}
							else
							{
								post.postReward.Add(itemName, itemCount);
							}

							post.isCanReceive = true;
						}
						else
						{
							Debug.LogWarning($"아직 지원하지 않는 차트 정보입니다. : {itemJson["chartName"].ToString()}");

							post.isCanReceive = false;
						}
					}

					postList.Add(post);
				}

				onGetPostListEvent?.Invoke(postList);

				for (int i = 0; i < postList.Count; ++i)
				{
					Debug.Log($"{i}번째 우편\n{postList[i].ToString()}");
				}
			}
			catch (System.Exception e)
			{
				Debug.LogError(e);
			}
		});
	}

	public void PostReceive(PostType postType, int index)
	{
		if (postList.Count <= 0)
		{
			Debug.LogWarning("받을 수 있는 우편이 존재하지 않습니다. 혹은 우편 리스트 불러오기를 먼저 호출해주세요.");
			return;
		}

		if (index >= postList.Count)
		{
			Debug.LogError($"해당 우편은 존재하지 않습니다. : 요청 index{index} / 우편 최대 개수 : {postList.Count}");
			return;
		}

		Debug.Log($"{postType.ToString()}의 {postList[index].inDate} 우편수령을 요청합니다.");

		Backend.UPost.ReceivePostItem(postType, postList[index].inDate, callback =>
		{
			if (!callback.IsSuccess())
			{
				Debug.LogError($"{postType.ToString()}의 {postList[index].inDate} 우편수령 중 에러가 발생했습니다. : {callback}");
				return;
			}

			Debug.Log($"{postType.ToString()}의 {postList[index].inDate} 우편수령에 성공했습니다. : {callback}");

			postList.RemoveAt(index);

			if (callback.GetFlattenJSON()["postItems"].Count > 0)
			{
				SavePostToLocal(callback.GetFlattenJSON()["postItems"]);
				

			}
			else
			{
				Debug.LogWarning("수령 가능한 우편 아이템이 존재하지 않습니다.");
			}
		});
	}

	public void PostReceiveAll(PostType postType)
	{
		if (postList.Count <= 0)
		{
			Debug.LogWarning("받을 수 있는 우편이 존재하지 않습니다. 혹은 우편 리스트 불러오기를 먼저 호출해주세요.");
			return;
		}

		Debug.Log($"{postType.ToString()} 우편 전체 수령을 요청합니다.");

		Backend.UPost.ReceivePostItemAll(postType, callback =>
		{
			if (!callback.IsSuccess())
			{
				Debug.LogError($"{postType.ToString()} 우편 전체 수령 중 에러가 발생했습니다. : {callback}");
				return;
			}

			Debug.Log($"우편 전체 수령에 성공했습니다. : {callback}");

			postList.Clear();

			foreach (LitJson.JsonData postItemsJson in callback.GetFlattenJSON()["postItems"])
			{
				SavePostToLocal(postItemsJson);
			}

			BackendGameData.Instance.GameDataUpdate();
		});
	}

	public void SavePostToLocal(LitJson.JsonData item)
	{
		try
		{
			foreach (LitJson.JsonData itemJson in item)
			{
				string chartFileName = itemJson["item"]["chartFileName"].ToString();
				string chartName = itemJson["chartName"].ToString();

				int itemId = int.Parse(itemJson["item"]["itemId"].ToString());
				string itemName = itemJson["item"]["itemName"].ToString();
				string itemInfo = itemJson["item"]["itemInfo"].ToString();

				int itemCount = int.Parse(itemJson["itemCount"].ToString());

				if (chartName.Equals(KSIConstants.GOODS_CHART_NAME))
				{
					if (itemName.Equals("sushi"))
					{
						BackendGameData.Instance.UserGameData.sushi += itemCount;
					}
					else if (itemName.Equals("cannedFood"))
					{
						BackendGameData.Instance.UserGameData.cannedFood += itemCount;
					}
					else if (itemName.Equals("silverKey"))
					{
						BackendGameData.Instance.UserGameData.silverKey += itemCount;
					}
					else if (itemName.Equals("goldKey"))
					{
						BackendGameData.Instance.UserGameData.goldKey += itemCount;
					}
					else if (itemName.Equals("money"))
					{
						BackendGameData.Instance.UserGameData.money += itemCount;
					}
					else if (itemName.Equals("brokenBlue"))
					{
						BackendGameData.Instance.UserGameData.brokenBlue += itemCount;
					}
					else if (itemName.Equals("brokenRed"))
					{
						BackendGameData.Instance.UserGameData.brokenRed += itemCount;
					}
					else if (itemName.Equals("brokenGreen"))
					{
						BackendGameData.Instance.UserGameData.brokenGreen += itemCount;
					}

					else if (itemName.Equals("resurrection"))
					{
						BackendGameData.Instance.UserGameData.resurrection += itemCount;
					}
					else if (itemName.Equals("wizardHuntBackground"))
					{
						BackendGameData.Instance.UserGameData.wizardHuntBackground += itemCount;
					}
					else if (itemName.Equals("wizardHuntEffect"))
					{
						BackendGameData.Instance.UserGameData.wizardHuntEffect += itemCount;
					}
					else if (itemName.Equals("wizardHuntSkin"))
					{
						BackendGameData.Instance.UserGameData.wizardHuntSkin += itemCount;
					}
					else if (itemName.Equals("nabinyangBackground"))
					{
						BackendGameData.Instance.UserGameData.nabinyangBackground += itemCount;
					}
					else if (itemName.Equals("nabinyangEffect"))
					{
						BackendGameData.Instance.UserGameData.nabinyangEffect += itemCount;
					}
					else if (itemName.Equals("nabinyangSkin"))
					{
						BackendGameData.Instance.UserGameData.nabinyangSkin += itemCount;
					}
				}
				BackendGameData.Instance.GameDataUpdate();
				Debug.Log($"{chartName} - {chartFileName}");
				Debug.Log($"[{itemId}] {itemName} : {itemInfo}, 획득 수량 : {itemCount}");
				Debug.Log($"아이템을 수령했습니다. : {itemName} - {itemCount}개");
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e);
		}
	}
}