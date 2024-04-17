using BCService;

namespace ProductionLines.Client.Interfaces;
public interface IBCData
{
    Task<List<Family>> GetProductionLines();
    Task<List<ProductionOrderList>> GetProductionOrdersPerProductionLine(string No_SelectedProductionLine, DateTime productionDate);
    Task<List<FinishedProductionOrder>> GetFinishedProductionOrdersPerProductionLine(string No_SelectedProductionOrder);
    Task<List<PlannedProductionOrder>> GetPlannedProductionOrdersPerProductionLine(string No_SelectedProductionOrder);
    Task<List<FimrPlannedProdOrder>> GetFirmPlannedProductionOrdersPerProductionLine(string No_SelectedProductionOrder);
    Task<List<ReleasedProductionOrder>> GetReleasedProductionOrdersPerProductionLine(string No_SelectedProductionOrder);
    Task<List<Routing>> GetRoutingsByFamily(string RoutingNo);
    Task<List<ReleasedProductionOrder>> GetReleasedProductionOrdersPerProductionLineAndDate(string No_SelectedProductionLine, DateTime productionDate);
    Task<List<ProductionBomProdBOMLine>> GetProductionBomByProduct(string production_BOM_No);
}
