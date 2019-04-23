using Nancy;
using Nancy.Metadata.Modules;
using Nancy.ModelBinding;
using Nancy.Swagger;
using Swagger.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transfer8Pro.Entity;
using Transfer8Pro.ForeignWebAPI.Models;

namespace Transfer8Pro.ForeignWebAPI.Modules
{
    public class ProductsModule : BaseModule
    {
        public ProductsModule() : base("/products")
        {
            Get("/", _ =>
            {
                var list = new List<Product>
            {
                new Product{ Name="p1", Price=199 , IsActive = true },
                new Product{ Name="p2", Price=299 , IsActive= true }
            };

                return Negotiate.WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/json"), list);
            }, null, "GetProductList");

            Get("/{productid}", _ =>
            {
                var productId = _.productid;
                if (string.IsNullOrWhiteSpace(productId))
                    return HttpStatusCode.NotFound;

                var isActive = Request.Query.isActive ?? true;

                var product = new Product
                {
                    Name = "apple",
                    Price = 100,
                    IsActive = isActive
                };

                return Negotiate.WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/json"), product);
            }, null, "GetProductByProductId");

            Post("/", _ =>
            {
                var product = this.Bind<Product>();

                if (!Request.Headers.Any(x => x.Key == "test"))
                    return HttpStatusCode.BadRequest;

                return Negotiate
                    .WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/json"), product)
                    .WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/xml"), product);
            }, null, "AddProduct");

            Put("/", _ =>
            {
                var product = this.Bind<Product>();

                return Negotiate
                    .WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/json"), product);
            }, null, "UpdateProductByProductId");

            Head("/", _ =>
            {
                return HttpStatusCode.OK;
            }, null, "HeadOfProduct");

            Delete("/{productid}", _ =>
            {
                var productId = _.productid;

                if (string.IsNullOrWhiteSpace(productId))
                    return HttpStatusCode.NotFound;

                return Response.AsText("Delete product successful");
            }, null, "DeleteProductByProductId");

            Get("/GetProductByID", _ =>
            {
               var productId=  HttpContext.Current.Request.QueryString["productid"];
                var isactive = HttpContext.Current.Request.QueryString["isactive"];

               //var productId = _.productid;
                if (string.IsNullOrWhiteSpace(productId))
                    //return HttpStatusCode.NotFound;
                    return ErrorResult("参数productid不能为空");

                var isActive = Request.Query.isActive ?? true;

                var product = new Product
                {
                    Name = "apple",
                    Price = 100,
                    IsActive = isActive
                };

                //return Negotiate.WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/json"), product);
                return OkResult(product);
            }, null, "GetProductByID");

            Post("/AddProductList", _ =>
            {              
                
                var products = this.Bind<List<Product>>();

                //if (!Request.Headers.Any(x => x.Key == "test"))
                //    return HttpStatusCode.BadRequest;

                //return Negotiate
                //    .WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/json"), products)
                //    .WithMediaRangeModel(new Nancy.Responses.Negotiation.MediaRange("application/xml"), products);
                return OkResult();

            }, null, "AddProductList");
        }
    }

    public class ProductsMetadataModule : MetadataModule<PathItem>
    {
        public ProductsMetadataModule(ISwaggerModelCatalog modelCatalog)
        {
            modelCatalog.AddModels(typeof(Product), typeof(IEnumerable<Product>));
           
            #region
            Describe["GetProductList"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("GetProductList")
                       //Tag means this api belongs to which group  
                       .Tag("Products")
                            //Description of this api  
                            .Description("This returns a list of products")
                       //Summary of this api  
                       .Summary("Get all products")
                       //Default response of the api  
                       .Response(r => r.Schema<IEnumerable<Product>>(modelCatalog).Description("OK"))
                       ));
            #endregion

            #region
            Describe["GetProductByProductId"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("GetProductByProductId")
                   .Tag("Products2")
                   .Summary("Get a product by product's id")
                   .Description("This returns a product's infomation by the special id")
                                  //specify the parameters of this api   
                                  .Parameter(new Parameter
                                  {
                                      Name = "productid",
                                  //specify the type of this parameter is path  
                                  In = ParameterIn.Path,
                                  //specify this parameter is required or not  
                                  Required = true,
                                      Description = "id of a product"
                                  })
                        .Parameter(new Parameter
                        {
                            Name = "isactive",
                            In = ParameterIn.Query,
                            Description = "get the actived product",
                            Required = false,
                        })
                        .Response(r => r.Schema<Product>(modelCatalog).Description("Here is the product"))
                        .Response(404, r => r.Description("Can't find the product"))
                        ));
            #endregion

