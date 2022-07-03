using Zeus.Models.Base.Commands;

namespace Zeus.Models.Locations.Commands
{
   public sealed class UpdateLocationCommand : BaseUpdateCommand
   {
      public string Name { get; init; }
      public string MacAddress { get; init; }
      public bool IncludeReport { get; init; }

      public UpdateLocationCommand()
      {
         Name = string.Empty;
         MacAddress = string.Empty;
      }
   }
}
