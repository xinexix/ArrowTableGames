/// <summary>
/// Represents an individual traditional playing card, which has 1 of 4 possible suits and
/// 1 of 13 possible ranks.
/// </summary>
public class PlayingCard
{
    private CardRank _rank;
    private CardSuit _suit;
    private string _cachedName;

    public PlayingCard(CardRank rank, CardSuit suit)
    {
        _rank = rank;
        _suit = suit;
        _cachedName = generateName();
    }

    private string generateName()
    {
        string prefix = nameof(_rank);

        string suffix = nameof(_suit);

        return $"{prefix} of {suffix}s";
    }

    public CardRank rank => _rank;

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

    public CardType type
    {
        get
        {
            switch (_rank)
            {
                case CardRank.Ace:
                    return CardType.Ace;

                case CardRank.Jack:
                case CardRank.Queen:
                case CardRank.King:
                    return CardType.Face;

                default:
                    return CardType.Number;
            }
        }
    }

    public string name => _cachedName;

    public PlayingCard clone()
    {
        return new PlayingCard(_rank, _suit);
    }
}
