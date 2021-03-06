using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Bet Steps List")]
public class BetStepsListSO : BaseSOProvider<List<int>>
{
    [SerializeField]
    private List<int> _betSteps;

    public override List<int> value => _betSteps;
}
