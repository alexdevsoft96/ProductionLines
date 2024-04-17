using BCService;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ProductionLines.Client.Interfaces;
using ProductionLines.Client.Layout.Modal;
using ProductionLines.Shared.Dtos;
using Radzen;

namespace ProductionLines.Client.Pages;
public partial class Home
{
    [Inject] public IJSRuntime js { get; set; } = default!;
    [Inject] public NavigationManager navigation { get; set; } = default!;
    [Inject] public IModalService modalService { get; set; } = default!;
    [Inject] public IBCData _bCDataService { get; set; } = default!;        

    public List<Family>? productionLines;
    public List<ProductionFamilyDto> productionFamilies = new List<ProductionFamilyDto>();
    Random rnd = new Random();
    Variant variant = Variant.Text;

    protected override async Task OnInitializedAsync()
    {
        await GetProductionLinesWithRouting();
    }
    private async Task GetProductionLinesWithRouting()
    {
        productionLines = await _bCDataService.GetProductionLines();
        foreach (var product in productionLines)
        {
            ProductionFamilyDto productionFamily = new ProductionFamilyDto();
            productionFamily.No = product.No;
            productionFamily.Description = product.Description;
            productionFamily.RoutingNo = product.Routing_No;
            if (product.FamilyControl13 is not null)
            {
                productionFamily.ItemsQuantity = product.FamilyControl13.Count();
                productionFamily.TankQuantityProduced= product.FamilyControl13.Sum(x => x.Quantity);
                productionFamily.MixingTankIcon = "./icons/mixing_tank.svg";
            }
            List<Routing> routings = await _bCDataService.GetRoutingsByFamily(productionFamily.RoutingNo);

            List<RoutingLineDto> routesLines = new List<RoutingLineDto>();
            foreach (var route in routings)
            {
                foreach (var routeLine in route.RoutingRoutingLine)
                {
                    string pathIcon = $"./icons/machine" + rnd.Next(1, 7) + ".svg";
                    RoutingLineDto routingLine = new RoutingLineDto();
                    routingLine.OperatioNo = routeLine.Operation_No;
                    routingLine.Type = routeLine.Type;
                    routingLine.No = routeLine.No;
                    routingLine.Description = routeLine.Description;
                    routingLine.SetupTime = routeLine.Setup_Time;
                    routingLine.RunTime = routeLine.Run_Time;
                    routingLine.Icon = pathIcon;
                    routesLines.Add(routingLine);
                }
            }
            productionFamily.RoutingLines = routesLines;
            productionFamilies.Add(productionFamily);
        }
    }
    private void ShowRoutingLines(RoutingLineDto route)
    {
        ModalParameters parameters = new ModalParameters();
        parameters.Add("SelectedRoutingLine", route);
        ModalOptions options = new ModalOptions()
        {
            HideCloseButton = false,
            DisableBackgroundCancel = true,
            AnimationType = ModalAnimationType.FadeInOut
        };
        IModalReference formModal = modalService.Show<ModalSelectedRoutingLine>(route.Description!, parameters, options);
    }
    private void ShowOrdersPerProductionLine(ProductionFamilyDto productionLine)
    {
        ModalParameters parameters = new ModalParameters();
        parameters.Add("SelectedProductionLine", productionLine);
        ModalOptions options = new ModalOptions()
        {
            HideCloseButton = false,
            DisableBackgroundCancel = true,
            AnimationType = ModalAnimationType.FadeInOut
        };
        IModalReference formModal = modalService.Show<ModalSelectedProductionLine>(productionLine.Description!, parameters, options);
    }
}
