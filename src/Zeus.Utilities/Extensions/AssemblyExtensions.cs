using System;
using System.Reflection;

namespace Zeus.Utilities.Extensions
{
   public static class AssemblyExtensions
   {
      public static string GetAssemblyVersion(Type type)
      {
         return type
            .Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? string.Empty;
      }
   }
}
