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

        historyPanel.SetActive(true);
        yield return null;

        if (verticalScrollbar != null)
            verticalScrollbar.gameObject.SetActive(true);

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

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
                        cardImage.rectTransform.localRotation = entry.isReversed ? Quaternion.Euler(0, 0, 180) : Quaternion.identity;
                        cardImage.preserveAspect = true;
                    }
                    else
                    {
                        Debug.LogWarning("画像が見つかりません: " + entry.imageName);
                    }
                }
            }

            // 意味・説明 背景パネル
            Transform meaningBG = item.transform.Find("MeaningBackgroundPanel");
            Transform descriptionBG = item.transform.Find("DiscriptionBackgroundPanel");

            // 意味テキストの高さに合わせて背景サイズ調整
            Transform meaningTextTransform = meaningBG?.Find("MeaningText");
            if (meaningTextTransform != null)
            {
                TextMeshProUGUI meaningText = meaningTextTransform.GetComponent<TextMeshProUGUI>();
                RectTransform meaningBGRect = meaningBG.GetComponent<RectTransform>();
                if (meaningText != null && meaningBGRect != null)
                {
                    float targetHeight = meaningText.preferredHeight + 0f;
                    meaningBGRect.sizeDelta = new Vector2(meaningBGRect.sizeDelta.x, targetHeight);
                }
            }

            // 説明テキストの高さに合わせて背景サイズ調整
            Transform descriptionTextTransform = descriptionBG?.Find("DescriptionText");
            if (descriptionTextTransform != null)
            {
                TextMeshProUGUI descriptionText = descriptionTextTransform.GetComponent<TextMeshProUGUI>();
                RectTransform descriptionBGRect = descriptionBG.GetComponent<RectTransform>();
                if (descriptionText != null && descriptionBGRect != null)
                {
                    float targetHeight = descriptionText.preferredHeight + 0f;
                    descriptionBGRect.sizeDelta = new Vector2(descriptionBGRect.sizeDelta.x, targetHeight);
                }
            }
        }

        RectTransform contentRect = contentParent.GetComponent<RectTransform>();
        Debug.Log($"\ud83d\udccf Contentの高さ: {contentRect.rect.height}");

        Canvas.ForceUpdateCanvases();

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

            //画像処理
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
            }

            //意味・説明 背景パネル
            Transform meaningBG = item.transform.Find("MeaningBackground");
            Transform descriptionBG = item.transform.Find("DiscriptionBackground");

            if (meaningBG != null) meaningBG.gameObject.SetActive(true);
            if (descriptionBG != null) descriptionBG.gameObject.SetActive(true);
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
