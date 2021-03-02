using System;
using UnityEngine;

public class AbortDialogController : BaseProvider<IAbortDialog>, IAbortDialog
{
    public bool isShowing => gameObject.activeSelf;

    public event EventHandler onAbortConfirmed;

    public event EventHandler onAbortCancelled;

    public event EventHandler onDialogHidden;

    public override IAbortDialog value => this;

    private void Awake()
    {
        hide();
    }

    public void show()
    {
        if (isShowing) return;

        gameObject.SetActive(true);
    }

    public void hide()
    {
        if (!isShowing) return;

        gameObject.SetActive(false);

        onDialogHidden?.Invoke(this, EventArgs.Empty);
    }

    public void acceptAbort()
    {
        onAbortConfirmed?.Invoke(this, EventArgs.Empty);

        hide();
    }

    public void acceptCancel()
    {
        onAbortCancelled?.Invoke(this, EventArgs.Empty);

        hide();
    }
}
