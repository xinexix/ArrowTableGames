using System;

public interface IToggleInput
{
    bool isVisible { get; set; }
    bool isEnabled { get; set; }
    bool isActive { get; set; }
    event EventHandler onStateChanged;
    event EventHandler onActiveInteraction;
    event EventHandler onInactiveInteraction;
}
