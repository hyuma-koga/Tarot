using UnityEngine;

public class TarotCardSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cardBackPrefab;
    public float radius = 5;
    public Transform cardRingTransform; // ← CardRing を Inspector にアサイン

    private TarotCardData[] cardDatas;

    public void SpawnCards()
    {
        //TarotCardDataをResourcesから読み込み
        cardDatas = Resources.LoadAll<TarotCardData>("TarotCards");

        if (cardDatas == null || cardDatas.Length == 0)
        {
            Debug.LogError("TarotCardsが読み込めませんでした。");
            return;
        }


        for (int i = 0; i < cardDatas.Length; i++)
        {
            float angleOffset = 360f / cardDatas.Length / 2f; // ← ずらし量
            float angle = (360f / cardDatas.Length) * i + angleOffset;
            float rad = angle * Mathf.Deg2Rad;

            //円周上のみに配置
            Vector3 pos = new Vector3(
                Mathf.Cos(rad) * radius,
                0f,
                Mathf.Sin(rad) * radius
            );


            //カメラ方向を向くように回転（上下立てる補正込み）
            Vector3 directionToCamera = mainCamera.transform.position - pos;
            directionToCamera.y = 0f;
            Quaternion rot = Quaternion.LookRotation(directionToCamera) * Quaternion.Euler(90f, 0f, 0f);

            GameObject card = Instantiate(cardBackPrefab, pos, rot, cardRingTransform);

            //カードに選択用スクリプトがある場合は初期化（仮想）
            var selector = card.GetComponent<CardSelector>();
            if (selector != null)
            {
                // 2引数のラムダで中継
                selector.Init(cardDatas[i], (cardData, cardObj) => OnCardSelected(cardData, cardObj));

            }
        }
    }

    /// <summary>
    /// 円形配置のポジションを算出
    /// </summary>
    Vector3 GetCirclePosition(int index, int total, float radius)
    {
        float angle = (float)index / total * Mathf.PI * 2;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0, z);
    }

    /// <summary>
    /// カード選択時の処理（仮）
    /// </summary>
    void OnCardSelected(TarotCardData selectedCard, GameObject cardObject)
    {
        // TarotGameManagerに処理を渡す
        TarotGameManager manager = FindAnyObjectByType<TarotGameManager>();
        manager?.OnCardSelected(selectedCard, cardObject);
    }
}
