namespace FitnessApp.Resources.Model
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RecommendedReps { get; set; }
        public string RecommendedSets { get; set; }
        public string RestTime { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public bool IsSuperset { get; set; }
    }
}