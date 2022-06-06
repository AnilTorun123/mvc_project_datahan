using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Dextra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetList()
        {
            return _categoryDal.List();
        }

        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);

        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }

        public Category GetByID(int id)
        {
            return _categoryDal.Get(x => x.CategoryID == id);
        }

        // son çıkmadan müsadenle sana bir naçizane önerim olacak. Normalde controller kısmında manager'ı direkt çağırmak solid prensibime uymuyor. Solidde bağımlılığı azalmak var. yani biz normalde kurumsal mimari yapıyorsan controller kısmında service ile çalışırız. şöyle
        public List<Category> GetCategoryList()
        {
            return _categoryDal.List();
        }

        public List<CategoryExtra> GetCategoryListExtra()
        {
            return _categoryDal.GetCategoryListExtra();
        }
    }


}

