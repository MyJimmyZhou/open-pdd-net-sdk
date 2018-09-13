using System.Collections.Generic;
using Newtonsoft.Json;
namespace App.Models.PddApiResponse
{
    public partial class CreateGoodsLogisticsTemplateResponseModel : PddResponseModel
    {
        /// <summary>
/// 返回resposne
/// </summary>
[JsonProperty("goods_logistics_template_create_response")]
public object GoodsLogisticsTemplateCreateResponse {get;set;}
/// <summary>
/// 模版id
/// </summary>
[JsonProperty("template_id")]
public object TemplateId {get;set;}

    public partial class GoodsLogisticsTemplateCreateResponseResponseModel : PddResponseModel
    {
        /// <summary>
/// 模版id
/// </summary>
[JsonProperty("template_id")]
public object TemplateId {get;set;}

}

}
}
