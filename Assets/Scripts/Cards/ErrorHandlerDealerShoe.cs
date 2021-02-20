using System;
using System.Runtime.CompilerServices;

/// <remarks>
/// It'd be more appropriate to name this ExceptionHandlerDealerShoe, but my UML is getting a bit
/// wide...
///
/// This is the decorator pattern.  This a) separates the concerns of error handling from the
/// actual work, and b) allows for different error handling (such as events that'd trigger a
/// popup instead of throwing an exception).
/// </remarks>
public class ErrorHandlerDealerShoe<TCard> : IDealerShoe<TCard>
{
    private IDealerShoe<TCard> _subComponent;
    private bool _isInitialized = false;

    public ErrorHandlerDealerShoe(IDealerShoe<TCard> subComponent)
    {
        _subComponent = subComponent;
    }

    public int numCardsRemaining => _subComponent.numCardsRemaining;

    public void populate(int numDecks)
    {
        _isInitialized = true;
        _subComponent.populate(numDecks);
    }

    public void resetAllDealt()
    {
        validateInit();

        _subComponent.resetAllDealt();
    }

    /// <remarks>
    /// According to StackOverflow, using this parameter attribute has very little overhead.
    /// </remarks>
    private void validateInit([CallerMemberName] string method = "?")
    {
        if (!_isInitialized)
        {
            throw new InvalidOperationException(
                $"IDealerShoe.{method} called before being populated");
        }
    }

    public void returnCards(TCard[] cards)
    {
        validateInit();

        if (cards == null || cards.Length == 0)
        {
            throw new ArgumentException("IDealerShoe.returnCards called with no cards");
        }

        _subComponent.returnCards(cards);
    }

    public void shuffleRemaining(int? seed)
    {
        validateInit();

        _subComponent.shuffleRemaining(seed);
    }

    public void sortRemaining(Comparison<TCard> comparison)
    {
        validateInit();

        _subComponent.sortRemaining(comparison);
    }

    public TCard drawNextCard()
    {
        validateInit();

        if (_subComponent.numCardsRemaining == 0)
        {
            throw new InvalidOperationException(
                "IDealerShoe.drawNextCard called when no cards are remaining");
        }

        return _subComponent.drawNextCard();
    }
}
