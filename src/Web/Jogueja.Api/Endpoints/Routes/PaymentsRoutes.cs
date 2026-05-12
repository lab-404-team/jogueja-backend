namespace Jogueja.Api.Endpoints.Routes
{
    internal class PaymentsRoutes
    {
        internal const string BaseUri = "v{version:apiVersion}/Payments";
        internal const string PaymentId = "PaymentId";
        internal const string SubscriptionId = "SubscriptionId";
        internal const string UserId = "UserId";
        internal const string GameId = "GameId";

        internal const string CreateGamePayment = $"{BaseUri}/game-payments";
        internal const string MarkGamePaymentPaid = $"{BaseUri}/game-payments/{{{PaymentId}:guid}}/mark-paid";
        internal const string RefundGamePayment = $"{BaseUri}/game-payments/{{{PaymentId}:guid}}/refund";
        internal const string GetGamePaymentById = $"{BaseUri}/game-payments/{{{PaymentId}:guid}}";
        internal const string ListGamePayments = $"{BaseUri}/game-payments/game/{{{GameId}:guid}}";

        internal const string CreateSubscription = $"{BaseUri}/subscriptions";
        internal const string RenewSubscription = $"{BaseUri}/subscriptions/{{{SubscriptionId}:guid}}/renew";
        internal const string CancelSubscription = $"{BaseUri}/subscriptions/{{{SubscriptionId}:guid}}/cancel";
        internal const string GetActiveSubscription = $"{BaseUri}/subscriptions/active/{{{UserId}:guid}}";
    }
}
