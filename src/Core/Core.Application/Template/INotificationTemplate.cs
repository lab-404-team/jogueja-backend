namespace Core.Application.Template;

public interface INotificationTemplate { }

public class NotificationTemplate : INotificationTemplate
{
    protected NotificationTemplate(string title, string message, string actionUrl)
    {
        Title = title;
        Message = message;
        ActionUrl = actionUrl;
    }

    public string Title { get; }
    public string Message { get; }
    public string ActionUrl { get; }
}
