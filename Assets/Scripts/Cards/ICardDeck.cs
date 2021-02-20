using System.Collections.Generic;

/// <summary>
/// Abstraction for an arbitrary deck of a specific type of cards.  This is really a special,
/// read-only collection and as such doesn't expose much functionality.  Its purpose is to
/// provide references to the cards in the collection.
/// </summary>
/// <typeparam name="TCard">The type of cards contained within the deck.</typeparam>
public interface ICardDeck<TCard>
{
    IReadOnlyList<TCard> cards { get; }

    ICardDeck<TCard> clone();
}
