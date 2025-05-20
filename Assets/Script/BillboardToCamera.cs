using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // �J�����Ɠ��������ŕ��������擾�iY�����j
        Vector3 lookDir = mainCamera.transform.position - transform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f)
        {
            // ���ʕ����։�]
            Quaternion rotation = Quaternion.LookRotation(lookDir);

            // �������Œ����␳��������I
            rotation *= Quaternion.Euler(90f, 0f, 0f);

            transform.rotation = rotation;
        }
    }
}
