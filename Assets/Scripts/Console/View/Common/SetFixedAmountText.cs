using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SetFixedAmountText : MonoBehaviour
{
    public BaseSOProvider<ICurrencyFormatter> currencyFormatterProvider;
    public int fixedValue;

    private void Start()
    {
        var formatter = currencyFormatterProvider.value;

        var label = GetComponent<TMP_Text>();

        label.text = formatter.format(fixedValue);
    }
}
