using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Scroll Settings")]
    public RectTransform menuContainer;
    public float smoothTime = 1f;
    public float pageHeight = 1000f;
    private float targetPositionY = 0f;
    public bool isAtTilte = true;

    private float currentVelocity = 1f;

    private void Update()
    {
        HandleScrolling();


        if (isAtTilte)
        {
            CheckInpuForNextPage();
        }
    }

    private void HandleScrolling()
    {
        if (menuContainer != null)
        {
            Vector2 currentPos = menuContainer.anchoredPosition;
            float newY = Mathf.SmoothDamp(currentPos.y, targetPositionY, ref currentVelocity, smoothTime);
            menuContainer.anchoredPosition = new Vector2(currentPos.x, newY);
        }
    }

    private void CheckInpuForNextPage()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            GotoNextPage();
        }
    }
    public void GotoNextPage()
    {
        isAtTilte = false;
        targetPositionY = pageHeight;
    }
}
