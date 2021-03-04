using System;

public interface IStepInput
{
    bool isVisible { get; set; }
    bool isEnabled { get; set; }
    event EventHandler onStateChanged;
    event EventHandler onIncreaseInteraction;
    event EventHandler onDecreaseInteraction;
}
