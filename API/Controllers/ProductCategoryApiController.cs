using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using IBLL;
using BLL;
using API.Models;

namespace API.Controllers
{
    public class ProductCategoryApiController : ApiController
    {
        public  IProductCategoryBLL Bll {
            get {
                return new ProductCategoryBLL();
            }
        }
        public ResponsMessage<List<ProductCategoryVModel>> GetAll()
        {
            //获取全部分类列表
            // var list = bll.GetAll();

            //递归生成json数据
            //1.获取一级节点(分类)
            var rootList = Bll.GetSub(0);
            List<ProductCategoryVModel> list = new List<ProductCategoryVModel>();
            foreach (var categary in rootList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = categary.ID;
                vModel.Name = categary.Name;
                vModel.OrderNum = categary.OrderNum;
                vModel.Img = categary.Img;
                vModel.PID = categary.PID;
                vModel.keyWords= categary.keyWords;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);
                list.Add(vModel);
            }
            return new ResponsMessage<List<ProductCategoryVModel>>()
            {
                Code = 200,
                Message = "请求成功",
                Data = list,
            };

        }
        //获取父节点下的所有子节点
        private void GetSub(ProductCategoryVModel parentNode)
        {
            var sublist = Bll.GetSub(parentNode.ID);

            if (sublist.Count() == 0)//递归的终止条件
            {
                return;
            }
            //判断子节点下是否还包含子节点
            foreach (var categary in sublist)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = categary.ID;
                vModel.Name = categary.Name;
                vModel.OrderNum = categary.OrderNum;
                vModel.Img = categary.Img;
                vModel.PID = categary.PID;
                vModel.keyWords = categary.keyWords;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);//开始递归
                parentNode.children.Add(vModel);
            }
        }
    }
}
