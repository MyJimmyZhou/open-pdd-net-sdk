using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PddOpenSdk.AspNetCore;
using PddOpenSdk.Models.Request.Ddk;
using PddOpenSdk.Models.Request.Ddkoauth;
using PddOpenSdk.Models.Request.Goods;
using PddOpenSdk.Services;
using Sample.Models;

namespace Sample.Controllers
{
    public class AuthController : Controller
    {
        readonly IHostingEnvironment _env;
        readonly PddService _pdd;
        readonly string AccessToken = "f4a72903f643401eaec5271dbb956b405d3ea1f0";
        public AuthController(PddService pdd, IHostingEnvironment env)
        {
            _pdd = pdd;
            _env = env;
            PddCommonApi.AccessToken = AccessToken;
        }
        public IActionResult Index()
        {
            string url = _pdd.AuthApi.GetWebOAuthUrl(PddCommonApi.RedirectUri);
            ViewData["url"] = url;
            return View();
        }

        /// <summary>
        /// 测试获取token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<IActionResult> Callback(string code)
        {
            var token = await _pdd.AuthApi.GetAccessTokenAsync(code);

            return Content(token.AccessToken);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Test()
        {
            var result = await _pdd.DdkApi.GetDdkOrderListIncrementAsync(new GetDdkOrderListIncrementRequestModel
            {
                StartUpdateTime = DateTimeOffset.Now.AddMinutes(-10).ToUnixTimeSeconds(),
                EndUpdateTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Page = 1,
                PageSize = 50
            });
            //var result = await _pdd.DdkApi.GetDdkOrderDetailAsync(new GetDdkOrderDetailRequestModel
            //{
            //    OrderSn = "190410-102467503050906"
            //});
            //var result = await _pdd.DdkApi.GetDdkMallGoodsListAsync(new GetDdkMallGoodsListRequestModel
            //{
            //    MallId = 712520300,
            //    PageNumber = 1,
            //    PageSize = 5
            //});
            return Json(result);
        }

        /// <summary>
        /// 测试图片上传
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TestImageUpload()
        {
            var filePath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            string base64 = "data:image/png;base64," + Convert.ToBase64String(bytes);

            var model = new UploadGoodsImageRequestModel
            {
                Image = base64
            };
            var result = await _pdd.GoodsApi.UploadGoodsImageAsync(model);
            return Json(base64);
        }

        /// <summary>
        /// 上传商品测试
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TestGoodsUpload()
        {
            var model = new AddGoodsRequestModel
            {
                GoodsName = "葡萄",
                GoodsType = 1,
                GoodsDesc = "葡萄串",
                CatId = 1,
                CountryId = 1,
                MarketPrice = 12,
                IsPreSale = false,
                ShipmentLimitSecond = 3600 * 24,
                CostTemplateId = 1000000,
                IsRefundable = true,
                SecondHand = false,
                IsFolt = true,
                SkuList = new System.Collections.Generic.List<AddGoodsRequestModel.SkuListRequestModel>
                {
                    new AddGoodsRequestModel.SkuListRequestModel
                    {
                       ThumbUrl = "https://t00img.yangkeduo.com/goods/images/2019-03-09/dacebcdc-9c26-479c-9174-f3ecf0b579b6.jpg",
                       OverseaSku = new AddGoodsRequestModel.SkuListRequestModel.OverseaSkuRequestModel
                       {
                           MeasurementCode = "123",
                           Taxation = 0,
                           Specifications = "spe"
                       },
                       SpecIdList = "[1754889520]",
                       Weight = 200,
                       Quantity = 10,
                       MultiPrice = 12,
                       Price = 15,
                       LimitQuantity = 10,
                       IsOnsale = 1
                    }
                },
                CarouselGallery = new System.Collections.Generic.List<string>
                {
                    "https://t00img.yangkeduo.com/goods/images/2019-03-09/dacebcdc-9c26-479c-9174-f3ecf0b579b6.jpg",
                    "https://t00img.yangkeduo.com/goods/images/2019-03-09/0a1ee8e4-e94e-4d5c-89ab-fe4a4e163ee9.jpg"
                },
                DetailGallery = new System.Collections.Generic.List<string>
                {
                    "https://t00img.yangkeduo.com/goods/images/2019-03-09/0a1ee8e4-e94e-4d5c-89ab-fe4a4e163ee9.jpg"
                }
            };
            var result = await _pdd.GoodsApi.AddGoodsAsync(model);
            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
