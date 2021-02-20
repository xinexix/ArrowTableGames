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

    public string name => _cachedName;

    public PlayingCard clone()
    {
        return new PlayingCard(_rank, _suit);
    }
}
