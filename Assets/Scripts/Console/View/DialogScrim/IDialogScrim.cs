using System;

public interface IDialogScrim
{
    bool isShowing { get; }
    event EventHandler onInteracted;

    void show();
    void hide();
}
