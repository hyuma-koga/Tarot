using UnityEngine;
using System;

public class CardSelector : MonoBehaviour
{
    private TarotCardData cardData;
    private Action<TarotCardData> onSelected;

    private float clickTimer = 0f;
    private int clickCount = 0;
    private const float doubleClickTime = 0.3f;
    private System.Action<TarotCardData, GameObject> onSelect;

    public void Init(TarotCardData data, System.Action<TarotCardData, GameObject> callback)
    {
        cardData = data;
        onSelect = callback;
    }

    private void OnMouseDown()
    {
        onSelect?.Invoke(cardData, this.gameObject);
    }
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        //PC�p�F�}�E�X�N���b�N�őI��
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                clickCount++;
                if (clickCount == 1) clickTimer = Time.time;
            }
        }

        if (clickCount == 2 && Time.time - clickTimer <= doubleClickTime)
        {
            onSelected?.Invoke(cardData);
            clickCount = 0;
        }
        else if (Time.time - clickTimer > doubleClickTime)
        {
            clickCount = 0;
        }
#else
        //���o�C���p�F�^�b�v�őI��
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if(Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                onSelected?.Invoke(cardData);
             }
         }
#endif
    }
}