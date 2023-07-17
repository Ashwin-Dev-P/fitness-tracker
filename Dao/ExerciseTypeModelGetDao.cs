namespace fitt.Dao
{
    public class ExerciseTypeModelGetDao
    {
        public int ExerciseTypeId { get; set; }


        public required string Name { get; set; }


        public string? Description { get; set; }

        public string? ImageExtension { get; set; }
    }
}
