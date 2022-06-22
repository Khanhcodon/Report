#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> to and from the ISO 8601 date format (e.g. 2008-04-12T12:53Z).
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class JsonNetCurrencyConverter : JsonConverter
    {
        private const NumberStyles DefaultCurrencyStyle = NumberStyles.Currency;

        /// <summary>
        /// 
        /// </summary>
        private CultureInfo _culture;

        /// <summary>
        /// Gets or sets the culture used when converting a date to and from JSON.
        /// </summary>
        /// <value>The culture used when converting a date to and from JSON.</value>
        public CultureInfo Culture
        {
            get { return _culture ?? CultureInfo.CurrentCulture; }
            set { _culture = value; }
        }

        private const string DefaultCurrencyFormat = "c";

        private string _currencyFormat;

        /// <summary>
        /// Gets or sets the currency format used when converting a currency to and from JSON.
        /// </summary>
        /// <value>The currency format used when converting a currency to and from JSON.</value>
        public string CurrencyFormat
        {
            get { return _currencyFormat ?? string.Empty; }
            set { _currencyFormat = string.IsNullOrEmpty(value) ? null : value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private NumberStyles _currencyStyles = DefaultCurrencyStyle;

        /// <summary>
        /// Gets or sets the culture used when converting a date to and from JSON.
        /// </summary>
        /// <value>The culture used when converting a date to and from JSON.</value>
        public NumberStyles CurrencyStyles
        {
            get { return _currencyStyles; }
            set { _currencyStyles = value; }
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string text;
            if (value is decimal)
            {
                var real = decimal.Parse(value.ToString());
                text = real.ToString(_currencyFormat ?? DefaultCurrencyFormat, Culture);
            }
            else
            {
                throw new ApplicationException("");
            }
            writer.WriteValue(text);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Sử dụng Reflection để gọi tới Internal Method.
            Assembly assembly = Assembly.LoadFile(Directory.GetCurrentDirectory() + @"\Newtonsoft.Json.dll");
            Type type = assembly.GetType("Newtonsoft.Json.Utilities.ReflectionUtils");
            MethodInfo method = type.GetMethod("IsNullableType", BindingFlags.Public | BindingFlags.Static);
            Type parameters = objectType;

            // bool nullable = ReflectionUtils.IsNullableType(objectType);
            var nullable = (bool)method.Invoke(null, new Object[] { parameters });
            Type t = (nullable) ? Nullable.GetUnderlyingType(objectType) : objectType;
            if (reader.TokenType == JsonToken.Null)
            {
                if (!(bool)method.Invoke(null, new Object[] { parameters }))
                    throw new ApplicationException("");
                return null;
            }

            if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
            {
                return reader.Value;
            }
            if (reader.TokenType != JsonToken.String)
            {
                throw new ApplicationException("");
            }

            var floatingPointText = reader.Value.ToString();

            if (string.IsNullOrEmpty(floatingPointText) && nullable)
            {
                return null;
            }

            return decimal.Parse(floatingPointText, _currencyStyles, Culture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }
    }
}