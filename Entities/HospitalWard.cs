using SQLite;
using System.Globalization;

namespace Calculator.Entities
{
    [Table("HospitalWards")]
    public class HospitalWard
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        public string? PersonInChargeName { get; set; } = null;
        public bool IsVIP { get; set; } = false;
        public int DroppersNumber { get; set; } = 0;

        public override string ToString()
        {
            return $"Hospital Ward N{Id}";
        }
    }
}
