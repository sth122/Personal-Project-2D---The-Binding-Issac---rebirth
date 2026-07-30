using System;
using System.Collections;
using Unity.VisualScripting;
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
    private bool isAtTilte = true;
    private bool isAtFileSelect = false;

    private IsaacInputActions inputAction;

    private float currentVelocity = 1f;

    private void Awake()
    {
        inputAction = new IsaacInputActions();
        inputAction.UI.Submit.performed += _ => OnSubmitPressed();
        inputAction.UI.Cancel.performed += _ => OnCancelPressed();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Update()
    {
        HandleScrolling();
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

    public void GotoNextPage()
    {
        targetPositionY += pageHeight;
        CheckTitle();
    }

    public void GoToPreviousPage()
    {
        targetPositionY -= pageHeight;
        CheckTitle();
    }

    private void CheckTitle()
    {
        if (targetPositionY == 0f)
        {
            isAtTilte = true;
            isAtFileSelect = false;
        }
        else if (targetPositionY == pageHeight)
        {
            isAtTilte = false;
            isAtFileSelect = true;
        }
    }

    private void OnSubmitPressed()
    {
        StartCoroutine(CheckRoutine(CheckTitle, GotoNextPage));
    }
    private void OnCancelPressed()
    {
        StartCoroutine(CheckRoutine(CheckTitle,
            () =>
            {
                if (isAtTilte) QuitGame();
                else GoToPreviousPage();
            }));
    }

    private IEnumerator CheckRoutine(Action onCheck, Action onNext)
    {
        onCheck?.Invoke();
        yield return null;
        onNext?.Invoke();
    }
    private void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.LogError("게임 종료");
    }

}
