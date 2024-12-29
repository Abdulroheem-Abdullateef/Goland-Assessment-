
using Golang_Assestment_Text.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
}
