using System;
using UnityEngine;

public class TransactionLogger : MonoBehaviour
{
    public BaseSOProvider<ITransactionLedger> ledgerProvider;

    private void Start()
    {
        var ledger = ledgerProvider.value;

        ledger.onTransactionCommitted += printTransaction;
    }

    private void printTransaction(object sender, TransactionEventArgs e)
    {
        var transaction = e.transaction;

        Debug.Log("----- TRANSACTION COMMITTED -----");
        Debug.Log($"  Game: {transaction.gameId}/{transaction.instanceId}");
        Debug.Log($"  Wager: {transaction.wagerAmount}   Win: {transaction.winAmount}");

        foreach (var step in transaction.steps)
        {
            var result = step.adjustment == 0 ? "" : $"({step.adjustment})";
            Debug.Log($"    {step.actor}: {step.action} => {step.outcome} {result}");
        }

        Debug.Log("---------------------------------");
    }
}
