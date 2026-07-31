using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private RectTransform cursorIcon;

    [SerializeField] private RectTransform[] cursorPositions;

    private bool isPaused = false;
    private int currentIndex = 0;

    private IsaacInputActions input;

    protected override void Awake()
    {
        isDDOL = false;
        base.Awake();
        input = new IsaacInputActions();
    }

    private void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    #region Input System Events
    private void OnEnable()
    {
        input.Enable();

        input.UI.Direction1.performed += OnNavigate;
        input.UI.Submit.performed += OnSubmit;
        input.UI.Cancel.performed += OnCancel;
    }

    private void OnDisable()
    {
        input.UI.Direction1.performed -= OnNavigate;
        input.UI.Submit.performed -= OnSubmit;
        input.UI.Cancel.performed -= OnCancel;

        input.Disable();
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (!isPaused) return;

        Vector2 navInput = context.ReadValue<Vector2>();

        if (navInput.y > 0.5f) // 위로 이동
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = cursorPositions.Length - 1;
            UpdateCursorPosition();
        }
        else if (navInput.y < 0.5f) // 아래로 이동
        {
            currentIndex++;
            if (currentIndex >= cursorPositions.Length) currentIndex = 0;
            UpdateCursorPosition();
        }
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            ExecuteCurrentOption();
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (isPaused) Resume();
        else Pause();
    }
    #endregion

    private void UpdateCursorPosition()
    {
        if (cursorIcon != null && cursorPositions.Length > 0)
        {
            cursorIcon.position = cursorPositions[currentIndex].position;
        }
    }

    private void ExecuteCurrentOption()
    {
        if (currentIndex == 0)
        {
            Resume();
        }
        else if (currentIndex == 1)
        {
            ExitGame();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        currentIndex = 0;
        UpdateCursorPosition();
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        IsaacSceneManager.Instance.LoadSceneWhiteFade("MainMenuScene");
    }
}