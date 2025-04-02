using UnityEngine;

public class StatPopupController : MonoBehaviour
{
    public GameObject popupPanel;

    public void TogglePopup()
    {
        popupPanel.SetActive(!popupPanel.activeSelf);
    }

    public void OpenPopup()
    {
        Debug.Log("팝업 열기 시도"); //디버그 로그 추가
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
