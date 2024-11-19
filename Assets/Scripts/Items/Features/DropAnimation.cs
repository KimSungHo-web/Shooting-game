using Defines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimation : MonoBehaviour
{
    public float baseJumpHeight = 2.0f;
    public float baseDropTime = 1.0f;
    public Vector3 randomDropOffset = new Vector3(1.5f, 0f, 1.5f);
    public Vector3 itemscale = new Vector3(3f, 3f, 3f);

    private float _jumpHeight;
    private float _dropTime;
    private Light _light;

    private Vector3 _targetPosition;

    public void StartDropAnimation(Vector3 originPosition,Rarity rarity)
    {
        _targetPosition = originPosition + new Vector3(
            Random.Range(-randomDropOffset.x, randomDropOffset.x),
            0,
            Random.Range(-randomDropOffset.z, randomDropOffset.z)
        );

        switch (rarity)
        {
            case Rarity.Common:
                _jumpHeight = baseJumpHeight;
                _dropTime = baseDropTime;
                //��ƼŬ �߰��ϱ� �� �ӽ�
                _light = gameObject.AddComponent<Light>();
                _light.color = Color.white;
                _light.intensity = 5f;
                _light.range = 1f;
                break;
            case Rarity.Uncommon:
                _jumpHeight = baseJumpHeight * 1.2f;
                _dropTime = baseDropTime * 1.2f;
                break;
            case Rarity.Rare:
                _jumpHeight = baseJumpHeight * 1.5f;
                _dropTime = baseDropTime * 1.5f;
                break;
            case Rarity.Epic:
                _jumpHeight = baseJumpHeight * 1.8f;
                _dropTime = baseDropTime * 1.8f;
                break;
            case Rarity.Legendary:
                _jumpHeight = baseJumpHeight * 2.0f;
                _dropTime = baseDropTime * 2.0f;
                break;
            default:
                _jumpHeight = baseJumpHeight;
                _dropTime = baseDropTime;
                break;
        }

        transform.localScale = itemscale;

        

        StartCoroutine(AnimateDrop(originPosition));
    }

    private IEnumerator AnimateDrop(Vector3 originPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = originPosition;

        while (elapsedTime < _dropTime)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / _dropTime;
            float height = Mathf.Sin(Mathf.PI * t) * _jumpHeight;

            Vector3 currentPosition = Vector3.Lerp(startPosition, _targetPosition, t);
            currentPosition.y += height;

            transform.position = currentPosition;

            transform.Rotate(Vector3.right * 360 * Time.deltaTime);
            transform.Rotate(Vector3.up * 360 * Time.deltaTime);

            yield return null;
        }

        transform.position = _targetPosition;
    }
}
