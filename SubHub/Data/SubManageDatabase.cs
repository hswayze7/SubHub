using SQLite;
using SubHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHub.Data;

public class SubManageDatabase
{
    SQLiteAsyncConnection _connection;
    public SubManageDatabase()
    {

    }

    async Task Init()
    {
        if (_connection is not null)
        {
            return;
        }

        _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await _connection.CreateTableAsync<SubItem>();
    }

    public async Task<List<SubItem>> GetSubsAsync()
    {
        await Init();
        return await _connection.Table<SubItem>().ToListAsync();
    }

    public async Task<SubItem> GetSubAsync(int id)
    {
        await Init();
        return await _connection.Table<SubItem>().Where(i => i.userID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveSubAsync(SubItem sub)
    {
        await Init();
        if (sub.userID != 0)
        {
            return await _connection.UpdateAsync(sub);
        }
        else
        {
            return await _connection.InsertAsync(sub);
        }
    }

    public async Task<int> DeleteSubAsync(SubItem sub)
    {
        await Init();
        return await _connection.DeleteAsync(sub);
    }
}
