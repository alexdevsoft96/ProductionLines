using BCService;
using Microsoft.OData.Client;
using ProductionLines.Client.Data;
using ProductionLines.Client.Interfaces;

namespace ProductionLines.Client.Services;

public class BCDataService:IBCData
{
    private readonly Uri uri;
    private readonly NAV context;
    public BCDataService()
    {
        uri = new Uri(HttpConstants.BaseAddress);
        context = new NAV(uri);
        context.HttpRequestTransportMode = HttpRequestTransportMode.HttpClient;
        context.BuildingRequest += Context_BuildingRequest;
    }
    public async Task<List<Family>> GetProductionLines()
    {
        IEnumerable<Family> tempProductionLines = await context.Family
            .Expand("FamilyControl13")
            .ExecuteAsync();
        List<Family>productionLines = tempProductionLines.ToList();
        return productionLines;
    }
    public async Task<List<ProductionOrderList>> GetProductionOrdersPerProductionLine(string No_SelectedProductionLine, DateTime productionDate)
    {
        DateTime StartDate = productionDate.AddDays(-5);
        DateTime EndDate = productionDate.AddDays(5);   

        IEnumerable<ProductionOrderList> tempProductionOrders = await context.ProductionOrderList
       .AddQueryOption("$filter", $"Source_No eq '{No_SelectedProductionLine}'")
       .ExecuteAsync();
        //return tempProductionOrders.Where(x=>x.Starting_Date== productionDate.Date).ToList();
        //var res = tempProductionOrders.Where(x=>x.Starting_Date>= StartDate.Date && x.Starting_Date<=EndDate.Date).ToList(); 
         return tempProductionOrders.Where(x => x.Starting_Date >= StartDate.Date && x.Starting_Date <= EndDate.Date).ToList();
    }
    public async Task<List<FinishedProductionOrder>> GetFinishedProductionOrdersPerProductionLine(string No_SelectedProductionOrder)
    {
        IEnumerable<FinishedProductionOrder> tempFinishedProductionOrders = await context.FinishedProductionOrder
        .Expand("FinishedProductionOrderProdOrderLines")
        .AddQueryOption("$filter", $"No eq '{No_SelectedProductionOrder}'")
        .ExecuteAsync();

        return  tempFinishedProductionOrders.ToList();
    }
    public async Task<List<PlannedProductionOrder>> GetPlannedProductionOrdersPerProductionLine(string No_SelectedProductionOrder)
    {
        IEnumerable<PlannedProductionOrder> tempPlannedProductionOrders = await context.PlannedProductionOrder
        .Expand("PlannedProductionOrderProdOrderLines")
        .AddQueryOption("$filter", $"No eq '{No_SelectedProductionOrder}'")
        .ExecuteAsync();
        
        return tempPlannedProductionOrders.ToList();
    }
    public async Task<List<FimrPlannedProdOrder>> GetFirmPlannedProductionOrdersPerProductionLine(string No_SelectedProductionOrder)
    {
        IEnumerable<FimrPlannedProdOrder> tempFirmPlannedProductionOrders = await context.FimrPlannedProdOrder
        .Expand("FimrPlannedProdOrderProdOrderLines")
        .AddQueryOption("$filter", $"No eq '{No_SelectedProductionOrder}'")
        .ExecuteAsync();
        
        return tempFirmPlannedProductionOrders.ToList();
    }
    public async Task<List<ReleasedProductionOrder>> GetReleasedProductionOrdersPerProductionLine(string No_SelectedProductionOrder)
    {
        IEnumerable<ReleasedProductionOrder> tempReleasedProductionOrders = await context.ReleasedProductionOrder
        .Expand("ReleasedProductionOrderProdOrderLines")
        .AddQueryOption("$filter", $"No eq '{No_SelectedProductionOrder}'")
        .ExecuteAsync();
        
        return tempReleasedProductionOrders.ToList();
    }
    
    //routes
    public async Task<List<Routing>> GetRoutingsByFamily(string RoutingNo)
    {
        IEnumerable<Routing> routings = await context.Routing
        .Expand("RoutingRoutingLine")
        .AddQueryOption("$filter", $"No eq '{RoutingNo}'")
        .ExecuteAsync();

        return routings.ToList();
    }

    public async Task<List<ReleasedProductionOrder>> GetReleasedProductionOrdersPerProductionLineAndDate(string No_SelectedProductionLine, DateTime productionDate)
    {
        DateTime StartDate = productionDate.AddDays(-5);
        DateTime EndDate = productionDate.AddDays(5);

        IEnumerable<ReleasedProductionOrder> tempReleasedProductionOrders = await context.ReleasedProductionOrder
        .Expand("ReleasedProductionOrderProdOrderLines")
        .AddQueryOption("$filter", $"Source_No eq '{No_SelectedProductionLine}'")
        .ExecuteAsync();

        return tempReleasedProductionOrders.Where(x => x.Starting_Date >= StartDate.Date && x.Starting_Date <= EndDate.Date).ToList();
    }
    public async Task<List<ProductionBomProdBOMLine>> GetProductionBomByProduct(string production_BOM_No)
    {
        IEnumerable<ProductionBomProdBOMLine> tempProductionBomByProduct = await context.ProductionBomProdBOMLine
       .AddQueryOption("$filter", $"Production_BOM_No eq '{production_BOM_No}'")
       .ExecuteAsync();

        return tempProductionBomByProduct.ToList();
    }
    private void Context_BuildingRequest(object? sender, BuildingRequestEventArgs e)
    {
        e.Headers.Add("Authorization", "Basic YWRtaW46RjdYWUh4bUNrMzlhMUlwL0tHR3R5NWROdlJwdTduTExSbDlWNUhjeFRIZz0=");
    }
}
