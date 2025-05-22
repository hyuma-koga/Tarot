using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CardHistoryUI : MonoBehaviour
{
    public GameObject historyPanel;
    public GameObject historyItemPrefab;
    public Transform contentParent;
    public Scrollbar verticalScrollbar;

    public void ShowHistory()
    {
        StartCoroutine(DelayedShowHistory());
    }

    private IEnumerator DelayedShowHistory()
    {
        Debug.Log($"履歴の数: {TarotGameManager.historyList.Count} 件");

        // パネルを先に表示（←ここが大事！）
        historyPanel.SetActive(true);

        // 1フレーム待ってCanvas更新（LayoutGroupが反映されるように）
        yield return null;

        // スクロールバーを表示
        if (verticalScrollbar != null)
            verticalScrollbar.gameObject.SetActive(true);

        // 既存アイテム削除
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 履歴を表示
        foreach (var entry in TarotGameManager.historyList)
        {
            GameObject item = Instantiate(historyItemPrefab, contentParent);
            var texts = item.GetComponentsInChildren<TextMeshProUGUI>();

            if (texts.Length >= 5)
            {
                texts[0].text = $"カード名：{entry.cardName}";
                texts[1].text = entry.isReversed ? "逆位置" : "正位置";
                texts[2].text = $"意味：{entry.meaning}";
                texts[3].text = $"説明：{entry.description}";
                texts[4].text = $"日付：{entry.date}";
            }
            else
            {
                Debug.LogWarning("CardHistoryItem 内の TextMeshProUGUI が足りません！");
            }
        }
        RectTransform contentRect = contentParent.GetComponent<RectTransform>();
        Debug.Log($"Contentの高さ: {contentRect.rect.height}");
        // レイアウトを強制更新（念のため）
        Canvas.ForceUpdateCanvases();

        // スクロール位置を一番上へ
        var scrollRect = GetComponentInChildren<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    public void HideHistory()
    {
        if (verticalScrollbar != null)
            verticalScrollbar.gameObject.SetActive(false);

        historyPanel.SetActive(false);
    }

    public void PopulateHistory()
    {
        Debug.Log($"履歴の数: {TarotGameManager.historyList.Count} 件");

        if (verticalScrollbar != null)
            verticalScrollbar.gameObject.SetActive(true);

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var entry in TarotGameManager.historyList)
        {
            GameObject item = Instantiate(historyItemPrefab, contentParent);
            var texts = item.GetComponentsInChildren<TextMeshProUGUI>();
            var image = item.GetComponentInChildren<Image>();

            if (texts.Length >= 5)
            {
                texts[0].text = $"{entry.date}";
                texts[1].text = $"{entry.cardName}";
                texts[2].text = entry.isReversed ? "逆位置" : "正位置";
                texts[3].text = $"{entry.meaning}";
                texts[4].text = $"{entry.description}";
                
            }
            else
            {
                Debug.LogWarning("CardHistoryItem 内の TextMeshProUGUI が足りません！");
            }

            Transform imageTransform = item.transform.Find("CardImage");
            if (imageTransform != null)
            {
                Image cardImage = imageTransform.GetComponent<Image>();
                if (cardImage != null)
                {
                    Sprite sprite = Resources.Load<Sprite>("TarotImages/" + entry.imageName);
                    if (sprite != null)
                    {
                        Debug.Log($"読み込み成功: TarotImages/{entry.imageName}");
                        cardImage.sprite = sprite;

                        //逆位置なら上下反転
                        cardImage.rectTransform.localRotation = entry.isReversed
                           ? Quaternion.Euler(0, 0, 180)
                           : Quaternion.identity;

                        cardImage.preserveAspect = true; // ← 追加で見た目も整える
                    }
                    else
                    {
                        Debug.LogWarning("Image コンポーネントが CardImage に見つかりません！");
                    }
                }
                else
                {
                    Debug.LogWarning("CardImage オブジェクトが見つかりません！");
                }
            }
        }

        // デバッグ：Content高さ出力
        RectTransform contentRect = contentParent.GetComponent<RectTransform>();
        Debug.Log($"📏 Contentの高さ: {contentRect.rect.height}");

        Canvas.ForceUpdateCanvases();

        var scrollRect = GetComponentInChildren<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    public void OnClickBackButton()
    {
        Debug.Log("戻るボタンが押されました");

        // パネル自体を非表示
        historyPanel.SetActive(false);

        // Scrollbar 非表示（オプション）
        if (verticalScrollbar != null)
            verticalScrollbar.gameObject.SetActive(false);

        // タイトルへ戻す
        TarotUIManager uiManager = FindAnyObjectByType<TarotUIManager>();
        if (uiManager != null)
        {
            uiManager.ReturnToTitle();
        }
        else
        {
            Debug.LogWarning("TarotUIManager が見つかりません！");
        }
    }

}
