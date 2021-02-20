public class CardValue
{

    private int _rank;
    private CardSuit _suit;
    private string _cachedName;

    public CardValue(int rank, CardSuit suit)
    {
        _rank = rank;
        _suit = suit;
        _cachedName = generateName();
    }

    public int rank => _rank;

    public CardSuit suit => _suit;

    public CardColor color
    {
        get
        {
            switch (_suit)
            {
                case CardSuit.Heart:
                case CardSuit.Diamond:
                    return CardColor.Red;

                default:
                    return CardColor.Black;
            }
        }
    }

    public string name => _cachedName;

    private string generateName()
    {
        if (_rank < 1 || rank > 13)
        {
            return "Cthulhu of Lovecraft";
        }

        string prefix;

        if (_rank >= 2 && _rank <= 10)
        {
            prefix = _rank.ToString();
        }
        else
        {
            switch (_rank)
            {
                case 11:
                    prefix = "J";
                    break;
                case 12:
                    prefix = "Q";
                    break;
                case 13:
                    prefix = "K";
                    break;
                case 1:
                default:
                    prefix = "A";
                    break;
            }
        }

        string suffix = nameof(_suit);

        return $"{prefix} of {suffix}s";
    }
}
