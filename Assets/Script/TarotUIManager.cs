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
        // 先にパネルを表示（CardHistoryUI はこの瞬間アクティブになる）
        historyUI.historyPanel.SetActive(true);

        // 自分（TarotUIManager）は常にアクティブなので、ここで Coroutine 実行
        StartCoroutine(ShowHistoryCoroutine());
    }

    private IEnumerator ShowHistoryCoroutine()
    {
        yield return null; // 1フレーム待つ（レイアウト更新のため）

        historyUI.PopulateHistory(); // ← 履歴データの生成処理はここに移す
    }


}
