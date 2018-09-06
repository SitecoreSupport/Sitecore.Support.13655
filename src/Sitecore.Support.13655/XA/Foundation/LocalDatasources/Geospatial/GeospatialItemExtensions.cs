using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Support.XA.Foundation.Geospatial
{
  public static class GeospatialItemExtensions
  {
    public static bool IsGeospatialItem(this Item item)
    {
      return item.InheritsFrom(SupportTemplates.IPoi.ID) ||
             item.InheritsFrom(SupportTemplates.MyLocationPoi.ID) ||
             item.InheritsFrom(SupportTemplates.PoiGroup.ID) ||
             item.InheritsFrom(SupportTemplates.PoiGroupingItem.ID);
    }
    public static class SupportTemplates
    {
      public struct IPoi
      {
        public static ID ID = ID.Parse("{7F018744-BC34-4A42-A380-C7259E744843}");
        public struct Fields
        {
          public static ID Latitude { get; } = new ID("{94969C59-C4B8-43ED-9C92-3FBF930C0ACF}");
          public static ID Longitude { get; } = new ID("{8C1AEA63-2E5E-4FEE-B413-D3B1E050DD26}");
        }
      }
      public struct MyLocationPoi
      {
        public static ID ID = ID.Parse("{7DD9ECE5-9461-498D-8721-7CBEA8111B5E}");

        public struct Fields
        {
          public static ID Title { get; } = new ID("{A9C02F55-E9D8-4D6B-9A7E-021794B3C6DE}");
          public static ID Type { get; } = new ID("{07AAC6E6-06F7-4AB9-9F16-A7D060C8A8A3}");
          public static ID Description { get; } = new ID("{BC03D1BC-7743-464F-90C3-C9E1542FD8BC}");
          public static ID Image { get; } = new ID("{971D6D7B-41C5-472A-91B3-876BA5CE89E3}");
          public static ID PoiPage { get; } = new ID("{6A2B140E-FCB5-46E7-AE9A-FB4061B3DAB0}");
        }
      }
      public struct PoiGroup
      {
        public static ID ID = ID.Parse("{9716FAFC-C305-4C95-A92B-0C0DA8714634}");
      }

      public struct PoiGroupingItem
      {
        public static ID ID = ID.Parse("{8CA8B80D-A1AD-4B45-9F08-8839357E71D4}");
      }
    }

  }
}