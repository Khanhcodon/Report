using System.Linq;
using AutoMapper;

namespace Bkav.eGovCloud.Infrastructure
{
	public static class AutoMapperHelper
	{
		public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
		{
			var sourceType = typeof(TSource);
			var destinationType = typeof(TDestination);
			var maps = Mapper.GetAllTypeMaps();
			if (maps != null)
			{
				var currentTypeMap = maps.FirstOrDefault(x => x.SourceType == sourceType && x.DestinationType == destinationType);
				if (currentTypeMap != null)
				{
					foreach (var property in currentTypeMap.GetUnmappedPropertyNames())
					{
						var isFromSource = sourceType.GetProperty(property) != null;
						if (isFromSource)
						{
							expression.ForSourceMember(property, opt => opt.Ignore());
						}
						else
						{
							expression.ForMember(property, opt => opt.Ignore());
						}

					}
				}
			}
			return expression;
		}
	}
}