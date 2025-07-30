using Beauty.Application.Modules.Setting.Mapping;
using Beauty.Application.Modules.Setting.Messaging;
using Beauty.Application.Modules.Setting.ViewModel;
using Beauty.Model.Product;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Application.File;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Setting.Implementation
{
    public class ProductService : ServiceBase<CoreDbContext>, IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductService(IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironmen)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironmen;
        }

        public async Task<ProductCreateResponse> Create(ProductCreateRequest request)
        {
            var response = new ProductCreateResponse();
            try
            {
                if (DbContext.Products.Any(x => (x.Name == request.Entity.Name || x.Code == request.Entity.Code) && !x.IsDeleted))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.ProductExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var imageList = new List<ProductImageViewModel>();
                if (request.Entity.Images != null)
                    foreach (var image in request.Entity.Images)
                    {
                        var fileManager = new FileManager(image.File, _hostingEnvironment);
                        var result = fileManager.SaveToHost($"product-images/{request.Entity.Code}", null);
                        if (result.IsSuccess)
                            image.ServerPath = result.SavedFileName;
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = result.Message;
                            response.AlertType = ResponseAlertResource_en.Danger;
                            return response;
                        }
                    }

                var entity = DbContext.Products.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ProductCreateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<ProductUpdateResponse> Update(ProductUpdateRequest request)
        {
            var response = new ProductUpdateResponse();
            try
            {
                if (DbContext.Products.Any(x => x.Id == request.Entity.Id))
                {

                    var product = DbContext.Products.Include(x => x.Images).FirstOrDefault(x => x.Id == request.Entity.Id);

                    if (request.Entity.Images != null)
                    {
                        request.Entity.Images = request.Entity.Images.Where(x => x.File != null).ToList();
                        foreach (var image in request.Entity.Images)
                        {
                            var fileManager = new FileManager(image.File, _hostingEnvironment);
                            var result = fileManager.SaveToHost($"product-images/{request.Entity.Code}", null);
                            if (result.IsSuccess)
                                product.Images.Add(new ProductImageModel
                                {
                                    ServerPath = result.SavedFileName
                                });
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = result.Message;
                                response.AlertType = ResponseAlertResource_en.Danger;
                                return response;
                            }
                        }
                    }

                    product = request.Entity.ToUpdateModel(product, _httpContextAccessor);
                    DbContext.Entry(product).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.CustomerUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.ProductNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<ProductFindByIdResponse> FindById(ProductFindByIdRequest request)
        {
            var response = new ProductFindByIdResponse();
            try
            {
                if (!DbContext.Products.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.ProductNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                response.Entity = DbContext.Products.Include(x => x.Images).Include(x => x.Unit).FirstOrDefault(x => x.Id == request.Id).ToFindByIdModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<ProductFindAllByPagingResponse> FindAllByPaging(ProductFindAllByPagingRequest request)
        {
            var response = new ProductFindAllByPagingResponse();
            try
            {
                var dbset = DbContext.Products.Include(x => x.Images).Include(x => x.Unit).OrderByDescending(x => x.CreateDateTime).AsQueryable();
                Expression<Func<ProductModel, bool>> predicate = null;


                if (request.Search?.Value.Length > 0 && !string.IsNullOrEmpty(request.Search?.Value))
                {
                    predicate = product => product.Name.Contains(request.Search.Value) ||
                                           product.Code.Contains(request.Search.Value);
                }

                var entities = predicate is null ?
                          dbset.Skip(request.Start).Take(request.Length) :
                          dbset.AsExpandable().Where(predicate).Skip(request.Start).Take(request.Length);

                response.Data = entities.ToDataTableViewModel();
                response.Draw = request.Draw;
                response.RecordsTotal = dbset.Count();
                response.RecordsFiltered = dbset.Count();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<ProductImageDeleteResponse> DeleteImage(ProductImageDeleteRequest request)
        {
            var response = new ProductImageDeleteResponse();
            try
            {
                if (DbContext.Products.Any(x => x.Images.Any(i => i.Id == request.Id)))
                {
                    var product = DbContext.Products.Include(x => x.Images).FirstOrDefault(x => x.Images.Any(i => i.Id == request.Id));

                    var imagePath = product.Images.FirstOrDefault(x => x.Id == request.Id).ServerPath;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var fileManager = new FileManager(_hostingEnvironment);
                        var result = fileManager.DeleteFromHost($"product-images/{product.Code}", imagePath);
                        if (result.IsSuccess)
                        {
                            var productImage = product.Images.FirstOrDefault(x => x.Id == request.Id);
                            DbContext.Entry(productImage).State = EntityState.Deleted;
                            DbContext.SaveChanges();

                            response.IsSuccess = true;
                            response.Message = MessagingResource_fa.ProductImageDeleteSucceed;
                            response.AlertType = ResponseAlertResource_en.Success;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = MessagingResource_fa.ProductImageDeleteFaild;
                            response.AlertType = ResponseAlertResource_en.Success;
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.ProductImageNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<ProductDeleteByIdResponse> Delete(ProductDeleteByIdRequest request)
        {
            var response = new ProductDeleteByIdResponse();
            try
            {
                if (DbContext.Products.Any(x => x.Id == request.Id))
                {

                    var product = DbContext.Products.FirstOrDefault(x => x.Id == request.Id);

                    product.ReverseDelete();
                    DbContext.Entry(product).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.ProductDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.ProductDeleteFaild;
                    response.AlertType = ResponseAlertResource_en.Danger;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }
    }
}
