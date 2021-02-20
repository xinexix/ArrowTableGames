using System;
using System.Collections.Generic;

public class PlayingCardDeck : ICardDeck<PlayingCard>
{
    private IReadOnlyList<PlayingCard> _cards;

    public PlayingCardDeck()
    {
        // Cache the collection as readonly
        _cards = buildDeck().AsReadOnly();
    }

    /// <remarks>
    /// A common pattern I follow is to return initialized resources rather than assign directly.
    /// Here, this allows _cards to remain private without completely blocking a derived class
    /// from impacting how this generation is done.
    /// </remarks>
    protected virtual List<PlayingCard> buildDeck()
    {
        var rankList = (CardRank[])Enum.GetValues(typeof(CardRank));
        var suitList = (CardSuit[])Enum.GetValues(typeof(CardSuit));
        var numCards = rankList.Length * suitList.Length;
        var temp = new List<PlayingCard>(numCards);

        // Iterate each suit; order doesn't matter
        foreach (var suit in suitList)
        {
            // Iterate each rank; order doesn't matter
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
    /// Factory method to support extending PlayingCard.  Seems unlikely, and I debated if it was
    /// worth the virtual method overhead, but I tend to wrap direct constructions like this.
    /// </remarks>
    protected virtual PlayingCard createCard(CardRank rank, CardSuit suit)
    {
        return new PlayingCard(rank, suit);
    }

    public IReadOnlyList<PlayingCard> cards => _cards;

    /// <remarks>
    /// The purpose of clone is we want completely separate card instances.
    /// This method is virtual to support additional constructor parameters for derived classes
    /// (for derived classes to satisfy LSP it has to be overridable anyway).
    ///
    /// It's occurred to me maybe this could be a static method, but I'd have to spend time
    /// researching the right way to hook that up.  Not worth it right now.
    /// </remarks>
    public virtual ICardDeck<PlayingCard> clone()
    {
        return new PlayingCardDeck();
    }
}
