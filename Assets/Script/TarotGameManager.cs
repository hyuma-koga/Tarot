using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TarotGameManager : MonoBehaviour
{
    public TarotCardUI cardUI;
    public TarotCardData[] allCards;
    public Button revealButton;    //表裏表示ボタン
    public Transform cardRingTransform; //CardRing（全カードの親）
    private GameObject selectedCardObject;
    private TarotCardData drawnCard;
    private bool hasCardBeenSelected = false;
    private Vector3 cameraInitialPosition;
    private Quaternion cameraInitialRotation;


    private void Awake()
    {
        allCards = Resources.LoadAll<TarotCardData>("TarotCards");
        Debug.Log($"自動読み込み完了！合計: {allCards.Length} 枚");
        // nullが入っていた場合を除外（保険）
        allCards = System.Array.FindAll(allCards, card => card != null);
    }

    private void Start()
    {
        if (revealButton != null)
        {
            revealButton.gameObject.SetActive(false);
            revealButton.onClick.AddListener(RevealCard);
            cameraInitialPosition = Camera.main.transform.position;
            cameraInitialRotation = Camera.main.transform.rotation;
        }

        // 説明UIを非表示
        if (cardUI != null)
        {
            cardUI.HideDetails();
        }
    }

    public void OnCardSelected(TarotCardData selectedCard, GameObject cardObject)
    {

        drawnCard = selectedCard;

        if (hasCardBeenSelected) return; //多重選択防止
        hasCardBeenSelected = true;

        //ここで逆位置を設定
        selectedCard.isReversed = Random.value > 0.5f;

        //他のカードを非表示にする
        foreach (Transform child in cardRingTransform)
        {
            if (child.gameObject != cardObject)
            {
                child.gameObject.SetActive(false);
            }
        }

        //選ばれたカードの挙動を停止
        var selector = cardObject.GetComponent<CardSelector>();
        if(selector != null)
        {
            selector.enabled = false;
        }


        StartCoroutine(MoveCardToCenter(cardObject, selectedCard));
    }
    private IEnumerator MoveCardToCenter(GameObject card, TarotCardData cardData)
    {
        // 理想のTransformに基づいて設定
        Vector3 targetPos = new Vector3(0f, 2.269f, -9.6f);
        Quaternion targetRot = Quaternion.Euler(90f, 0f, -180f);  // 正面向き

        Camera.main.transform.position = new Vector3(0f, 2.3f, -13.42f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = card.transform.position;
        Quaternion startRot = card.transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            card.transform.position = Vector3.Lerp(startPos, targetPos, t);
            card.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        //Revealボタンを表示
        if (revealButton != null)
        {
            revealButton.gameObject.SetActive(true);
            revealButton.interactable = true;
        }


        selectedCardObject = card; // 最後に保持
    }

    public void RevealCard()
    {
        Debug.Log("RevealCard() が呼ばれました！");
        if (drawnCard == null)
        {
            Debug.LogWarning("drawnCard が null です！");
            return;
        }
        if (selectedCardObject != null)
        {
            Destroy(selectedCardObject);
            selectedCardObject = null; // 念のため
        }

        cardUI.ShowCardFront(drawnCard);
        cardUI.ShowDetails(); // ← ここで表示！
        ApplyCardEffect(drawnCard);

        // ボタン自体を削除
        if (revealButton != null)
        {
            revealButton.gameObject.SetActive(false);
        }

        //結果の背景を表示
        FindAnyObjectByType<TarotUIManager>()?.ShowResultScreen();
    }

    private void ApplyCardEffect(TarotCardData card)
    {
        //演出（音・エフェクト）もここで再生可能
        if (card.cardSound != null)
        {
            AudioSource.PlayClipAtPoint(card.cardSound, Vector3.zero);
        }

        if (card.visualEffectPrefab != null)
        {
            Instantiate(card.visualEffectPrefab, Vector3.zero, Quaternion.identity);
        }
    }


    public void SetSelectedCard(TarotCardData card)
    {
        drawnCard = card;
        cardUI.DisplayCard(card, false); // 裏面表示

        if (revealButton != null)
        {
            revealButton.gameObject.SetActive(true);
            revealButton.interactable = true;
        }
    }

    public void ResetState()
    {
        hasCardBeenSelected = false;
        drawnCard = null;
        selectedCardObject = null;

        if(revealButton != null)
        {
            revealButton.gameObject.SetActive(false);
        }

        if(cardUI != null)
        {
            cardUI.HideDetails();
        }
    }

    public void ResetCameraPosition()
    {
        Camera.main.transform.position = cameraInitialPosition;
        Camera.main.transform.rotation = cameraInitialRotation;
    }




}
