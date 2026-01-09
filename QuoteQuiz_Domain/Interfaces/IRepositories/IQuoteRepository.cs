using QuoteQuiz_Domain.Entities;
using QuoteQuiz_Domain.Entities.Base;
using QuoteQuiz_Domain.Interfaces.IRepositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz_Domain.Interfaces.IRepositories
{
    public interface IQuoteRepository : IGenericRepository<QuoteEntity>
    {
    }
}
