using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JassPro.Web.Models
{
    public class JsonPResult : ContentResult
    {
        private object data = null;
        private string callparam = "";
        public JsonPResult() { }
        public JsonPResult(string callparam, object data)
        {
            this.callparam = callparam;
            this.data = data;
        }
        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext != null)
            {
                HttpResponseBase Response = controllerContext.HttpContext.Response;
                HttpRequestBase Request = controllerContext.HttpContext.Request;
                if (string.IsNullOrEmpty(callparam))
                {
                    throw new Exception("Callback function name must be provided in the request!");
                }
                Response.ContentType = "application/x-javascript";
                if (data != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Response.Write(string.Format("{0}({1});",
                        callparam, serializer.Serialize(data)));
                }
            }
        }
    }
}