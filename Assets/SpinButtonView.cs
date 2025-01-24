using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    public event Action Clicked;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke();
    }

    public void SetInteractable(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }
}
