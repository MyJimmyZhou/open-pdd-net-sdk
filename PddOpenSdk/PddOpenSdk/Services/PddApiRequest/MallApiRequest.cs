using App.Models.PddApiRequest;
using App.Models.PddApiResponse;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace App.Services.PddApiRequest
{
    public class MallApiRequest : PddRequest {
        /// <summary>
/// 店铺信息接口
/// </summary>
public async Task<GetMallInfoResponseModel> GetMallInfoAsync(GetMallInfoRequestModel getMallInfo)
{
    var result = await PostAsync<GetMallInfoRequestModel,GetMallInfoResponseModel>("pdd.mall.info.get",getMallInfo);
    return result;
}

    }
}
