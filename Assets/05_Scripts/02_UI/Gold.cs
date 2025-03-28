using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public TMP_Text goldText;
    public UserData userData;

    private void Update()
    {
        goldText.text = $"Gold: {userData.gold:0}";
    }
}
