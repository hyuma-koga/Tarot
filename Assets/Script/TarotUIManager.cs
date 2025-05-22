using System.Collections;
using UnityEngine;

public class TarotUIManager : MonoBehaviour
{
    [SerializeField] private GameObject cardRingCanvas;
    [SerializeField] private GameObject titleCanvas;         //タイトル画面
    [SerializeField] private GameObject backgroundResult;
    [SerializeField] private TarotCardSpawner cardSpawner;
    [SerializeField] private TarotGameManager tarotGameManager;
    [SerializeField] private TarotCardUI tarotCardUI;
    [SerializeField] private CardHistoryUI historyUI;
    [SerializeField] private GameObject noHistoryPopup;
    public void OnClickFortuneButton()
    {
        tarotGameManager.ResetCameraPosition(); // ← 必ず最初に呼ぶ
        titleCanvas.SetActive(false);
        cardRingCanvas.SetActive(true);
        backgroundResult.SetActive(false);
        cardSpawner.SpawnCards();
    }

    public void ReturnToTitle()
    {
        titleCanvas.SetActive(true);
        cardRingCanvas.SetActive(false);
        backgroundResult.SetActive(false);

        // 📌 CardHistoryPanel を確実に非表示にする
        historyUI.historyPanel.SetActive(false);

        // UIリセット
        tarotCardUI.HideDetails();

        // 状態初期化
        tarotGameManager.ResetState();

        // カード削除
        foreach (Transform child in tarotGameManager.cardRingTransform)
        {
            Destroy(child.gameObject);
        }
    }


    public void ShowResultScreen()
    {
        backgroundResult.SetActive(true);
    }

    public void OnClickHistoryButton()
    {
        Debug.Log($"履歴の数: {TarotGameManager.historyList.Count}");

        if (TarotGameManager.historyList.Count == 0)
        {
            Debug.Log("履歴が空なので開けません！");
            if (noHistoryPopup != null)
                StartCoroutine(ShowNoHistoryPopup());
            return;
        }

        if (noHistoryPopup != null && noHistoryPopup.activeSelf)
        {
            noHistoryPopup.SetActive(false); // 念のため確実に閉じる
        }

        historyUI.historyPanel.SetActive(true);
        historyUI.ShowHistory();
    }



    private IEnumerator ShowNoHistoryPopup()
    {
        noHistoryPopup.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noHistoryPopup.SetActive(false);
    }

}
