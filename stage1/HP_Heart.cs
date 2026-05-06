using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Heart : MonoBehaviour
{ 
    [SerializeField] private Image hpHeart;

    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (hpHeart == null)
            hpHeart = GetComponent<Image>();

        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateUI();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpHeart.fillAmount = (float)currentHealth / maxHealth;
    }
}
