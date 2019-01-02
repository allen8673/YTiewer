using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YTiewer.Unity
{
    public class YTInfo : PropertyObserver<YTInfo>
    {

        public static YTInfo Detail { get; set; } = new YTInfo();
        private YTInfo() { }

        public string YTUrl
        {
            get => GetValue<string>();
            set
            {
                Uri uri;
                if (!Uri.TryCreate(value, UriKind.Absolute, out uri) || uri.Host != "www.youtube.com") return;
                NameValueCollection queryValues = HttpUtility.ParseQueryString(uri.Query);
                YTParamAttribute attri;
                foreach (var prop in typeof(YTInfo).GetProperties())
                {
                    attri = prop.GetCustomAttribute<YTParamAttribute>();
                    if (attri == null) continue;
                    attri = prop.GetCustomAttribute<YTParamAttribute>();
                    prop.SetValue(YTInfo.Detail, queryValues[attri.Value]);
                }

                SetValue(value);
            }
        }

        [YTParam("v")]
        public string VedioID
        {
            get => GetValue<string>();
            set
            {
                EmbedeURL = string.IsNullOrEmpty(value) ? EmbedeURL : $@"https://www.youtube.com/embed/{value}";
                //EmbedeHTML = string.IsNullOrEmpty(value) ? EmbedeHTML :
                //    $@"<iframe width=""901"" height=""507"" src=""https://www.youtube.com/embed/{value}"" frameborder=""0"" allow=""accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>";
                SetValue(value);
            }
        }

        public string EmbedeURL
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        //public string EmbedeHTML
        //{
        //    get => GetValue<string>();
        //    set => SetValue(value);
        //}
    }
}
