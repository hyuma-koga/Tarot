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
    public TMP_Text effectText;
    public TMP_Text imageNameText;
    public RectTransform cardRootTransform; //回転用親オブジェクト
    public GameObject backToTitleButton;

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
            effectText.text = $"効果:{cardData.effectDescription}";

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
        effectText.text = "";
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
        effectText.gameObject.SetActive(false);

        if(backToTitleButton != null)
        {
            backToTitleButton.SetActive(false);
        }
    }

    public void ShowDetails()
    {
        cardImage.gameObject.SetActive(true);
        cardNameText.gameObject.SetActive(true);
        imageNameText.gameObject.SetActive(true);
        meaningText.gameObject.SetActive(true);
        effectText.gameObject.SetActive(true);

        if(backToTitleButton != null)
        {
            backToTitleButton.SetActive(true);
        }
    }

}
