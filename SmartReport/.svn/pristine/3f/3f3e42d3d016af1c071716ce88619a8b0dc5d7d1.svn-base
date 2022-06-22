using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Bkav.eGovCloud.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        private bool _loadAppDomainAssemblies = true;

        private string _assemblySkipLoadingPattern = @"^System|^mscorlib|^Microsoft|^CppCodeProvider|^VJSharpCodeProvider|^WebDev|^Nuget|^Castle|^Iesi|^log4net|^Autofac|^AutoMapper|^EntityFramework|^EPPlus|^Fasterflect|^nunit|^TestDriven|^MbUnit|^Rhino|^QuickGraph|^TestFu|^Telerik|^Antlr3|^Recaptcha|^FluentValidation|^ImageResizer|^itextsharp|^MiniProfiler|^Newtonsoft|^Pandora|^WebGrease|^Noesis|^DotNetOpenAuth|^Facebook|^LinqToTwitter|^PerceptiveMCAPI|^CookComputing|^GCheckout|^Mono\.Math|^Org\.Mentalis|^App_Web|^BundleTransformer|^ClearScript|^JavaScriptEngineSwitcher|^MsieJavaScriptEngine|^Glimpse|^Ionic|^App_GlobalResources|^AjaxMin|^MaxMind|^NReco|^OffAmazonPayments|^UAParser";
        private string _assemblyRestrictToLoadingPattern = ".*";
        private readonly IDictionary<string, bool> _assemplyMatchTable = new Dictionary<string, bool>();

        private Regex _assemblySkipLoadingRegex = null;
        private Regex _assemblyRestrictToLoadingRegex = null;

        private bool _ignoreReflectionErrors = true;
        private IEnumerable<string> _customAssemblyNames = new List<string>();

        /// <summary>
        /// Giá trị xác định load các assembly thuộc current domain hiện tại
        /// </summary>
        public bool LoadAppDomainAssemblies
        {
            get { return _loadAppDomainAssemblies; }
            set { _loadAppDomainAssemblies = value; }
        }

        /// <summary>
        /// Lấy và thiết lập các Custom Assembly
        /// </summary>
        public IEnumerable<string> CustomAssemblyNames
        {
            get { return _customAssemblyNames; }
            set { _customAssemblyNames = value; }
        }

        /// <summary>
        /// Bỏ qua các Assembly lỗi
        /// </summary>
        public bool IgnoreReflectionErrors
        {
            get { return _ignoreReflectionErrors; }
            set { _ignoreReflectionErrors = value; }
        }

        #region ITypeFinder Members

        /// <summary>
        /// Trả về tất cả Assembly phù hợp
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            var addedAssemblyNames = new HashSet<string>();
            var assemblies = new List<Assembly>();

            if (LoadAppDomainAssemblies)
            {
                AddAssembliesInAppDomain(addedAssemblyNames, assemblies);
            }

            AddCustomAssemblies(addedAssemblyNames, assemblies);

            return assemblies;
        }

        /// <summary>
        /// Trả về tất cả các class có kiểu type phù hợp
        /// </summary>
        /// <param name="assignTypeFrom"></param>
        /// <param name="assemblies"></param>
        /// <param name="onlyConcreteClasses"></param>
        /// <returns></returns>
        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<System.Reflection.Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();

            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch
                    {
                        // Entity Framework 6 doesn't allow getting types (throws an exception)
                        if (!_ignoreReflectionErrors)
                        {
                            throw;
                        }
                    }
                    if (types != null)
                    {
                        foreach (var t in types)
                        {
                            if (assignTypeFrom.IsAssignableFrom(t) || (assignTypeFrom.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                            {
                                if (!t.IsInterface)
                                {
                                    if (onlyConcreteClasses)
                                    {
                                        if (t.IsClass && !t.IsAbstract)
                                        {
                                            result.Add(t);
                                        }
                                    }
                                    else
                                    {
                                        result.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);

                throw fail;
            }

            return result;
        }

        #endregion

        #region Private Methods

        private void AddAssembliesInAppDomain(HashSet<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            // Danh sách tất cả các assembly trong domain
            var currentAppdomainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in currentAppdomainAssemblies)
            {
                if (Match(assembly.FullName))
                {
                    if (!addedAssemblyNames.Contains(assembly.FullName))
                    {
                        addedAssemblyNames.Add(assembly.FullName);
                        assemblies.Add(assembly);
                    }
                }
            }
        }

        private void AddCustomAssemblies(HashSet<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            foreach (string assemblyName in CustomAssemblyNames)
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (!addedAssemblyNames.Contains(assembly.FullName))
                {
                    assemblies.Add(assembly);
                    addedAssemblyNames.Add(assembly.FullName);
                }
            }
        }

        /// <summary>
        /// Trả về kết quả kiểm tra assemply có phù hợp với các điều kiện cần kiểm tra không.
        /// </summary>
        /// <param name="assemblyFullName"></param>
        /// <returns></returns>
        private bool Match(string assemblyFullName)
        {
            if (_assemplyMatchTable.ContainsKey(assemblyFullName))
            {
                return _assemplyMatchTable[assemblyFullName];
            }

            if (_assemblySkipLoadingRegex == null)
            {
                _assemblySkipLoadingRegex = new Regex(_assemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            }

            if (_assemblySkipLoadingRegex.IsMatch(assemblyFullName))
            {
                _assemplyMatchTable[assemblyFullName] = false;
                return false;
            }

            if (_assemblyRestrictToLoadingRegex == null)
            {
                _assemblyRestrictToLoadingRegex = new Regex(_assemblyRestrictToLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            }

            var isMatch = _assemblyRestrictToLoadingRegex.IsMatch(assemblyFullName);
            _assemplyMatchTable[assemblyFullName] = isMatch;

            return isMatch;
        }
        
        private bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
