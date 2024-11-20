using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private float _checkRate = 0.05f;
    private float _checkTime;
    private float _checkDistance = 10.0f;
    private LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        layerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Time.time - _checkTime > _checkRate)
        {
            _checkTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _checkDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(hit.collider.transform);
                }
            }
            else
            {
                if (curInteractGameObject != null)
                {
                    curInteractGameObject = null;
                    curInteractable = null;
                    DisablePromptText();  // 텍스트 비활성화
                }
            }
        }
    } 

    // 해당 게임 오브젝트의 텍스트를 활성화/비활성화하는 함수
    private void SetPromptText(Transform targetTransform)
    {
        // 타겟 오브젝트의 World Space Canvas를 찾아서 텍스트를 표시
        var worldSpaceCanvas = targetTransform.GetComponentInChildren<Canvas>();

        if (worldSpaceCanvas != null)
        {
            TextMeshProUGUI promptText = worldSpaceCanvas.GetComponentInChildren<TextMeshProUGUI>();

            if (curInteractable != null)
            {
                promptText.text = curInteractable.GetInteractPrompt();
                promptText.gameObject.SetActive(true);

                // 텍스트 위치 및 크기 조정
                AdjustTextPositionAndSize(promptText, targetTransform);
            }
        }
    }

    // 텍스트를 비활성화하는 함수
    private void DisablePromptText()
    {
        if (curInteractGameObject != null)
        {
            var worldSpaceCanvas = curInteractGameObject.GetComponentInChildren<Canvas>();
            if (worldSpaceCanvas != null)
            {
                TextMeshProUGUI promptText = worldSpaceCanvas.GetComponentInChildren<TextMeshProUGUI>();
                if (promptText != null)
                {
                    promptText.gameObject.SetActive(false);  // 텍스트 비활성화
                }
            }
        }
    }

    private void AdjustTextPositionAndSize(TextMeshProUGUI promptText, Transform targetTransform)
    {
        // 텍스트 위치를 오브젝트 위로 설정 (World Space에서 적절한 위치)
        promptText.rectTransform.position = targetTransform.position + new Vector3(0, 2.0f, 0);  // 오브젝트 위로 2 단위

        // 텍스트 크기 조정 (폰트 크기 변경)
        promptText.fontSize = 24;  // 폰트 크기를 24로 설정 (적절히 조정 가능)

        // 텍스트의 정렬 및 배치 방식 설정 (가운데 정렬 등)
        promptText.alignment = TextAlignmentOptions.Center;  // 텍스트 가운데 정렬
    }

    public void OnInteract()
    {
        if (curInteractable != null)
        {
            curInteractable.OnInteract();  // 상호작용 실행
        }
    }
}
