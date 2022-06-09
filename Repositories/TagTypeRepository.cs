using Dapper;
using logapp.Models;
using logapp.Repositories;
using logapp.Utilities;

namespace logapp.Repositories;

public interface ITagTypeRepository
{
    Task<List<TagType>> GetAllTagTypes();
    Task<TagType> GetTagTypeById(int Id);
    Task<TagType> CreateTagType(TagType Item);
    // Task<bool> UpdateTagType(TagType Item);
    Task DeleteTagType(int Id);
    // Task<List<Log>> GetTagTypeLogsById(int Id);

    // Task<List<TagType>> GetTagTypeTagTypesById(int Id);

}

public class TagTypeRepository : BaseRepository, ITagTypeRepository
{
    public TagTypeRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<TagType> CreateTagType(TagType Item)
    {
        var query = $@"INSERT INTO ""{TableNames.tag_type}"" (name) 
       VALUES (@Name)
       RETURNING *";


        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<TagType>(query, Item);
            return res;
        }
    }

    public async Task DeleteTagType(int Id)
    {
        var query = $@"DELETE FROM ""{TableNames.tag_type}"" WHERE id = @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { Id });
    }

    public async Task<List<TagType>> GetAllTagTypes()
    {
        var query = $@"SELECT * FROM ""{TableNames.tag_type}"" ";

        using (var con = NewConnection)
            return (await con.QueryAsync<TagType>(query)).AsList();
    }

    public async Task<TagType> GetTagTypeById(int Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.tag_type}"" WHERE id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<TagType>(query, new { Id });
    }

    
    // public async Task<List<Log>> GetTagTypeLogsById(int Id)
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.log}"" l LEFT JOIN ""{TableNames.log_TagType}"" lt ON lt.TagType_id = l.id  WHERE  lt.log_id = @Id ";

    //     using (var con = NewConnection)
    //     {
    //         var res = (await con.QueryAsync<Log>(query, new { Id })).AsList();
    //         return res;
    //     }
    // }

    // public async Task<List<TagType>> GetTagTypeTagTypesById(int Id)
    // {
    //     // SELECT * FROM TagType t LEFT JOIN TagType_TagType lt ON lt.TagType_id  = t.id  where lt.TagType_id = 1
    //     var query = $@"SELECT * FROM ""{TableNames.TagType}"" t LEFT JOIN ""{TableNames.TagType_type}"" tt ON tt.id = t.id  WHERE  tt.id = @Id ";

    //     using (var con = NewConnection) 
    //     {
    //         var res = (await con.QueryAsync<TagType>(query, new { Id })).AsList();
    //         return res;
    //     }
    // }

    // public async Task<TagType> GetTagTypeById(int Id)
    // {
    //     // var query = $@"SELECT * FROM ""{TableNames.TagType}"" t LEFT JOIN ""{TableNames.TagType_type}"" tt ON tt.id = t.id WHERE t.id = @Id";
    //     var query = $@"SELECT * FROM ""{TableNames.TagType_type}"" tt LEFT JOIN ""{TableNames.TagType}"" t ON tt.id = t.id WHERE t.id = @Id";


    //     using (var con = NewConnection)
    //         return await con.QuerySingleOrDefaultAsync<TagType>(query, new { Id });
    // }

    // public async Task<bool> UpdateTagType(TagType Item)
    // {
    //     var query = $@"UPDATE ""{TableNames.tag_type}"" SET name = @Name WHERE id = @Id";


    //     using (var con = NewConnection)
    //     {
    //         var rowCount = await con.ExecuteAsync(query, Item);

    //         return rowCount == 1;
    //     }
    // }

}