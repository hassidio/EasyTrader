using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Exceptions;
using EasyTrader.Core.Views.DataTableView;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace EasyTrader.Core.Models.Entities
{
    public abstract class Entity : IApiRequestor, IEntity
    {
        public Entity() { }

        #region IEntity

        public abstract string EntityId { get; set; }

        public abstract string? EntityParentId { get; set; }

        public abstract string? EntityStamp { get; }

        public abstract dynamic RawClientData { get; set; }


        [JsonIgnore]
        public string? SerializedEntityParent { get; set; }

        public TParent GetDeserializedEntityParent<TParent>()
        {
            if (SerializedEntityParent is null) { return default; }
            return JsonConvert.DeserializeObject<TParent>(SerializedEntityParent);
        }

        public abstract void SetEntity(string? id = null, string? parentId = null, dynamic? rawClientData = null);

        public abstract void OnSetEntity(string? id = null, string? parentId = null, dynamic? rawClientData = null);


        public abstract IList<dynamic> GetRawClientDataSets(int recodesCount);

        public abstract bool IsRawClientDataTypeIEnumerable { get; }
        

        [JsonIgnore]
        public IList<dynamic> ClientDataList
        {
            get
            {
                if (RawClientData is null) { return null; }

                if (IsRawClientDataTypeIEnumerable)
                {
                    try { return new List<dynamic>(JsonConvert.DeserializeObject<dynamic>(RawClientData)); }
                    catch { }
                    try { return RawClientData.ToList(); }
                    catch { }
                    try { return new List<dynamic>(RawClientData); }
                    catch { }
                }

                return null;
            }
        }

        [JsonIgnore]
        public JArray ClientDataArray
        {
            get
            {
                if (RawClientData is null) { return null; }
                if (RawClientData.GetType() == typeof(JArray)) { return RawClientData; }
                try { return JsonConvert.DeserializeObject<dynamic>(RawClientData); }
                catch { return null; }
            }
        }


        #endregion



        #region IApiRequester
        public ApiHttpRequest GetApiRequest(
         string baseUri,
         string? pathUri = null,
         ApiRequestOptions? requestOptions = null)
        {
            return new ApiHttpRequest(
                this,
                baseUri,
                pathUri,
                requestOptions);
        }


        public abstract Dictionary<string, string>? ApiRequestClientQuery { get; }
        public abstract Dictionary<string, string>? ApiRequestClientHeaders { get; }

        public abstract object? GetApiRequestClientContent(object? requestContent = null);


        //*/ Test
        public abstract int GetResponseWeight(ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse);


        //*/ Test
        protected int GetWeightFromHeader(
            ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse)
        {
            if (string.IsNullOrEmpty(apiHttpRequest.ResponseHeaderWeightKeyName) ||
                            apiHttpResponse.Headers is null) { return 0; }

            string? weight = null;

            try
            {
                weight = apiHttpResponse.Headers[apiHttpRequest.ResponseHeaderWeightKeyName].FirstOrDefault();

                return int.Parse(weight);
            }
            catch (Exception ex)
            {
                throw CommonException.InvalidResponseHeaderWeight(apiHttpRequest.ResponseHeaderWeightKeyName, weight, ex); //*/ test 
            }

        }


        public abstract Task<HttpException> GetApiHttpException(HttpResponseMessage response);




        #endregion


        ////*/ Usage? => Test 
        //[JsonIgnore]
        //public PropertyDataTable ClientPropertyDataTable { get { return new PropertyDataTable(RawClientData); } }

        ////*/ Usage? => Test 
        //[JsonIgnore]
        //public CommonDataTable ClientCommonDataTable { get { return new CommonDataTable(RawClientData); } }

    }
}