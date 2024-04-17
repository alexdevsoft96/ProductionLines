

namespace ProductionLines.Shared.Dtos;
public class RoutingLineDto
{
    public string? OperatioNo { get; set; }
    public string? Type { get; set; }
    public string? No { get; set; }
    public string? Description { get; set; }
    public decimal? SetupTime { get; set; }
    public decimal? RunTime { get; set; }
    public decimal? FixedScrapQuantity { get; set; }
    public decimal? ScrapFactorPercentaje { get; set; }
    public string? Icon { get; set; }
}
