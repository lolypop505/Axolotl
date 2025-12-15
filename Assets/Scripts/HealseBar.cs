using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealseBar : MonoBehaviour
{
    public Image healsthBarSpriteYellow;
    public Image healthBarSpriteRed;

    private float target = 1;
    public float reduceSpeed = 2;

    public void UpdateHealthBar(float maxHealth, float health)
    {
        healthBarSpriteRed.fillAmount = health / maxHealth;
        target = health / maxHealth;
    }

    void Update()
    {
        healsthBarSpriteYellow.fillAmount = Mathf.MoveTowards(healsthBarSpriteYellow.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
