using Esri.ArcGISRuntime.Mapping;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MapTest.ViewModels
{
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

    public IPageDialogService GetDialogService()
    {
      return dialogService;
    }

    public void SetDialogService(IPageDialogService value)
    {
      dialogService = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public MapPageViewModel(IPageDialogService dialogService)
    {
      this.Map = new Map();
      this.SetDialogService(dialogService);
      AddLayers();
    }

    private void AddLayers()
    {
      try
      {
        this.Map.Basemap = Basemap.CreateTopographic();
        AgregarCapa("http://sampleserver5.arcgisonline.com/arcgis/rest/services/Elevation/WorldElevations/MapServer", "dynamic");
        MostrarMensaje("MapTest", "Finish");
      }
      catch (Exception ex)
      {
        MostrarMensaje("Exception", ex.Message);
      }
    }

    private void AgregarCapa(string url, string tipo)
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
        MostrarMensaje("MapTest", "layer Added.");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="titulo"></param>
    /// <param name="mensaje"></param>
    private void MostrarMensaje(string titulo, string mensaje)
    {
      System.Diagnostics.Debug.WriteLine(mensaje);
      Device.BeginInvokeOnMainThread(async () =>
      {
        var result = await this.GetDialogService().DisplayAlertAsync(
          titulo,
          mensaje,
          "Aceptar",
          "Cancelar");
      });
    }


  }
}
