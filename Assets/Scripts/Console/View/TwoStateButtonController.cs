using System;
using UnityEngine;
using UnityEngine.UI;

public class TwoStateButtonController : BaseProvider<ITwoStateButton>, ITwoStateButton
{
    private bool _visible = true;
    private bool _enabled = true;
    private bool _activeState = false;

    public Button activeButton;
    public Button inactiveButton;

    public event EventHandler onStateChanged;
    public event EventHandler onActiveInteracted;
    public event EventHandler onInactiveInteracted;

    public override ITwoStateButton value => this;

    public bool isVisible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;

                if (_visible)
                {
                    activeButton?.gameObject.SetActive(_activeState);
                    inactiveButton?.gameObject.SetActive(!_activeState);
                }
                else
                {
                    activeButton?.gameObject.SetActive(false);
                    inactiveButton?.gameObject.SetActive(false);
                }

                onStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool isEnabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;

                if (activeButton != null)
                {
                    activeButton.interactable = value;
                }

                if (inactiveButton != null)
                {
                    inactiveButton.interactable = value;
                }

                onStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool activeState
    {
        get => _activeState;
        set
        {
            if (_activeState != value)
            {
                _activeState = value;

                activeButton?.gameObject.SetActive(_activeState);
                inactiveButton?.gameObject.SetActive(!_activeState);

                onStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void Awake()
    {
        if (activeButton != null)
        {
            activeButton.interactable = _enabled;
            activeButton.gameObject.SetActive(_activeState);
        }

        if (inactiveButton != null)
        {
            inactiveButton.interactable = _enabled;
            inactiveButton.gameObject.SetActive(!_activeState);
        }
    }

    public void handleActiveInteracted()
    {
        onActiveInteracted?.Invoke(this, EventArgs.Empty);
    }

    public void handleInactiveInteracted()
    {
        onInactiveInteracted?.Invoke(this, EventArgs.Empty);
    }
}
