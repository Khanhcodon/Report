using AutoMapper;
using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;
namespace Bkav.eGovCloud.Api.Dto
{
    public static class MappingDtoEntityExtensions
    {
        #region DocType

        public static DocTypeDto ToDto(this DocType entity)
        {
            return Mapper.Map<DocType, DocTypeDto>(entity);
        }

        public static IEnumerable<DocTypeDto> ToListDto(this IEnumerable<DocType> listEntity)
        { 
            return Mapper.Map<IEnumerable<DocType>, IEnumerable<DocTypeDto>>(listEntity);
        }

        #endregion

        #region Department

        public static DepartmentDto ToDto(this Department entity)
        {
            return Mapper.Map<Department, DepartmentDto>(entity);
        }

        public static IEnumerable<DepartmentDto> ToListDto(this IEnumerable<Department> listEntity)
        {
            return Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(listEntity);
        }

        #endregion

        #region Form Group

        public static FormGroupDto ToDto(this FormGroup entity)
        {
            return Mapper.Map<FormGroup, FormGroupDto>(entity);
        }

        public static IEnumerable<FormGroupDto> ToListDto(this IEnumerable<FormGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<FormGroup>, IEnumerable<FormGroupDto>>(listEntity);
        }

        #endregion

        #region Form

        public static FormDto ToDto(this Form entity)
        {
            return Mapper.Map<Form, FormDto>(entity);
        }

        public static IEnumerable<FormDto> ToListDto(this IEnumerable<Form> listEntity)
        {
            return Mapper.Map<IEnumerable<Form>, IEnumerable<FormDto>>(listEntity);
        }

        #endregion

        #region Supplementary

        public static SupplementaryDto ToDto(this Supplementary entity)
        {
            return Mapper.Map<Supplementary, SupplementaryDto>(entity);
        }

        #endregion

        #region Fee

        public static IEnumerable<FeeDto> ToDto(this IEnumerable<Fee> entity)
        {
            return Mapper.Map<IEnumerable<Fee>, IEnumerable<FeeDto>>(entity);
        }

        #endregion

        #region Paper

        public static IEnumerable<PaperDto> ToDto(this IEnumerable<Paper> entity)
        {
            return Mapper.Map<IEnumerable<Paper>, IEnumerable<PaperDto>>(entity);
        }

        #endregion

        #region SearchResultDto

        public static SearchResultDto ToDto(this DocumentDto entity)
        {
            return Mapper.Map<DocumentDto, SearchResultDto>(entity);
        }

        public static IEnumerable<SearchResultDto> ToListDto(this IEnumerable<DocumentDto> listEntity)
        {
            return Mapper.Map<IEnumerable<DocumentDto>, IEnumerable<SearchResultDto>>(listEntity);
        }

        #endregion
    }
}
