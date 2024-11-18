using UnityEngine;

public interface IInteractable
{
    // ��ȣ�ۿ� ������ ������Ʈ ���� �ؽ�Ʈ
    public string GetInteractPrompt();
    // ��ȣ�ۿ� ������ ������Ʈ�� Ŭ�������� ����Ǵ� �Լ�
    public void OnInteract(Transform target);
    // ��ȣ�ۿ� ������ ��� �����̰� �ϱ����� �Լ�
    public void Flash();
}