            #region
            Describe["AddProduct"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("AddProduct")
                            .Tag("Products")
                            .Summary("Add a new product to database")
                            .Description("This returns the added product's infomation")
                            //Request body when using POST,PUT..  
                            .BodyParameter(para => para.Name("para").Schema<Product>().Description("the infomation of the adding product").Build())
                            .Parameter(new Parameter()
                            {
                                Name = "test",
                                In = ParameterIn.Header,//http header  
                            Description = "must be not null",
                                Required = true,
                            })
                            //specify the request parameters can only be a JSON object  
                            .ConsumeMimeType("application/json")
                            //specify the response result can be a JSON object or a xml object  
                            .ProduceMimeTypes(new List<string> { "application/json", "application/xml" })
                            .Response(r => r.Schema<Product>(modelCatalog).Description("Here is the added product"))
                            .Response(400, r => r.Description("Some errors occur during the processing"))
            ));
            #endregion

            #region
            Describe["HeadOfProduct"] = desc => desc.AsSwagger(
                   with => with.Operation(
                       op => op.OperationId("HeadOfProduct")
                               //specify this api belongs to multi groups  
                               .Tags(new List<string>() { "Products", "Products2" })
                               .Summary("Something is deprecated")
                               .Description("This returns only http header")
                               //specify this api is deprecated  
                               .IsDeprecated()
                               .Response(r => r.Description("Nothing will return but http headers"))
                                ));
            #endregion

            #region
            Describe["GetProductByID"] = desc => desc.AsSwagger(
       with => with.Operation(
           op => op.OperationId("GetProductByID")
           .Tag("GetProductByID")
           .Summary("根据产品ID获取产品实体数据")
           .Description("返回产品数据实体")
           .Parameters(new List<Parameter>
           {
                new Parameter{Name = "productid",In = ParameterIn.Query,Required = true,Description = "产品ID(必填项)"},
                new Parameter{Name = "isactive",In = ParameterIn.Query,Required = false,Description = "是否最新产品"}
           })
           .ProduceMimeType("application/json")
           .Response("正确",r => r.Schema<Product>(modelCatalog).Description("正常响应JSON").Example("application/json", new ResponseModel { Status = 1, Data = new { Name = "产品名称", Price = "产品价格", IsActive = "是否最新产品" }, Msg = "" }))
           //.Response(404, r => r.Description("Can't find the product"))
           .Response("错误" ,r => r.Description("异常响应JSON").Example("application/json", new ResponseModel { Status = -1, Msg = "数据异常",Data="" }))
           ));
            #endregion

            #region
            Describe["AddProductList"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("AddProductList")
                            .Tag("AddProductList")
                            .Summary("添加多条产品")
                            .Description("添加多条产品，并返回处理状态")
                            //Request body when using POST,PUT..  
                            .BodyParameter(para => para.Name("para").Schema<Product>().Description("产品实体集").Build())
                            
                            //.Parameters(new List<Parameter>
                            //{                                  
                            //       new Parameter{Name = "Name",In = ParameterIn.Body,Required = true,Description = "产品名称(必填项）"},
                            //        new Parameter{Name = "Price",In = ParameterIn.Body,Required = true,Description = "产品价格(必填项)"},
                            //       new Parameter{Name = "IsActive",In = ParameterIn.Body,Required = false,Description = "是否最新产品(布尔类型)"}
                            //})
                            //.Parameter(new Parameter()
                            //{
                            //    Name = "test",
                            //    In = ParameterIn.Header,//http header  
                            //    Description = "must be not null",
                            //    Required = true,
                            //})
                            //specify the request parameters can only be a JSON object  
                            .ConsumeMimeType("application/json")
                            //specify the response result can be a JSON object or a xml object  
                            .ProduceMimeTypes(new List<string> { "application/json", "application/xml" })
                            //.Response(r => r.Schema<Product>(modelCatalog).Description("Here is the added product"))
                            //.Response(400, r => r.Description("Some errors occur during the processing"))
                            .Response("正确", r => r.Description("正常响应JSON").Example("application/json", new ResponseModel { Status = 1, Msg = "", Data = "" }))
                            .Response("错误", r => r.Description("异常响应JSON").Example("application/json", new ResponseModel { Status = -1, Msg = "数据异常", Data = "" }))
            ));
            #endregion
        }
    }
}