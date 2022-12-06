namespace BasketballAppSoftuni.DTOs.PlayerDTOs
{
    public class PlayerFullInfoDTO
    {
        public string FullName { get; set; }
        public double PointsPerGame { get; set; }
        public double AssistsPerGame { get; set; }
        public double ReboundsPerGame { get; set; }
        public string PictureURL { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public string Salary { get; set; }
        public string TeamLogoUrl { get; set; }
        public int TeamId { get; set;}
    }
}
