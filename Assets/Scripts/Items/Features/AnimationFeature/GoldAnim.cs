
using UnityEngine;

public class GoldAnim : MonoBehaviour
{
    public int RotateSpeed = 60;

    private void Update()
    {
        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
    }
}