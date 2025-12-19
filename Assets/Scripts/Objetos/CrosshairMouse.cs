using UnityEngine;

public class CrosshairMouse : MonoBehaviour
{
    RectTransform rect;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.position = Input.mousePosition;
    }
}
