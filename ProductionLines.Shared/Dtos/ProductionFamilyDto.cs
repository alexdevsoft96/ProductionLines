
namespace ProductionLines.Shared.Dtos;
public class ProductionFamilyDto
{
    public string? No { get; set; }
    public string? Description { get; set; }
    public string? RoutingNo { get; set; }
    public int ItemsQuantity { get; set; }
    public decimal? TankQuantityProduced { get; set; }
    public string? MixingTankIcon { get; set; }
    public List<RoutingLineDto>? RoutingLines { get; set; }
}
