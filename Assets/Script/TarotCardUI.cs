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
    public RectTransform cardRootTransform; //��]�p�e�I�u�W�F�N�g
    public GameObject backToTitleButton;
    public GameObject descriptionBackgroundPanel;  // �� �w�i�p�l����Inspector����A�T�C��
    public GameObject meaningBackgroundPanel;  // �� �w�i�p�l����Inspector����A�T�C��

    public RectTransform meaningBackgroundPanelRect; 
    public TextMeshProUGUI meaningTextTMP; 
    public RectTransform descriptionBackgroundPanelRect; 
    public TextMeshProUGUI discriptionTextTMP; 


    /// <summary>
    /// �^���b�g�J�[�h�̕\��\��
    /// </summary>
    public void DisplayCard(TarotCardData cardData, bool showFront = true)
    {
        if (cardData == null)
        {
            Debug.LogWarning("�J�[�h�f�[�^������܂���");
            return;
        }

        if (showFront)
        {
            cardImage.sprite = cardData.illustration;
            cardNameText.text = $"{cardData.cardName}";
            imageNameText.text = $"{cardData.imageName}";
            meaningText.text = cardData.isReversed
                ? $"�t�ʒu�F{cardData.meaningReversed}"
                : $"���ʒu�F{cardData.meaningUpright}";
            discriptionText.text = cardData.isReversed
               ? $"{cardData.discriptionReversed}"
               : $"{cardData.discriptionUpright}";

            //�摜���㉺���]
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
    /// �J�[�h�̗��ʂ�\���i����J���̂Ƃ��Ȃǁj
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
    /// �J�[�h�̕\�ʂ̂ݍX�V�i�\���ςݑO��j
    /// </summary>
    public void ShowCardFront(TarotCardData cardData)
    {
        Debug.Log("ShowCardFront ���Ă΂�܂����I");
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
            descriptionBackgroundPanel.SetActive(false);  // �w�i�p�l����\��
        }

        if (meaningBackgroundPanel != null)
        {
            meaningBackgroundPanel.SetActive(false);  // �w�i�p�l����\��
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
            descriptionBackgroundPanel.SetActive(true);  // �w�i�p�l����\��
        }

        if (meaningBackgroundPanel != null)
        {
            meaningBackgroundPanel.SetActive(true);  // �w�i�p�l����\��
        }

        if (descriptionBackgroundPanelRect != null && discriptionTextTMP != null)
        {
            float targetHeight = discriptionTextTMP.preferredHeight + 60f; // +�]����
            Vector2 currentSize = descriptionBackgroundPanelRect.sizeDelta;
            descriptionBackgroundPanelRect.sizeDelta = new Vector2(currentSize.x, targetHeight);
        }

        if (meaningBackgroundPanelRect != null && meaningTextTMP != null)
        {
            float targetHeight = meaningTextTMP.preferredHeight + 60f; // +�]����
            Vector2 currentSize = meaningBackgroundPanelRect.sizeDelta;
            meaningBackgroundPanelRect.sizeDelta = new Vector2(currentSize.x, targetHeight);
        }
    }

}
