using System;
using UnityEngine;

public class DialogScrimController : BaseProvider<IDialogScrim>, IDialogScrim
{
    public bool isShowing => gameObject.activeSelf;

    public event EventHandler onInteracted;

    public override IDialogScrim value => this;

    private void Start()
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
    }

    public void raiseInteraction()
    {
        onInteracted?.Invoke(this, EventArgs.Empty);
    }
}
