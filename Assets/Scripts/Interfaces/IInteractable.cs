using UnityEngine;

public interface IInteractable
{
    // 상호작용 가능한 오브젝트 정보 텍스트
    public string GetInteractPrompt();
    // 상호작용 가능한 오브젝트를 클릭했을때 실행되는 함수
    public void OnInteract(Transform target);
    // 상호작용 가능한 경우 깜박이게 하기위한 함수
    public void Flash();
}
