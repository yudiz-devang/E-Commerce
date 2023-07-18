using e_commerce.admin.Service.Shared;
using Newtonsoft.Json;

namespace e_commerce.admin.Model.Const
{
    public class CallAPI
    {
        public ResponseMetaCallAPI meta { get; set; }

        public dynamic data { get; set; }

        public string GetJsonData() => JsonConvert.SerializeObject(data);
    }
    public class CallAPIList
    {
        public ResponseMetaListCallAPI meta { get; set; }
    }

    public class ResponseMetaCallAPI
    {
        public int statusCode { get; set; }

        public dynamic message { get; set; }
    }

    public class ResponseMetaListCallAPI
    {
        public int statusCode { get; set; }

        public string[] message { get; set; }
    }

}
