using System;
using MediatR;

namespace Zeus.Models.Base.Commands
{
   public abstract class BaseCreatePlcCommand : IRequest<Result>
   {
      public Guid Id { get; init; }
      public DateTime Date { get; init; }
      public int DeviceId { get; init; }
      public int LocationId { get; private set; }

      public BaseCreatePlcCommand()
      {
         Id = Guid.NewGuid();
      }

      public void Update(int locationId)
      {
         LocationId = locationId;
      }
   }
}
