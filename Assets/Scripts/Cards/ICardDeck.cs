using System.Collections.Generic;

public interface ICardDeck<TCard>
{
    IReadOnlyList<TCard> cards { get; }

    ICardDeck<TCard> clone();
}
