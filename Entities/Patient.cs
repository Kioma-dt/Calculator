using SQLite;

namespace Calculator.Entities
{
    [Table("Patients")]
    public class Patient
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public int Age { get; set; } = 0;
        public string? Diagnoses { get; set; } = null;

        [Indexed]
        public int HospitalWardId { get; set; }
    }
}
