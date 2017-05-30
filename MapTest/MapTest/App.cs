using MapTest.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTest
{
  /// <summary>
  /// 
  /// </summary>
  public class App : PrismApplication
  {
    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
      NavigationService.NavigateAsync("MapPage");
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void RegisterTypes()
    {
      Container.RegisterTypeForNavigation<MapPage>();
    }
  }
}
