using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public float CurrentHealth =1f;
    public static UIHandler instance { get; private set; }
    VisualElement fullBar;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        fullBar = uiDocument.rootVisualElement.Q<VisualElement>("healthBar");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealthValue(float health)
    {
        CurrentHealth = health / 10f;
        fullBar.style.width = Length.Percent(CurrentHealth * 100.0f);
    }
}
