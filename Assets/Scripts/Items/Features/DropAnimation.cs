using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimation : MonoBehaviour
{
    public float jumpHeight = 2.0f;
    public float dropTime = 1.0f;
    public Vector3 randomDropOffset = new Vector3(1.5f, 0f, 1.5f);
    public Vector3 itemscale = new Vector3(2f, 2f, 2f);

    private Vector3 _targetPosition;

    public void StartDropAnimation(Vector3 originPosition)
    {
        _targetPosition = originPosition + new Vector3(
            Random.Range(-randomDropOffset.x, randomDropOffset.x),
            0,
            Random.Range(-randomDropOffset.z, randomDropOffset.z)
        );

        transform.localScale = itemscale;

        StartCoroutine(AnimateDrop(originPosition));
    }

    private IEnumerator AnimateDrop(Vector3 originPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = originPosition;

        while (elapsedTime < dropTime)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / dropTime;
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;

            Vector3 currentPosition = Vector3.Lerp(startPosition, _targetPosition, t);
            currentPosition.y += height;

            transform.position = currentPosition;

            yield return null;
        }

        transform.position = _targetPosition;
    }
}
