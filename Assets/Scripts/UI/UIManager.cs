using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainPanel;
    public GameObject rePanel;
    public GameObject healthPanel;
    public GameObject DoorPanel;
    public Sprite targetSprite;
    private Image[] healthImages;
    private int currentHealthIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        if (healthPanel != null)
        {
            healthImages = healthPanel.GetComponentsInChildren<Image>();
            currentHealthIndex = 0;
        }
    }

    void OnEnable()
    {
        EventHandler.PlayerDieEvent += () => rePanel.SetActive(true);
        EventHandler.PlayerRebornEvent += ResetHealth;
    }

    void OnDisable()
    {
        EventHandler.PlayerDieEvent -= () => rePanel.SetActive(true);
        EventHandler.PlayerRebornEvent -= ResetHealth;
    }

    public void RePlayer()
    {
        rePanel.SetActive(false);
        EventHandler.CallPlayerRebornEvent();
    }

    public void StartGame()
    {
        EventHandler.CallStartNewGameEvent(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPlayerHurt()
    {
        if (healthPanel != null)
        {
            healthPanel.SetActive(true);

            if (healthImages == null)
            {
                healthImages = healthPanel.GetComponentsInChildren<Image>();
            }

            if (healthImages != null && healthImages.Length > 0 && targetSprite != null)
            {
                if (currentHealthIndex < healthImages.Length)
                {
                    healthImages[currentHealthIndex].sprite = targetSprite;
                    currentHealthIndex++;
                }
            }
            DOVirtual.DelayedCall(1f, () =>
            {
                if (healthPanel != null)
                {
                    healthPanel.SetActive(false);
                }
            });
        }
    }

    public void ResetHealth()
    {
        currentHealthIndex = 0;
    }

    public void EndGame()
    {
        EventHandler.CallEndGameEvent();
    }
}
