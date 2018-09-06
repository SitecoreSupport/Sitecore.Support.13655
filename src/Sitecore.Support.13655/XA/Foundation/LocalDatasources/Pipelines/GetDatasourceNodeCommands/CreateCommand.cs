namespace Sitecore.Support.XA.Foundation.LocalDatasources.Pipelines.GetDatasourceNodeCommands
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Masters;
  using Sitecore.DependencyInjection;
  using Sitecore.Globalization;
  using Sitecore.Support.XA.Foundation.Geospatial;
  using Sitecore.XA.Foundation.LocalDatasources;
  using Sitecore.XA.Foundation.LocalDatasources.Pipelines.GetDatasourceNodeCommands;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.XA.Foundation.SitecoreExtensions.Repositories;
  using System.Linq;

  public class CreateCommand
  {
    public void Process(GetDataSourceNodeCommandsArgs args)
    {
      Database database = ServiceLocator.ServiceProvider.GetService<IDatabaseRepository>().GetDatabase(args.DatabaseName);
      if (database != null)
      {
        string text = ID.Parse(args.ItemId).ToString();
        Item item = database.GetItem(text);
        #region Modified code
        if (item != null && !text.Equals(Items.VirtualPageStandardValue.ToString()) && !item.InheritsFrom(Sitecore.XA.Foundation.Presentation.Templates.PartialDesignFolder.ID) && !item.IsGeospatialItem())
        #endregion
        {
          string item2 = string.Empty;
          if (item.InheritsFrom(Items.VirtualPageData) || item.InheritsFrom(Sitecore.XA.Foundation.Editing.Templates.PageDataFolder))
          {
            item2 = string.Format("<a href=\"#\" onclick=\"javascript:return scForm.postEvent(this,event,'CreateLocalDataSource(&amp;quot;{0}&amp;quot;)')\">{1}</a>", text, Translate.Text("Create"));
          }
          else if (Masters.GetMasters(item).Any())
          {
            item2 = string.Format("<a href=\"#\" onclick=\"javascript:return scForm.postEvent(this,event,'CreateDataSource(&amp;quot;{0}&amp;quot;)')\">{1}</a>", text, Translate.Text("Create"));
          }
          args.Commands.Add(item2);
        }
      }
    }
  }
}