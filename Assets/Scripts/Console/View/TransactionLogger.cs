using System.Text;
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
        var sb = new StringBuilder();
        var transaction = e.transaction;

        sb.Append("----- TRANSACTION COMMITTED -----");
        sb.Append($"\n> Game: {transaction.gameId}     ID: {transaction.instanceId}");
        sb.Append($"\n> Wager: {transaction.wagerAmount}     Win: {transaction.winAmount}");

        foreach (var step in transaction.steps)
        {
            var result = step.adjustment == 0 ? "" : $"({step.adjustment})";
            sb.Append($"\n>   {step.actor}: {step.action} => {step.outcome} {result}");
        }

        sb.Append("\n---------------------------------");

        Debug.Log(sb.ToString());
    }
}
