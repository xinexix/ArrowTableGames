using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Float Variable")]
public class FloatSO : BaseSOProvider<float>
{
    [SerializeField]
    private float _value;

    public override float value => _value;
}
