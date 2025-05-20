using UnityEngine;

public class CardRingRotator : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private float currentRotation = 0f;

    private void Update()
    {
        //マウスのドラッグ or スマホのスワイプで回転
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            currentRotation += (-mouseX) * rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }
}
