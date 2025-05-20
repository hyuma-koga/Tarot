using UnityEngine;

public class TarotUIManager : MonoBehaviour
{
    [SerializeField] private GameObject cardRingCanvas;
    [SerializeField] private GameObject titleCanvas;         //�^�C�g�����
    [SerializeField] private TarotCardSpawner cardSpawner;
    [SerializeField] private TarotGameManager tarotGameManager;
    [SerializeField] private TarotCardUI tarotCardUI;
    public void OnClickFortuneButton()
    {
        titleCanvas.SetActive(false);
        cardRingCanvas.SetActive(true);
        cardSpawner.SpawnCards();
    }

    public void ReturnToTitle()
    {
        //UI�؂�ւ�
        titleCanvas.SetActive(true);
        cardRingCanvas.SetActive(false);

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
}
