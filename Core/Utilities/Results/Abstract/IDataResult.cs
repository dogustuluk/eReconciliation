using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstract
{
    //out ile disaridan T parametresine veri gonderebiliyor olabiliriz.
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
