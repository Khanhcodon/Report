#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

#endregion

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    ///   Read/Write object graphs to/from json using NewtonSoft.Json
    /// </summary>
    /// <remarks>
    /// Thư viện này sử dụng Json.Net, và khởi tạo với các tham số mặc định cho từng mục đích yêu cầu.
    /// Cung cấp các API xử lý dữ liệu Json đáp ứng các mục đích cụ thể sau:
    /// 1. C# vs Js: 
    ///     + Tối ưu chuỗi json trả về cho client
    ///     + DateTime, Currency, Number (int, float, decimal...) --> StringifyJs, Parse, ParseAs theo CurrentCulture     
    /// 2. C# vs database (json file, json field in database):
    ///     + Tối ưu chuỗi json lưu trữ thông tin đối tượng
    ///     + DateTime, Currency, Number (int, float, decimal...) --> StringifyJs, Parse, ParseAs theo InvariantCulture
    /// 3. Một số format
    ///     + DateTime: http://msdn.microsoft.com/en-us/library/az4se3k1.aspx
    ///         Một số format không phụ thuộc culture:
    ///         - "O", "o" (Round-trip date/time pattern.):      6/15/2009 1:45:30 PM -> 2009-06-15T13:45:30.0900000
    ///         - "R", "r" (RFC1123 pattern.):                   6/15/2009 1:45:30 PM -> Mon, 15 Jun 2009 20:45:30 GMT
    ///         - "s" (Sortable date/time pattern.):             6/15/2009 1:45:30 PM -> 2009-06-15T13:45:30
    ///         - "u" (Universal sortable date/time pattern.):   6/15/2009 1:45:30 PM -> 2009-06-15 20:45:30Z
    ///     + Number: http://msdn.microsoft.com/en-us/library/dwhawy9k.aspx
    ///         - "g", "G": --> Phân cách phần thập phân... VD: 1234,34 (vi)
    ///         - "n", "N": --> Cả phân cách phần thập phân và phần nghìn... VD: 1.234,34 (vi)
    /// 4. Đảm bảo dữ liệu trong chuỗi json luôn luôn xử lý theo culture("en-US"); từ json --> js, c# theo culture("en-US") --> Đưa lên view theo currentCulture
    /// </remarks>
    [DebuggerStepThrough]
    public static class Json2
    {
        #region Class Methods

        #region For Backend - Using IntervalCulture ("en-US")

        /// <summary>
        ///   Get a serializer suited for two-way serialization of .NET types.
        /// </summary>
        /// <returns> </returns>
        public static JsonSerializer Serializer(
            TypeNameHandling typeNameHandling = TypeNameHandling.None,// Default:None
            DefaultValueHandling defaultValueHandling = DefaultValueHandling.Include, // Default: Include
            FormatterAssemblyStyle assemblyNameType = FormatterAssemblyStyle.Simple,// Default: Simple
            NullValueHandling nullValueHandling = NullValueHandling.Ignore, // Default: Include
            MissingMemberHandling missingMemberHandling = MissingMemberHandling.Ignore,// Default: Ignore
            ReferenceLoopHandling loopHandling = ReferenceLoopHandling.Ignore,// Default: Error
            ObjectCreationHandling objectCreationHandling = ObjectCreationHandling.Auto)// Default: Auto
        {
            // Mặc định Json.Net đã sử dụng InvariantCulture để xử lý dữ liệu, nên không cần gán lại culture.
            var result = new JsonSerializer
                {
                    MissingMemberHandling = missingMemberHandling,
                    NullValueHandling = nullValueHandling,
                    ReferenceLoopHandling = loopHandling,
                    DefaultValueHandling = defaultValueHandling,
                    TypeNameHandling = typeNameHandling,
                    TypeNameAssemblyFormat = assemblyNameType,
                    ObjectCreationHandling = objectCreationHandling,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };
            // Xử lý json với Backend, các kiểu dữ liệu mặc định ở dạng chuẩn, luôn luôn có thể Deserialize về Object
            // --> Không cần add các converter đặc biệt, như: Datetime, Float, Currency...
            result.Converters.Add(new StringEnumConverter());
            // Nếu add thêm Converter cần gán InvariantCulture cho các Converter này nếu có

            return result;
        }

        /// <summary>
        ///   Stringifies the specified graph with type information (suitable for two-way serialization of complex .NET types) (defaule cultrue)
        /// </summary>
        /// <param name="graph"> The graph. </param>
        /// <returns> </returns>
        public static string Stringify(this object graph)
        {
            using (var writer = new StringWriter())
            {
                Stringify(graph, writer);
                return writer.ToString();
            }
        }

        /// <summary>
        ///   Stringifies the specified graph to a text writer.
        /// </summary>
        /// <param name="graph"> The graph. </param>
        /// <param name="writer"> The writer. </param>
        private static void Stringify(object graph, TextWriter writer)
        {
            Serializer().Serialize(writer, graph);
        }

        /// <summary>
        ///   Parses the specified reader and create an object graph of the provided type.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static object Parse(TextReader reader, Type type)
        {
            return Serializer().Deserialize(reader, type);
        }

        /// <summary>
        ///   Parses the input json string as an instance of type T.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="json"> The json. </param>
        /// <returns> </returns>
        [DebuggerStepThrough]
        public static T ParseAs<T>(string json) where T : class
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentNullException("json");
            }
            using (var reader = new StringReader(json))
            {
                // return Parse(reader, typeof(T)) as T;
                return ParseAs<T>(reader);
            }
        }

        /// <summary>
        ///   Parses as.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="reader"> The reader. </param>
        /// <returns> </returns>
        public static T ParseAs<T>(TextReader reader) where T : class
        {
            // return Parse(reader, typeof(T)) as T;
            JsonReader textReader = new JsonTextReader(reader);
            return Serializer().Deserialize<T>(textReader);
        }

        /// <summary>
        ///   Parses the input json string as an instance of type T.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="jsonStream"> The json stream. </param>
        /// <returns> </returns>
        public static T ParseAs<T>(Stream jsonStream) where T : class
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }
            using (var reader = new StreamReader(jsonStream))
            {
                // return Parse(reader, typeof(T)) as T;
                return ParseAs<T>(reader);
            }

        }

        /// <summary>
        ///   Loads an object graph (of T) from a json file.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="file"> The file. </param>
        /// <returns> </returns>
        public static T LoadFromFile<T>(string file) where T : class
        {
            if (String.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException("file");
            }
            using (var reader = new StreamReader(file, Encoding.UTF8, true))
            {
                return ParseAs<T>(reader);
            }
        }

        /// <summary>
        ///   Saves an object graph to a file as Json.
        /// </summary>
        /// <param name="graph"> The graph. </param>
        /// <param name="file"> The file. </param>
        public static void SaveToFile(object graph, string file)
        {
            if (String.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException("file");
            }
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(file, false, Encoding.UTF8);
                using (var jsWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented })
                {
                    var serializer = Serializer();
                    serializer.TypeNameHandling = TypeNameHandling.Auto;
                    serializer.Serialize(jsWriter, graph);
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Dispose();
                }
            }
        }

        #endregion

        #region For Presentation - Using CurrentCulture

        /// <summary>
        ///   Get a serializer suited for one-way serialization for Javascript clients' consumption.
        /// </summary>
        /// <param name="ignoreDefaultValue"> Ignore default value: true else false </param>
        /// <returns> </returns>
        public static JsonSerializer JsSerializer(bool ignoreDefaultValue = false)
        {
            var result = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.None, // Default:None
                DefaultValueHandling = DefaultValueHandling.Include, // Default: Include --> Khac voi Serializer DefaultValueHandling.Ignore
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,// Default: Simple
                NullValueHandling = NullValueHandling.Ignore,// Default: Include
                MissingMemberHandling = MissingMemberHandling.Ignore,// Default: Ignore
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,// Default: Error
                ObjectCreationHandling = ObjectCreationHandling.Auto,// Default: Auto
                DateFormatHandling = DateFormatHandling.IsoDateFormat// Default: IsoDateFormat,
            };
            if (ignoreDefaultValue)
            {
                result.DefaultValueHandling = DefaultValueHandling.Ignore;
            }
            result.Converters.Add(new StringEnumConverter());

            // Mặc định Json.Net đã sử dụng InvariantCulture để xử lý dữ liệu --> cần gán lại current culture.
            result.Culture = CultureInfo.CurrentCulture;

            // Với các kiểu dữ liệu đặc biệt: DateTime, Số có dấu chấm động (float, double, decimal), Tiền tệ... nếu là xử lý với Presentation thì cần phụ thuộc vào culture
            // --> Cần add thêm 3 Converter bên dưới để đảm nhận việc xử lý này. Các kiểu dữ liệu khác vẫn làm việc với IntervalCulture mặc định và đúng cho mọi Culture.
            result.Converters.Add(new IsoDateTimeConverter()); // Mặc định sử dụng CultureInfo.CurrentCulture; --> Không cần new IsoDateTimeConverter{Culture = CultureInfo.CurrentCulture}
            result.Converters.Add(new JsonNetFloatingPointConverter()); // Mặc định sử dụng CultureInfo.CurrentCulture; --> Không cần new JsonNetFloatingPointConverter{Culture = CultureInfo.CurrentCulture}
            result.Converters.Add(new JsonNetCurrencyConverter()); // Mặc định sử dụng CultureInfo.CurrentCulture; --> Không cần new JsonNetCurrencyConverter{Culture = CultureInfo.CurrentCulture}


            return result;
        }

        /// <summary>
        ///   Serialize the graph into Json without type information (suited for Javascript client).
        /// </summary>
        /// <param name="graph"> The graph. </param>
        /// <param name="ignoreDefaultValue"> Ignore default value: true else false </param>
        /// <returns> </returns>
        public static string StringifyJs(this object graph, bool ignoreDefaultValue = false)
        {
            using (var writer = new StringWriter())
            {
                var jsSerializer = JsSerializer(ignoreDefaultValue);
                jsSerializer.Serialize(writer, graph);
                return writer.ToString();
            }
        }

        /// <summary>
        ///   Parses the specified reader and create an object graph of the provided type.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        /// <param name="type"> The type. </param>
        /// <returns> </returns>
        public static object ParseJs(TextReader reader, Type type)
        {
            return JsSerializer().Deserialize(reader, type);
        }

        /// <summary>
        ///   Parses the input json string as an instance of type T.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="json"> The json. </param>
        /// <returns> </returns>
        public static T ParseAsJs<T>(string json) where T : class
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentNullException("json");
            }
            using (var reader = new StringReader(json))
            {
                // return Parse(reader, typeof(T)) as T;
                return ParseAsJs<T>(reader);
            }
        }

        /// <summary>
        ///   Parses as.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="reader"> The reader. </param>
        /// <returns> </returns>
        public static T ParseAsJs<T>(TextReader reader) where T : class
        {
            // return Parse(reader, typeof(T)) as T;
            JsonReader textReader = new JsonTextReader(reader);
            return JsSerializer().Deserialize<T>(textReader);
        }

        /// <summary>
        ///   Parses the input json string as an instance of type T.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="jsonStream"> The json stream. </param>
        /// <returns> </returns>
        public static T ParseAsJs<T>(Stream jsonStream) where T : class
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }
            using (var reader = new StreamReader(jsonStream))
            {
                // return Parse(reader, typeof(T)) as T;
                return ParseAsJs<T>(reader);
            }
        }

        /// <summary>
        ///   Remove type information for a json previously created for .NET.
        /// </summary>
        /// <param name="dotNetJson"> The dot net json. </param>
        /// <returns> </returns>
        public static string SanitizeDotNetJsonForJs(string dotNetJson)
        {
            return ParseAsJs<IDictionary<string, object>>(dotNetJson).StringifyJs();
        }

        #endregion

        #region For C# Object Serializer

        // TODO: Các method phần này sử dụng TypeNameHandling.All hoặc TypeNameHandling.Object

        #endregion

        #endregion
    }
}