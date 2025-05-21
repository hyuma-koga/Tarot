using UnityEngine;

public class TarotUIManager : MonoBehaviour
{
    [SerializeField] private GameObject cardRingCanvas;
    [SerializeField] private GameObject titleCanvas;         //�^�C�g�����
    [SerializeField] private GameObject backgroundResult;
    [SerializeField] private TarotCardSpawner cardSpawner;
    [SerializeField] private TarotGameManager tarotGameManager;
    [SerializeField] private TarotCardUI tarotCardUI;
    public void OnClickFortuneButton()
    {
        tarotGameManager.ResetCameraPosition(); // �� �K���ŏ��ɌĂ�
        titleCanvas.SetActive(false);
        cardRingCanvas.SetActive(true);
        backgroundResult.SetActive(false);
        cardSpawner.SpawnCards();
    }

    public void ReturnToTitle()
    {
        //UI�؂�ւ�
        titleCanvas.SetActive(true);
        cardRingCanvas.SetActive(false);
        backgroundResult.SetActive(false);

        //UI���Z�b�g
        tarotCardUI.HideDetails();

        //��ԏ�����
        tarotGameManager.ResetState();

        //�J�[�h�폜
        foreach (Transform child in tarotGameManager.cardRingTransform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowResultScreen()
    {
        backgroundResult.SetActive(true);
    }
}
