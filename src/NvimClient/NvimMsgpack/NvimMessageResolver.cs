using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using NvimClient.NvimMsgpack.Models;

namespace NvimClient.NvimMsgpack
{
  public class NvimMessageResolver : IFormatterResolver
  {
    // Resolver should be singleton.
    public static readonly IFormatterResolver Instance = new NvimMessageResolver();

    private NvimMessageResolver()
    {
    }

    // GetFormatter<T>'s get cost should be minimized so use type cache.
    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return FormatterCache<T>.Formatter;
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> Formatter;

      // generic's static constructor should be minimized for reduce type generation size!
      // use outer helper method.
      static FormatterCache()
      {
        Formatter = (IMessagePackFormatter<T>)NVimMessageResolverHelper.GetFormatter(typeof(T));
      }
    }
  }

  internal static class NVimMessageResolverHelper
  {
    // If type is concrete type, use type-formatter map
    static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
    {
        {typeof(NvimMessage), new NvimMessageFormatter()}
        // add more your own custom serializers.
    };

    internal static object GetFormatter(Type t)
    {
      object formatter;
      if (formatterMap.TryGetValue(t, out formatter))
      {
        return formatter;
      }

      // If target type is generics, use MakeGenericType.
      if (t.IsGenericParameter && t.GetGenericTypeDefinition() == typeof(ValueTuple<,>))
      {
        return Activator.CreateInstance(typeof(ValueTupleFormatter<,>).MakeGenericType(t.GenericTypeArguments));
      }

      // If type can not get, must return null for fallback mechanism.
      return null;
    }
  }
}
