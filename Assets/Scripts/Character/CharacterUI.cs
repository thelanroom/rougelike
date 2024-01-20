using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    public void UpdateHealthBar(float ratio)
    {
        healthBar.fillAmount = ratio;
        healthBar.color = ratio < 0.2f ? Color.red : Color.green;
    }

    private void Update()
    {
        healthBar.transform.LookAt(Camera.main.transform.position); 
    }
}
