using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    [SerializeField]
    private bool enemy = true;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Color low;
    [SerializeField]
    private Color high;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy)
            slider.transform.position = mainCamera.WorldToScreenPoint(transform.parent.position + offset);
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
