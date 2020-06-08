namespace CoronavirusTracking.Dtos
{
    public class GenerationDto
    {
        public double LatFrom { get; set; } = 1;
        public double LatTo { get; set; } = 2;
        public double LongFrom { get; set; } = 1;
        public double LongTo { get; set; } = 2;
        public int StepInSeconds { get; set; } = 1;
        public int LengthInSeconds { get; set; } = 100;
        public int UsersQuantity { get; set; } = 10;
        public double StepLength { get; set; } = 5;
        public int ContactQuantity { get; set; }
    }
}
