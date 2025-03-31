using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public Text goldText;
    public UserData userData;

    private void Update()
    {
        goldText.text = $"{userData.gold:0}°³";
    }
}
