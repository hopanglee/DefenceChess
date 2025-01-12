using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GridPositionable))]
public class GridMovable : MonoBehaviour
{
    private GridPositionable m_gridPositionable;
    public bool isMoving = false;

    private void Awake()
    {
        m_gridPositionable = GetComponent<GridPositionable>();
    }

    public void MoveTo(Unit unit, int speed)
    {
        var node = PathFinder.GetNextNode(m_gridPositionable.gridPosition, unit.GetPosition());

        if (node == null) return;

        // 해당 node로 이동
        isMoving = true;
        m_gridPositionable.SetGridPosition(node.pos); // 노드 위치 미리 이동한걸로 설정해두기.

        // 이동 시작
        StartCoroutine(MoveCoroutine(node, speed));
    }

    private IEnumerator MoveCoroutine(Node node, int speed)
    {
        float moveSpeed = (float)speed / 100f;
        Vector3 targetPosition = node.transform.position;

        // 바라볼 방향 계산
        Vector3 direction = (targetPosition - transform.position).normalized;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 방향 회전
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime * 5f); // 부드러운 회전
            }
            yield return null; // 다음 프레임까지 대기
        }

        transform.position = targetPosition; // 정확히 목표 지점에 위치
        isMoving = false; // 이동 완료
    }


}
