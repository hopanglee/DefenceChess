using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion _initialRotation;

    void Start()
    {
        // 초기 로컬 회전값 저장
        _initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // 부모의 회전 영향을 제거하고 초기 로컬 회전값 유지
        transform.rotation = _initialRotation;
    }
}