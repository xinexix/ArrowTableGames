using System;
using UnityEngine;

public class ControlStripController : BaseProvider<IControlStrip>, IControlStrip
{
    public BaseProvider<IToggleInput> lobbyToggleProvider;
    public BaseProvider<IToggleInput> bankingToggleProvider;
    public BaseProvider<IStepInput> betStepProvider;
    public BaseProvider<IToggleInput> autoBetToggleProvider;
    public BaseProvider<IToggleInput> soundToggleProvider;

    public override IControlStrip value => this;

    public int temp => 0;
}
