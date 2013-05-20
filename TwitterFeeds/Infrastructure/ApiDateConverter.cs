using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;

namespace TwitterFeeds.Infrastructure
{
    public class ApiDateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDateConverter"/> class.
        /// </summary>
        public ApiDateConverter()
        {
            DateTimeFormat = "ddd MMM dd HH:mm:ss zzz yyyy";
        }
    }
}