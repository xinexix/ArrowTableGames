using System;
using UnityEngine;

/// <remarks>
/// There is some problem with "en-US" not being supported?  Haven't figured this out yet so
/// for the sake of time I'm simply serving up an instance of the more specialized
/// UsdCurrencyFormatter.  Ideally that formatter wouldn't exist.  Also, WTF is with string
/// comparison in C#?
/// </remarks>
[CreateAssetMenu(menuName = "Script Objects/Currency Formatter")]
public class CurrencyFormatterSO : BaseSOProvider<ICurrencyFormatter>
{
    private ICurrencyFormatter _formatter;

    public FloatSO denomination;
    public string cultureName;

    public override ICurrencyFormatter value
    {
        get
        {
            /// <remarks>
            /// It's been a while since I've dealt with the singleton pattern in C#;
            /// I vaguely recall there was a better method for thread safety...but
            /// I don't think that is an issue in Unity?
            /// </remarks>
            if (_formatter == null)
            {
                _formatter = new UsdCurrencyFormatter(denomination.value);
                // _formatter = new CultureCurrencyFormatter(denomination.value, cultureName);
            }

            return _formatter;
        }
    }
}
