using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UICurrentState
{
    Title, FileSelect, GameMenu
}

public class MainMenuController : MonoBehaviour
{
    [Header("Scroll Settings")]
    public RectTransform menuContainer;
    public float smoothTime = 0.2f;
    [SerializeField] private float pageHeight;
    [SerializeField] private float targetPositionY = 0f;
    private float currentVelocity = 1f;


    [SerializeField] private FileSlotUI[] fileSlots;
    [SerializeField] private int currentFileIdx = 0;

    [SerializeField] private UICurrentState state;

    private IsaacInputActions inputAction;

    [SerializeField] private RectTransform cursorIcon;
    [SerializeField] private RectTransform[] gameMenuItems;
    private int currentGameMenuIdx = 0;

    public StateMachine<MainMenuController> stateMachine;
    public Dictionary<UICurrentState, UIState> uiStateDic = new Dictionary<UICurrentState, UIState>();

    private void Awake()
    {
        state = UICurrentState.Title;
        inputAction = new IsaacInputActions();
        inputAction.UI.Submit.performed += _ => OnSubmitPressed();
        inputAction.UI.Cancel.performed += _ => OnCancelPressed();
        inputAction.UI.Direction1.performed += dir => OnNavigate(dir.ReadValue<Vector2>());

        stateMachine = new StateMachine<MainMenuController>(this);

        uiStateDic[UICurrentState.Title] = new UITitleState(this);
        uiStateDic[UICurrentState.FileSelect] = new UIFileSelectState(this);
        uiStateDic[UICurrentState.GameMenu] = new UIGameMenuState(this);
    }

    private void OnEnable()
    {
        inputAction.Enable();
        stateMachine.ChangeState(uiStateDic[UICurrentState.Title]);
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }


    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
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
            state = UICurrentState.Title;
        }
        else if (targetPositionY == pageHeight)
        {
            state = UICurrentState.FileSelect;
        }
    }

    private void OnSubmitPressed()
    {
        if (state == UICurrentState.Title)
        {
            StartCoroutine(CheckRoutine(CheckTitle, GotoNextPage));

            currentFileIdx = 0;
            UpdateFileSlotUI();
        }
        else if (state == UICurrentState.FileSelect)
        {
        }
    }
    private void OnCancelPressed()
    {
        StartCoroutine(CheckRoutine(CheckTitle,
            () =>
            {
                if (state == UICurrentState.Title) QuitGame();
                else GoToPreviousPage();
            }));
    }

    private void OnNavigate(Vector2 dir)
    {
        if (state == UICurrentState.Title) return;

        if (state == UICurrentState.FileSelect)
        {
            if (dir.x >= 0.5f)
            {
                currentFileIdx = (currentFileIdx + 1) % fileSlots.Length;
                UpdateFileSlotUI();
            }
            else if (dir.x < -0.5f)
            {
                currentFileIdx = (currentFileIdx - 1 + fileSlots.Length) % fileSlots.Length;
                UpdateFileSlotUI();
            }
        }
        else if(state == UICurrentState.GameMenu)
        {
            if (dir.y > 0.5f) // 위로 이동 (인덱스 감소)
            {
                currentGameMenuIdx = (currentGameMenuIdx - 1 + gameMenuItems.Length) % gameMenuItems.Length;
            }
            else if (dir.y < -0.5f) // 아래로 이동 (인덱스 증가)
            {
                currentGameMenuIdx = (currentGameMenuIdx + 1) % gameMenuItems.Length;
            }
        }
    }

    private void UpdateFileSlotUI()
    {
        if (fileSlots == null)
        {
            Debug.Log("fileSlots null");
        }

        for (int i = 0; i < fileSlots.Length; i++)
        {
            fileSlots[i].SetFocus(i == currentFileIdx);
        }
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
