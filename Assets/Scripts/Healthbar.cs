using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public bool enemy = true;
    public Slider slider;
    public Vector3 offset;
    public Color low;
    public Color high;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy)
        {
            slider.transform.position = camera.WorldToScreenPoint(transform.parent.position + offset);
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (enemy)
            slider.gameObject.SetActive(health < maxHealth);
        
        slider.maxValue = maxHealth;
        slider.value = health;
        slider.fillRect.GetComponent<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}
