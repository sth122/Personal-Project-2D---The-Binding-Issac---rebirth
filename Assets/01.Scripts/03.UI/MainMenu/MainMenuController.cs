using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UICurrentState
{
    Title, FileSelect, GameMenu, CharacterSelect,
}

public class MainMenuController : MonoBehaviour
{
    #region variable
    [Header("Scroll Settings")]
    public RectTransform menuContainer;
    [SerializeField] private FileSlotUI[] fileSlots;
    [SerializeField] private RectTransform cursorIcon;
    [SerializeField] private RectTransform[] gameMenuItems;
    [SerializeField] private Image iconImage;
    [SerializeField] private float fadeDuration;
    public int fileSlotsCount;
    public int currentFileIdx = 0;
    public int gameMenuItemsCount;
    public int currentGameMenuIdx = 0;

    public float smoothTime = 0.2f;
    public float pageHeight;
    public float targetPositionY = 0f;
    private float currentVelocity = 1f;
    #endregion


    [SerializeField] private UICurrentState state;
    private IsaacInputActions inputAction;
    public StateMachine<MainMenuController> stateMachine;
    public Dictionary<UICurrentState, MainMenuState> uiStateDic = new Dictionary<UICurrentState, MainMenuState>();

    private void Awake()
    {
        state = UICurrentState.Title;

        inputAction = new IsaacInputActions();
        inputAction.UI.Submit.performed += _ => (stateMachine.CurrentState as MainMenuState)?.OnSubmit();
        inputAction.UI.Cancel.performed += _ => (stateMachine.CurrentState as MainMenuState)?.OnCancel();
        inputAction.UI.Direction1.performed += dir => (stateMachine.CurrentState as MainMenuState)?.OnNavigate(dir.ReadValue<Vector2>());


        stateMachine = new StateMachine<MainMenuController>(this);

        uiStateDic[UICurrentState.Title] = new UITitleState(this);
        uiStateDic[UICurrentState.FileSelect] = new UIFileSelectState(this);
        uiStateDic[UICurrentState.GameMenu] = new UIGameMenuState(this);
        uiStateDic[UICurrentState.CharacterSelect] = new UICharacterSelectState(this);
    }

    private void Start()
    {
        fileSlotsCount = fileSlots.Length;
        gameMenuItemsCount = gameMenuItems.Length;
    }

    private void OnEnable()
    {
        inputAction.Enable();
        stateMachine.ChangeState(uiStateDic[UICurrentState.Title]);
    }
    private void OnDisable()
    {
        inputAction.Disable();
        StopAllCoroutines();
    }


    private void Update()
    {
        HandleScrolling();
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


    public void ChangeUIState(UICurrentState nextState)
    {
        state = nextState;

        stateMachine.ChangeState(uiStateDic[nextState]);
        SoundManager.Instance.PlayPageTurnSFX();
    }

    public void UpdateFileSlotUI()
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

    public void ExecuteGameMenuAction()
    {
        if (currentGameMenuIdx == 0)
        {
            ChangeUIState(UICurrentState.CharacterSelect);
        }
        else if (currentGameMenuIdx == 1)
        {
            Debug.Log("설정 미구현");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.LogError("게임 종료");
    }

    public void UpdateCursorPosition()
    {
        if (cursorIcon == null || gameMenuItems.Length == 0) return;

        Vector2 targetPos = cursorIcon.anchoredPosition;
        targetPos.y = gameMenuItems[currentGameMenuIdx].anchoredPosition.y;
        cursorIcon.anchoredPosition = targetPos;
    }
}
