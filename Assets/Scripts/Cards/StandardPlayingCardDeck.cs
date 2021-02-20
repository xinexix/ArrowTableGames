using System;
using System.Collections.Generic;

public class StandardPlayingCardDeck : ICardDeck<CardValue>
{
    private IReadOnlyList<CardValue> _cards;

    public StandardPlayingCardDeck()
    {
        // Cache the collection as readonly
        _cards = buildDeck().AsReadOnly();
    }

    protected virtual List<CardValue> buildDeck()
    {
        var rankList = (CardRank[])Enum.GetValues(typeof(CardRank));
        var suitList = (CardSuit[])Enum.GetValues(typeof(CardSuit));
        var numCards = rankList.Length * suitList.Length;
        var temp = new List<CardValue>(numCards);

        // Iterate each suit; order doesn't matter
        foreach (var suit in suitList)
        {
            foreach (var rank in rankList)
            {
                // Create a card for each rank in each suit
                var card = createCard(rank, suit);

                temp.Add(card);
            }
        }

        return temp;
    }

    /// <remarks>
    /// Factory method to support extending CardValue.  Seems unlikely, and I debated if it was
    /// worth the virtual method overhead, but I tend to wrap direct constructions like this.
    /// </remarks>
    protected virtual CardValue createCard(CardRank rank, CardSuit suit)
    {
        return new CardValue(rank, suit);
    }

    public IReadOnlyList<CardValue> cards => _cards;

    /// <remarks>
    /// The purpose of clone is we want completely separate card instances.
    /// This method is virtual to support additional constructor parameters for derived classes
    /// (to be effective it has to be overridable anyway).
    ///
    /// It's occurred to me maybe this could be a static method, but I'd have to spend time
    /// researching the right way to hook that up.  Not worth it right now.
    /// </remarks>
    public virtual ICardDeck<CardValue> clone()
    {
        return new StandardPlayingCardDeck();
    }
}
