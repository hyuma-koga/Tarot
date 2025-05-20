using UnityEngine;

public class TarotUIManager : MonoBehaviour
{
    [SerializeField] private GameObject cardRingCanvas;
    [SerializeField] private GameObject titleCanvas;         //タイトル画面
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
        //UI切り替え
        titleCanvas.SetActive(true);
        cardRingCanvas.SetActive(false);

        //UIリセット
        tarotCardUI.HideDetails();

        //状態初期化
        tarotGameManager.ResetState();

        //カード削除
        foreach (Transform child in tarotGameManager.cardRingTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
