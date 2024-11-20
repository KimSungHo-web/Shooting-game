using UnityEngine;

public interface IInteractable
{
    // 상호작용 가능한 오브젝트 정보 텍스트
    public string GetInteractPrompt();

    public void OnInteract();

    public void Flash();
}
