using System;
using System.Collections.Generic;

public class DealerShoe<TCard> : IDealerShoe<TCard>
{
    private IRng _rng;
    private List<TCard> _availableCards = new List<TCard>();
    private List<TCard> _dealtCards = new List<TCard>();

    public DealerShoe(IRng rng)
    {
        _rng = rng;
    }

    public int numCardsRemaining => _availableCards.Count;

    /// <remarks>
    /// Because of the use of TimeExcess, this approach might be problematic for a  long-running
    /// instance such as a bar machine.  However in this project I don't expect this method to
    /// be utilized much and so don't expect significant performance concerns.
    /// </remarks>
    public void clearShoe()
    {
        _availableCards.Clear();
        _dealtCards.Clear();

        // Reset capacities to the default
        _availableCards.TrimExcess();
        _dealtCards.TrimExcess();
    }

    public void addDeck(ICardDeck<TCard> referenceDeck)
    {
        var uniqueDeck = referenceDeck.clone();

        _availableCards.AddRange(uniqueDeck.cards);
    }

    public void returnAllDealt()
    {
        _availableCards.AddRange(_dealtCards);
        _dealtCards.Clear();

        // Reset the capacity to the default and allow it to grow as necessary
        _dealtCards.TrimExcess();
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

            var swapIndex = rng.randomInRange(0, walkIndex);

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
