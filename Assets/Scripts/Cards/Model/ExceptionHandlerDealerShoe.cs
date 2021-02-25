using System;
using System.Runtime.CompilerServices;

/// <remarks>
/// This is the decorator pattern.  This a) separates the concerns of error handling from the
/// desired functionality, and b) allows for different error handling (such as events that'd
/// trigger a popup instead of throwing an exception).
/// </remarks>
public class ExceptionHandlerDealerShoe<TCard> : IDealerShoe<TCard>
{
    private IDealerShoe<TCard> _subComponent;
    private bool _hasCards = false;

    public ExceptionHandlerDealerShoe(IDealerShoe<TCard> subComponent)
    {
        _subComponent = subComponent;
    }

    public int numCardsRemaining => _subComponent.numCardsRemaining;

    public void clearShoe()
    {
        _subComponent.clearShoe();
        _hasCards = false;
    }

    public void addDeck(ICardDeck<TCard> referenceDeck)
    {
        _subComponent.addDeck(referenceDeck);
        _hasCards = _subComponent.numCardsRemaining > 0;
    }

    public void returnAllDealt()
    {
        validateHasCards();

        _subComponent.returnAllDealt();
    }

    /// <remarks>
    /// According to StackOverflow, using this parameter attribute has very little overhead.
    /// </remarks>
    private void validateHasCards([CallerMemberName] string method = "?")
    {
        if (!_hasCards)
        {
            throw new InvalidOperationException(
                $"IDealerShoe.{method} called while the shoe is empty");
        }
    }

    public void returnCards(TCard[] cards)
    {
        validateHasCards();

        if (cards == null || cards.Length == 0)
        {
            throw new ArgumentException("IDealerShoe.returnCards called with no cards to return");
        }

        _subComponent.returnCards(cards);
    }

    public void shuffleRemaining(int? seed)
    {
        validateHasCards();

        _subComponent.shuffleRemaining(seed);
    }

    public void sortRemaining(Comparison<TCard> comparison)
    {
        validateHasCards();

        _subComponent.sortRemaining(comparison);
    }

    public TCard drawNextCard()
    {
        validateHasCards();

        if (_subComponent.numCardsRemaining == 0)
        {
            throw new InvalidOperationException(
                "IDealerShoe.drawNextCard called with no cards remaining to deal");
        }

        return _subComponent.drawNextCard();
    }
}
