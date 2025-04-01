using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public Text goldText;
    public UserData userData;

    private void Update()
    {
        goldText.text = $"Gold: {GameManager.Instance.userData.gold:0}";
    }
}
