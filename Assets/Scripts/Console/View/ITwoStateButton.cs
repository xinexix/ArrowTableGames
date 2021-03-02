using System;

public interface ITwoStateButton
{
    bool isVisible { get; set; }
    bool isEnabled { get; set; }
    bool activeState { get; set; }
    event EventHandler onStateChanged;
    event EventHandler onActiveInteracted;
    event EventHandler onInactiveInteracted;
}
