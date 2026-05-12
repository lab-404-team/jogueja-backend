namespace Core.Application.Template;

public interface IEmailTemplate
{
    string GetSubject(Dictionary<string, string>? subjectParameters = null);
    string GetHtmlString();
}

public class EmailTemplate : IEmailTemplate
{
    protected EmailTemplate(string path, string subject = "")
    {
        FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        HtmlString = File.ReadAllText(FilePath);
        Subject = subject;
    }

    private string FilePath { get; }
    private string HtmlString { get; }
    private string Subject { get; }

    public string GetHtmlString() => HtmlString;

    public string GetSubject(Dictionary<string, string>? subjectParameters = null)
    {
        var result = Subject;
        if (subjectParameters is not null)
        {
            foreach (var parameter in subjectParameters)
                result = result.Replace(parameter.Key, parameter.Value);
        }
        return result;
    }
}
