using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : Singleton<HealthUIManager>
{
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private Transform heartContainer;

    [SerializeField] private List<Image> heartImages = new List<Image>();

    protected override void Awake()
    {
        isDDOL = false;
        base.Awake();
    }

    public void UpdateHealthUI(float currentHP, float maxHP)

    {
        int maxHeartContainters = Mathf.CeilToInt(maxHP / 2f);
        int currentHealthPoints = Mathf.CeilToInt(currentHP);

        for (int i = 0; i < heartImages.Count; i++)
        {
            if(i >=  maxHeartContainters)
            {
                heartImages[i].color = new Color(1, 1, 1, 0);
                continue;
            }

            heartImages[i].color = new Color(1, 1, 1, 1);

            int heartValue = i * 2;

            if(currentHealthPoints >= heartValue + 2)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else if(currentHealthPoints == heartValue + 1)
            {
                heartImages[i].sprite = halfHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }

        }
    }
}
