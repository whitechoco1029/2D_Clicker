using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public Text goldText;
    public UserData userData;

    private void Update()
    {
        goldText.text = FormatGold(GameManager.Instance.userData.gold);
    }

    string FormatGold(float gold)
    {
        if (gold >= 1_000_000_000_000)
            return $"{gold / 1000000000000f:0.00}Á¶";
        if (gold >= 100_000_000)
            return $"{gold / 100000000f:0.00}¾ï";
        if (gold >= 10_000)
            return $"{gold / 10000f:0.00}¸¸";
        return $"{gold.ToString("N0")}";
    }

}
