namespace CSharpProject.Models
{
    public class StatisticData
    {
        private List<DateTime> _coursedates;
        public List<DateTime> coursedates
        {
            get => _coursedates;
            set => _coursedates = value;
        }


        public Course course { get; set; }
        public int count { get; set; }



        
        
    }
}
