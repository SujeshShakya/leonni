using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Leonni.Models.ViewModels;
using Leonni.Models.ModelMappers;
using Leonni.Interfaces;
using Leonni.Core.Controllers;

namespace Leonni.Core.Filters
{
    public class DropdownFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = (filterContext.Controller as LeonniApplicationController);

            if (controller != null)
            {
                var viewModel = (filterContext.Controller.ViewData.Model as BaseViewModel);
                if (viewModel != null)
                {
                    viewModel.Categories = controller.Categories;
                    viewModel.Provinces = controller.Provinces;
                    viewModel.Years = controller.Years;
                    viewModel.Months = controller.Months;
                    viewModel.Sorts = controller.Sorts;
                    viewModel.Disciplines = controller.Disciplines;
                    viewModel.Countries = controller.Countries;
                    viewModel.Sexs = controller.Sexs;

                }
            }
            else
            {
                throw new Exception("Always derive controller from LeonniApplicationController");
            }

            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _repCountry = DependencyResolver.Current.GetService<ICountryRepository>();
            var _repCategory = DependencyResolver.Current.GetService<ICategoryRepository>();
            var _repYear = DependencyResolver.Current.GetService<IYearRepository>();
            var _repMonth = DependencyResolver.Current.GetService<IMonthRepository>();

            //Countries and Provinces
            List<CountryModel> lstCountries = new List<CountryModel>();

            lstCountries.Add(new CountryModel
            {
                Id = 0,
                Name = "Country"
            });


             lstCountries.AddRange(CountryMap.Map(_repCountry.GetList().ToList()));

            List<ProvinceModel> lstProvinces = new List<ProvinceModel>();

            lstProvinces.Add(new ProvinceModel
            {
                Id = 0,
                Name = "State"
            });



            ///Categories
            ///
            List<CategoryModel> lstCategories = new List<CategoryModel>();

            lstCategories.Add(new CategoryModel
            {
                Id = 0,
                CategoryName = "Categories",
                LanguageId = 1
            });

            List<YearModel> lstYears = new List<YearModel>();

            lstCategories.AddRange(CategoryMap.Map(_repCategory.GetList(x => x.LanguageId == 1).ToList()));

            ///Years

            lstYears.Add(new YearModel
            {
                Id = 0,
                Year = "Years",
                LanguageId = 1
            });

            lstYears.AddRange(YearMap.Map(_repYear.GetList(x => x.LanguageId == 1).ToList()));

            List<MonthModel> lstMonths = new List<MonthModel>();

            lstMonths.Add(new MonthModel
            {
                Id = 0,
                Month = "Months",
                LanguageId = 1
            });

            lstMonths.AddRange(MonthMap.Map(_repMonth.GetList(x => x.LanguageId == 1).ToList()));


            List<DisciplineModel> lstDisciplines = new List<DisciplineModel>();

            lstDisciplines.Add(new DisciplineModel
            {
                Id = 0,
                DisciplineName = "Discipline",
                LanguageId = 1
            });


            List<SortByModel> lstSort = new List<SortByModel>();
            lstSort.Add(new SortByModel
            {
                Id = 0,
                OrderBy = "Sort By"
            }

                );

            lstSort.Add(new SortByModel
            {
                Id = 1,
                OrderBy = "Ascending"
            });

            lstSort.Add(new SortByModel
            {
                Id = 2,
                OrderBy = "Descending"
            });

            List<SexModel> lstSex = new List<SexModel>();

            lstSex.Add(new SexModel
            {
                Id = 0,
                Sex = "Sex"
            });

            lstSex.Add(new SexModel
            {
                Id = 1,
                Sex = "Male"
            });

            lstSex.Add(new SexModel
            {
                Id = 2,
                Sex = "Female"
            });

            var controller = (filterContext.Controller as LeonniApplicationController);
            if (controller != null)
            {
                controller.Categories = new SelectList(lstCategories, "Id", "CategoryName");
                controller.Countries = new SelectList(lstCountries, "Name", "Name");
                controller.Years = new SelectList(lstYears, "Id", "Year");
                controller.Months = new SelectList(lstMonths, "Id", "Month");
                controller.Provinces = new SelectList(lstProvinces, "Id", "Name");
                controller.Sorts = new SelectList(lstSort, "Id", "OrderBy");
                controller.Disciplines = new SelectList(lstDisciplines, "Id", "DisciplineName");
                controller.Sexs = new SelectList(lstSex, "Sex", "Sex");

            }
            else
            {
                throw new Exception("Always derieve controller from LeonniApplicationController");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
