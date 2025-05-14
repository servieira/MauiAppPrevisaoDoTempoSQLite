using System;
using MauiAppPrevisaoDoTempoSQLite.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLite;

namespace MauiAppPrevisaoDoTempoSQLite.Data
{
    public class PrevisaoTempoDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public PrevisaoTempoDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PrevisaoTempo.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            
            _database.CreateTableAsync<PesquisaPrevisaoTempo>().Wait();
        }

        public async Task<List<PesquisaPrevisaoTempo>> GetConsultasAsync()
        {
            return await _database.Table<PesquisaPrevisaoTempo>().ToListAsync();
        }
        public Task<int> SaveConsultaAsync(PesquisaPrevisaoTempo consulta)
        {
            return _database.InsertAsync(consulta);
        }

        public async Task<List<PesquisaPrevisaoTempo>> GetConsultasByDateAsync(DateTime date)
        {
            return await _database.Table<PesquisaPrevisaoTempo>()
                         .Where(p => p.DataPesquisa.Date == date.Date)
                         .ToListAsync();
        }
    }
}
