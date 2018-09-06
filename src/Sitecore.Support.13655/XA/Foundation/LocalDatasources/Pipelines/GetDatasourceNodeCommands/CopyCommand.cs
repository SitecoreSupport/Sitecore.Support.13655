namespace Sitecore.Support.XA.Foundation.LocalDatasources.Pipelines.GetDatasourceNodeCommands
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Abstractions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.DependencyInjection;
  using Sitecore.Globalization;
  using Sitecore.XA.Foundation.LocalDatasources.Pipelines.GetDatasourceNodeCommands;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.XA.Foundation.SitecoreExtensions.Repositories;
  using System.Collections.Generic;
  using System.Linq;

  public class CopyCommand
  {
    private IEnumerable<ID> _excludeItems;

    protected IEnumerable<ID> ExcludeItems
    {
      get
      {
        if (_excludeItems == null)
        {
          LazyResetable<BaseFactory> requiredResetableService = ServiceLocator.GetRequiredResetableService<BaseFactory>();
          _excludeItems = from s in requiredResetableService.Value.GetStringSet("experienceAccelerator/datasourceCopyCommand/excludedTemplates/*")
                          select new ID(s);
        }
        return _excludeItems;
      }
    }

    public void Process(GetDataSourceNodeCommandsArgs args)
    {
      if (ShouldExecute(ID.Parse(args.ItemId)))
      {
        Database database = ServiceLocator.ServiceProvider.GetService<IDatabaseRepository>().GetDatabase(args.DatabaseName);
        ID iD = ID.Parse(args.ItemId);
        Item item = database.GetItem(iD);
        if (item != null && !item.InheritsFrom(Sitecore.XA.Foundation.Presentation.Templates.PartialDesignFolder.ID) && !item.InheritsFrom(Sitecore.XA.Foundation.Presentation.Templates.PartialDesign.ID) && !item.Parent.InheritsFrom(Sitecore.XA.Foundation.Multisite.Templates.Data.ID))
        {
          string item2 = string.Format("<a href=\"#\" onclick=\"javascript:return scForm.postEvent(this,event,'CopyDataSource(&amp;quot;{0}&amp;quot;)')\">{1}</a>", iD, Translate.Text("Copy To"));
          args.Commands.Add(item2);
        }
      }
    }

    protected virtual bool ShouldExecute(ID itemId)
    {
      return !ExcludeItems.Contains(itemId);
    }
  }
}