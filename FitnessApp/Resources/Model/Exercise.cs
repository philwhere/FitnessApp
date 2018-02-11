namespace FitnessApp.Resources.Model
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RecommendedReps { get; set; }
        public int RecommendedSets { get; set; }
        public string RestTime { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public bool IsSuperset { get; set; }
    }
}