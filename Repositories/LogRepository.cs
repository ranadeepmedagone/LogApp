using Dapper;
using logapp.Models;
using logapp.Utilities;

namespace logapp.Repositories;

public interface ILogRepository
{
    Task<List<Log>> GetAllLogs();
    Task<Log> GetLogById(int Id);
    Task<Log> CreateLog(Log Item);
    Task<bool> UpdateLog(Log Item);
    Task DeleteLog(int Id);

    Task<List<Tag>> GetLogTagsById(int Id);

}

public class LogRepository : BaseRepository, ILogRepository
{
    public LogRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Log> CreateLog(Log Item)
    {
        var query = $@"INSERT INTO ""{TableNames.log}"" (title, description, stack_trace) 
       VALUES (@Title, @Description, @StackTrace)
       RETURNING *";


        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Log>(query, Item);
            return res;
        }
    }

    public async Task DeleteLog(int Id)
    {
        var query = $@"DELETE FROM ""{TableNames.log}"" WHERE id = @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { Id });
    }

    public async Task<List<Log>> GetAllLogs()
    {
        var query = $@"SELECT * FROM ""{TableNames.log}"" ORDER BY created_at DESC";

        using (var con = NewConnection)
            return (await con.QueryAsync<Log>(query)).AsList();
    }

    public async Task<List<Tag>> GetLogTagsById(int Id)
    {
        // SELECT * FROM tag t LEFT JOIN log_tag lt ON lt.tag_id  = t.id  where lt.log_id = 1
        var query = $@"SELECT * FROM ""{TableNames.tag}"" t LEFT JOIN ""{TableNames.log_tag}"" lt ON lt.tag_id = t.id  WHERE  lt.log_id = @Id ";

        using (var con = NewConnection)
        {
            var res = (await con.QueryAsync<Tag>(query, new { Id })).AsList();
            return res;
        }
    }

    public async Task<Log> GetLogById(int Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.log}"" WHERE id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Log>(query, new { Id });
    }

    public async Task<bool> UpdateLog(Log Item)
    {
        var query = $@"UPDATE ""{TableNames.log}"" SET description = @Description WHERE id = @Id";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);

            return rowCount == 1;
        }
    }

}