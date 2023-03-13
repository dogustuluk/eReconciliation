using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public IResult Add(Company company)
        {
            //validation islemi daha sonradan fluent api ile duzenlenecektir.
            if (company.Name.Length > 10)
            {
                _companyDal.Add(company);
                return new SuccessResult(Messages.AddedCompany);
            }
            return new ErrorResult("Sirket adi en az 10 karakter olmalidir!");
        }

        public IDataResult<List<Company>> GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetList(), "Listeleme islemi basarili");
        }
    }
}
