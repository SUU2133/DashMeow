[System.Serializable]
public class UserGameData
{
	public int sushi;
	public int cannedFood;
	public int silverKey;
	public int goldKey;
	public int money;
	public int brokenBlue;
	public int brokenRed;
	public int brokenGreen;
	public int INFINITBestscore;
	public int resurrection;
	public int wizardHuntBackground;
	public int wizardHuntEffect;
	public int wizardHuntSkin;
	public int nabinyangBackground;
	public int nabinyangEffect;
	public int nabinyangSkin;

	public void Reset()
	{
		sushi = 100000;
		cannedFood = 10000;
		silverKey = 30;
		goldKey = 5;
		INFINITBestscore = 0;
	}
}