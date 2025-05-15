using System;
using SQLite;

namespace PrevisaoDoTempoApp.Models
{
    public class PrevisaoTempo
    {
        public int Id { get; set; }
        public string Cidade { get; set; }
        public double Temperatura { get; set; }
        public double temp { get; set; }

        public DateTime Data { get; set; }
        public string Previsao { get; set; }
        
    }
}