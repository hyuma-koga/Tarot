using UnityEngine;

public class TarotCardSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cardBackPrefab;
    public float radius = 5;
    public Transform cardRingTransform; // �� CardRing �� Inspector �ɃA�T�C��

    private TarotCardData[] cardDatas;

    public void SpawnCards()
    {
        //TarotCardData��Resources����ǂݍ���
        cardDatas = Resources.LoadAll<TarotCardData>("TarotCards");

        if (cardDatas == null || cardDatas.Length == 0)
        {
            Debug.LogError("TarotCards���ǂݍ��߂܂���ł����B");
            return;
        }


        for (int i = 0; i < cardDatas.Length; i++)
        {
            float angleOffset = 360f / cardDatas.Length / 2f; // �� ���炵��
            float angle = (360f / cardDatas.Length) * i + angleOffset;
            float rad = angle * Mathf.Deg2Rad;

            //�~����݂̂ɔz�u
            Vector3 pos = new Vector3(
                Mathf.Cos(rad) * radius,
                0f,
                Mathf.Sin(rad) * radius
            );


            //�J���������������悤�ɉ�]�i�㉺���Ă�␳���݁j
            Vector3 directionToCamera = mainCamera.transform.position - pos;
            directionToCamera.y = 0f;
            Quaternion rot = Quaternion.LookRotation(directionToCamera) * Quaternion.Euler(90f, 0f, 0f);

            GameObject card = Instantiate(cardBackPrefab, pos, rot, cardRingTransform);

            //�J�[�h�ɑI��p�X�N���v�g������ꍇ�͏������i���z�j
            var selector = card.GetComponent<CardSelector>();
            if (selector != null)
            {
                // 2�����̃����_�Œ��p
                selector.Init(cardDatas[i], (cardData, cardObj) => OnCardSelected(cardData, cardObj));

            }
        }
    }

    /// <summary>
    /// �~�`�z�u�̃|�W�V�������Z�o
    /// </summary>
    Vector3 GetCirclePosition(int index, int total, float radius)
    {
        float angle = (float)index / total * Mathf.PI * 2;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0, z);
    }

    /// <summary>
    /// �J�[�h�I�����̏����i���j
    /// </summary>
    void OnCardSelected(TarotCardData selectedCard, GameObject cardObject)
    {
        // TarotGameManager�ɏ�����n��
        TarotGameManager manager = FindAnyObjectByType<TarotGameManager>();
        manager?.OnCardSelected(selectedCard, cardObject);
    }
}
