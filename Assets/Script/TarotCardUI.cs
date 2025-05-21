using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TarotCardUI : MonoBehaviour
{
    [Header("UI Components")]
    public Image cardImage;
    public Sprite cardBackSprite;
    public TMP_Text cardNameText;
    public TMP_Text meaningText;
    public TMP_Text discriptionText;
    public TMP_Text imageNameText;
    public RectTransform cardRootTransform; //回転用親オブジェクト
    public GameObject backToTitleButton;
    public GameObject descriptionBackgroundPanel;  // ← 背景パネルをInspectorからアサイン
    public GameObject meaningBackgroundPanel;  // ← 背景パネルをInspectorからアサイン

    public RectTransform meaningBackgroundPanelRect; 
    public TextMeshProUGUI meaningTextTMP; 
    public RectTransform descriptionBackgroundPanelRect; 
    public TextMeshProUGUI discriptionTextTMP; 


    /// <summary>
    /// タロットカードの表を表示
    /// </summary>
    public void DisplayCard(TarotCardData cardData, bool showFront = true)
    {
        if (cardData == null)
        {
            Debug.LogWarning("カードデータがありません");
            return;
        }

        if (showFront)
        {
            cardImage.sprite = cardData.illustration;
            cardNameText.text = $"{cardData.cardName}";
            imageNameText.text = $"{cardData.imageName}";
            meaningText.text = cardData.isReversed
                ? $"逆位置：{cardData.meaningReversed}"
                : $"正位置：{cardData.meaningUpright}";
            discriptionText.text = cardData.isReversed
               ? $"{cardData.discriptionReversed}"
               : $"{cardData.discriptionUpright}";

            //画像を上下反転
            cardImage.rectTransform.localRotation = cardData.isReversed
                ? Quaternion.Euler(0, 0, 180)
                : Quaternion.identity;
        }
        else
        {
            ShowCardBack();
        }  
    }

    /// <summary>
    /// カードの裏面を表示（非公開情報のときなど）
    /// </summary>
    /// 
    public void ShowCardBack()
    {
        cardImage.sprite = cardBackSprite;
        cardNameText.text = "";
        imageNameText.text = "";
        meaningText.text = "";
        discriptionText.text = "";
    }

    /// <summary>
    /// カードの表面のみ更新（表示済み前提）
    /// </summary>
    public void ShowCardFront(TarotCardData cardData)
    {
        Debug.Log("ShowCardFront が呼ばれました！");
        DisplayCard(cardData, true);
    }
    public void HideDetails()
    {
        cardImage.gameObject.SetActive(false);
        cardNameText.gameObject.SetActive(false);
        imageNameText.gameObject.SetActive(false);
        meaningText.gameObject.SetActive(false);
        discriptionText.gameObject.SetActive(false);

        if (backToTitleButton != null)
        {
            backToTitleButton.SetActive(false);
        }

        if (descriptionBackgroundPanel != null)
        {
            descriptionBackgroundPanel.SetActive(false);  // 背景パネルを表示
        }

        if (meaningBackgroundPanel != null)
        {
            meaningBackgroundPanel.SetActive(false);  // 背景パネルを表示
        }
    }

    public void ShowDetails()
    {
        cardImage.gameObject.SetActive(true);
        cardNameText.gameObject.SetActive(true);
        imageNameText.gameObject.SetActive(true);
        meaningText.gameObject.SetActive(true);
        discriptionText.gameObject.SetActive(true);

        if(backToTitleButton != null)
        {
            backToTitleButton.SetActive(true);
        }

        if (descriptionBackgroundPanel != null)
        {
            descriptionBackgroundPanel.SetActive(true);  // 背景パネルを表示
        }

        if (meaningBackgroundPanel != null)
        {
            meaningBackgroundPanel.SetActive(true);  // 背景パネルを表示
        }

        if (descriptionBackgroundPanelRect != null && discriptionTextTMP != null)
        {
            float targetHeight = discriptionTextTMP.preferredHeight + 60f; // +余白分
            Vector2 currentSize = descriptionBackgroundPanelRect.sizeDelta;
            descriptionBackgroundPanelRect.sizeDelta = new Vector2(currentSize.x, targetHeight);
        }

        if (meaningBackgroundPanelRect != null && meaningTextTMP != null)
        {
            float targetHeight = meaningTextTMP.preferredHeight + 60f; // +余白分
            Vector2 currentSize = meaningBackgroundPanelRect.sizeDelta;
            meaningBackgroundPanelRect.sizeDelta = new Vector2(currentSize.x, targetHeight);
        }
    }

}
