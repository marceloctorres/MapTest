using Esri.ArcGISRuntime.Mapping;
using Prism.Mvvm;
using Prism.Services;
using System;
using Xamarin.Forms;

/// <summary>
/// 
/// </summary>
namespace MapTest.ViewModels
{
  /// <summary>
  /// 
  /// </summary>
  public class MapPageViewModel : BindableBase
  {

    private Map _map;

    /// <summary>
    /// 
    /// </summary>
    public Map Map
    {
      get { return _map; }
      set { SetProperty(ref _map, value); }
    }

    private IPageDialogService dialogService;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IPageDialogService GetDialogService()
    {
      return dialogService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetDialogService(IPageDialogService value)
    {
      dialogService = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public MapPageViewModel(IPageDialogService dialogService)
    {
      Map = new Map();
      SetDialogService(dialogService);
      AddLayers();
    }

    /// <summary>
    /// 
    /// </summary>
    private void AddLayers()
    {
      try
      {
        Map.Basemap = Basemap.CreateTopographic();
        AddLayer("http://sampleserver5.arcgisonline.com/arcgis/rest/services/Elevation/WorldElevations/MapServer", "dynamic");
        ShowAlert("MapTest", "Finish");
      }
      catch (Exception ex)
      {
        ShowAlert("Exception", ex.Message);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="tipo"></param>
    private void AddLayer(string url, string tipo)
    {
      Layer layer = null;
      if (tipo == "tiled")
      {
        layer = new ArcGISTiledLayer(new Uri(url));
      }
      else if (tipo == "dynamic")
      {
        layer = new ArcGISMapImageLayer(new Uri(url));
      }

      try
      {
        this.Map.OperationalLayers.Add(layer);
        ShowAlert("MapTest", "layer Added.");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    private void ShowAlert(string title, string message)
    {
      System.Diagnostics.Debug.WriteLine(message);
      Device.BeginInvokeOnMainThread(async () =>
      {
        var result = await this.GetDialogService().DisplayAlertAsync(
          title,
          message,
          "Aceptar",
          "Cancelar");
      });
    }


  }
}
