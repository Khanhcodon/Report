using System;
using System.Collections.Generic;
using System.Reflection;
namespace Bkav.eGovCloud.Core.Infrastructure
{
    /// <summary>
    /// Cung cấp các thông tin về Kiểu (Type) của các đối tượng trong cả engine.
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// Trả về danh sách tất cả các assemble.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies();

        /// <summary>
        /// Trả về danh sách các Class có kiểu Type nào đó
        /// </summary>
        /// <param name="assignTypeFrom">Type cần tìm</param>
        /// <param name="assemblies">Danh sách các assembly cần tìm</param>
        /// <param name="onlyConcreteClasses"></param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public static class ITypeFinderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="finder"></param>
        /// <param name="onlyConcreteClasses"></param>
        /// <param name="ignoreInactivePlugins"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindClassesOfType<T>(this ITypeFinder finder, bool onlyConcreteClasses = true, bool ignoreInactivePlugins = false)
        {
            return finder.FindClassesOfType(typeof(T), finder.GetAssemblies(), onlyConcreteClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="finder"></param>
        /// <param name="assignTypeFrom"></param>
        /// <param name="onlyConcreteClasses"></param>
        /// <param name="ignoreInactivePlugins"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindClassesOfType(this ITypeFinder finder, Type assignTypeFrom, bool onlyConcreteClasses = true, bool ignoreInactivePlugins = false)
        {
            return finder.FindClassesOfType(assignTypeFrom, finder.GetAssemblies(), onlyConcreteClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="finder"></param>
        /// <param name="assemblies"></param>
        /// <param name="onlyConcreteClasses"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindClassesOfType<T>(this ITypeFinder finder, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return finder.FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

    }
}
