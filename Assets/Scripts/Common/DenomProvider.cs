using UnityEngine;

public class DenomProvider : BaseProvider<float>
{
    public FloatSO denomination;

    public override float value => denomination.value;
}
