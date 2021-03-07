using System;

public interface IAbortDialog
{
    bool isShowing { get; }
    event EventHandler onAbortConfirmed;
    event EventHandler onAbortCancelled;
    event EventHandler onDialogHidden;

    void show();
    void hide();
}
