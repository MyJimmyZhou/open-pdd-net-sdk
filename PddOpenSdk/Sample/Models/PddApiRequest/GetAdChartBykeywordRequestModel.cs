using System.Collections.Generic;
using Newtonsoft.Json;
namespace App.Models.PddApiRequest
{
    public partial class GetAdChartBykeywordRequestModel : PddRequestModel
    {
        /// <summary>
/// 该值为：pdd.ad.chart.bykeyword.get
/// </summary>
[JsonProperty("type")]
public object Type {get;set;}
/// <summary>
/// 报表日期，仅能查询近30天的报表数据，格式为：YYYY-MM-DD
/// </summary>
[JsonProperty("date")]
public object Date {get;set;}
/// <summary>
/// 返回页数，默认1页
/// </summary>
[JsonProperty("page")]
public int? Page {get;set;}
/// <summary>
/// 每页返回条数，默认20条
/// </summary>
[JsonProperty("page_size")]
public int? PageSize {get;set;}

}
}
