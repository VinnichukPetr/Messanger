using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Interfaces
{
    public interface ITranslator<TModel, TModelDTO>
    {
        TModel ModelDTOtoModel(TModelDTO modelDTO);
        TModelDTO ModeltoModelDTO(TModel model);
    }
}
