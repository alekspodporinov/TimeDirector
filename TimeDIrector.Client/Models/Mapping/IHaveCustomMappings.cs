using AutoMapper;

namespace TimeDIrector.Client.Models.Mapping
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IMapperConfigurationExpression configuration);
	}
}