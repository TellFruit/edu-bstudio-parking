namespace CoolParking.WebAPI.Models
{
    public class ApiTopUp
    {
        public int Id { get; }
        
        public string Sum { get; }

        public ApiTopUp(int id, string sum)
        {
            Id = id;
            Sum = sum;
        }
    }
}
