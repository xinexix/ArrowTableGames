using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/Game ID")]
public class GameIdSO : BaseSOProvider<string>
{
    [SerializeField]
    private string _gameId;

    public override string value => _gameId;
}
