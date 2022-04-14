using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyhealthBar : MonoBehaviour
{
    public Slider Slider;
    public Color low;
    public Color Highh;
    public Vector3 offset;
    void Start()
    {
        
    }

    public void SetHealth(float health,float MaxHealth)
    {
        Slider.gameObject.SetActive(health < MaxHealth);
        Slider.value = health;
        Slider.maxValue = MaxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, Highh, Slider.normalizedValue);
    }


    // Update is called once per frame
  public  void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
