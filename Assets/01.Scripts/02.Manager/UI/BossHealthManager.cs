using UnityEngine;
using UnityEngine.UI;
public class BossHealthManager : Singleton<BossHealthManager>
{
    [SerializeField] private GameObject bossHealthPanel;
    [SerializeField] private Image healthFillImage;

    private float currenMaxHP = 1f;

    protected override void Awake()
    {
        isDDOL = false;
        base.Awake();
    }

    private void Start()
    {
        if(bossHealthPanel != null)
            bossHealthPanel.SetActive(false);
    }


    public void ShowBossHealthBar(float maxHP)
    {
        currenMaxHP = maxHP;
        healthFillImage.fillAmount = 1f;
        bossHealthPanel.SetActive(true);
    }

    public void UpdateBossHealth(float currentHP)
    {
        healthFillImage.fillAmount = Mathf.Clamp01(currentHP / currenMaxHP);
    }

    public void HideBossHealthBar()
    {
        bossHealthPanel.SetActive(false);
    }
}
