using System;

/// <summary>
/// The dealer shoe is the box next to the dealer at a table game which holds one or more decks
/// of cards and from which the dealer draws cards throughout gameplay.  The abstraction expects
/// all of the cards to be of a common type but any number of cards may be contained.
/// </summary>
/// <typeparam name="TCard">The type of cards which are dealt from this shoe.</typeparam>
public interface IDealerShoe<TCard>
{
    void populate(int numDecks);
    void resetAllDealt();
    void returnCards(TCard[] cards);
    void shuffleRemaining(int? seed);
    void sortRemaining(Comparison<TCard> comparison);
    TCard drawNextCard();

    int numCardsRemaining { get; }
}
