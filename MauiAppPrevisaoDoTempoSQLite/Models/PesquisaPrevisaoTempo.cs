using System;
using SQLite;

namespace MauiAppPrevisaoDoTempoSQLite.Models
{
    public class PesquisaPrevisaoTempo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }        
        public string Cidade { get; set; }

        public double Temperatura { get; set; }
        public DateTime DataPesquisa { get; set; }
        public string Previsao { get; set; }
    }
}
