using System;
using System.Collections.Generic;

public class DealerShoe<TCard> : IDealerShoe<TCard>
{
    private IRng _rng;
    private ICardDeck<TCard> _referenceDeck;
    private List<TCard> _availableCards = new List<TCard>();
    private List<TCard> _dealtCards = new List<TCard>();

    public DealerShoe(IRng rng, ICardDeck<TCard> referenceDeck)
    {
        _rng = rng;
        _referenceDeck = referenceDeck;
    }

    public int numCardsRemaining => _availableCards.Count;

    public void populate(int numDecks)
    {
        // Release any previous instances
        /// <remarks>
        /// Given C#s generational garbage collection, I expect this would be a bad implementation
        /// for performance on a long-running machine, but I didn't want to manage a count of each
        /// specific card so this implementation felt reasonable here
        /// </remarks>
        releaseCards();

        // We obtain separate deck instances to have separate card instances
        for (var i = 0; i < numDecks; i++)
        {
            var tempDeck = _referenceDeck.clone();

            _availableCards.AddRange(tempDeck.cards);
        }

        trimCards();
    }

    private void releaseCards()
    {
        _availableCards.Clear();
        _dealtCards.Clear();
    }

    private void trimCards()
    {
        // Reduce the capacity to just what is needed
        _availableCards.TrimExcess();

        // Reset the capacity to the default and allow it to grow as necessary
        /// <remarks>
        /// Again, this might be unwise for a long-running game; possible memory fragmentation?
        /// </remarks>
        _dealtCards.TrimExcess();
    }

    public void resetAllDealt()
    {
        _availableCards.AddRange(_dealtCards);
        _dealtCards.Clear();
    }

    public void returnCards(TCard[] cards)
    {
        foreach (var card in cards)
        {
            // Uses reference equality to find and extract the card from the list
            var wasDealt = _dealtCards.Remove(card);

            // Only insert to available pool if it had been dealt
            if (wasDealt)
            {
                _availableCards.Add(card);
            }
        }
    }

    public void shuffleRemaining(int? seed)
    {
        // Set the RNG seed if one was provided
        if (seed.HasValue)
        {
            _rng.setSeed(seed.Value);
        }

        shuffleInPlace(_rng, _availableCards);
    }

    private void shuffleInPlace(IRng rng, List<TCard> list)
    {
        var walkIndex = list.Count;
        while (walkIndex > 1)
        {
            walkIndex--;

            var swapIndex = rng.randomIntRange(0, walkIndex);

            /// <remarks>
            /// C# 7 tuple trick to swap without an explicit temp...kinda ugly
            /// </remarks>
            (list[walkIndex], list[swapIndex]) = (list[swapIndex], list[walkIndex]);
        }
    }

    public void sortRemaining(Comparison<TCard> comparison)
    {
        _availableCards.Sort(comparison);
    }

    public TCard drawNextCard()
    {
        if (_availableCards.Count == 0)
        {
            return default(TCard);
        }

        var card = _availableCards[0];
        _availableCards.RemoveAt(0);
        _dealtCards.Add(card);

        return card;
    }
}
