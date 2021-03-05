using System;
using UnityEngine;
using UnityEngine.UI;

public class TwoButtonToggleController : BaseProvider<IToggleInput>, IToggleInput
{
    private bool _visible = true;
    private bool _enabled = true;
    private bool _isActive = false;

    public Button activeButton;
    public Button inactiveButton;

    public event EventHandler onStateChanged;
    public event EventHandler onActiveInteraction;
    public event EventHandler onInactiveInteraction;

    public override IToggleInput value => this;

    public bool isVisible
    {
        get => _visible;
        set
        {
            if (_visible == value) return;

            _visible = value;

            updateState();

            onStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool isEnabled
    {
        get => _enabled;
        set
        {
            if (_enabled == value) return;

            _enabled = value;

            updateState();

            onStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool isActive
    {
        get => _isActive;
        set
        {
            if (_isActive == value) return;

            _isActive = value;

            updateState();

            onStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Start()
    {
        updateState();
    }

    private void updateState()
    {
        if (activeButton != null)
        {
            activeButton.interactable = isEnabled;
            activeButton.gameObject.SetActive(isVisible && isActive);
        }

        if (inactiveButton != null)
        {
            inactiveButton.interactable = isEnabled;
            inactiveButton.gameObject.SetActive(isVisible && !isActive);
        }
    }

    public void handleActiveInteracted()
    {
        onActiveInteraction?.Invoke(this, EventArgs.Empty);
    }

    public void handleInactiveInteracted()
    {
        onInactiveInteraction?.Invoke(this, EventArgs.Empty);
    }
}
