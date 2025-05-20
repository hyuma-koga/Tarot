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
        // カメラと同じ高さで方向だけ取得（Y無視）
        Vector3 lookDir = mainCamera.transform.position - transform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f)
        {
            // 正面方向へ回転
            Quaternion rotation = Quaternion.LookRotation(lookDir);

            // ★ここで直立補正を加える！
            rotation *= Quaternion.Euler(90f, 0f, 0f);

            transform.rotation = rotation;
        }
    }
}
