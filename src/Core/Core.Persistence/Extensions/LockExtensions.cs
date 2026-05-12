using MySqlConnector;

namespace Core.Persistence.Extensions;

public static class LockExtensions
{
    public static async Task<bool> WithMySqlLockAsync(
        MySqlConnection conn, string name, TimeSpan timeout, Func<Task> action, CancellationToken ct)
    {
        var acquired = Convert.ToInt32(await new MySqlCommand("SELECT GET_LOCK(@n, @t);", conn)
        {
            Parameters =
            {
                new("@n", MySqlDbType.VarChar) { Value = name },
                new("@t", MySqlDbType.Int32) { Value = (int)timeout.TotalSeconds }
            }
        }.ExecuteScalarAsync(ct)) == 1;

        if (!acquired)
            return false;

        try
        {
            await action();
            return true;
        }
        finally
        {
            await new MySqlCommand("SELECT RELEASE_LOCK(@n);", conn)
            {
                Parameters = { new("@n", MySqlDbType.VarChar) { Value = name } }
            }.ExecuteNonQueryAsync(ct);
        }
    }
}
