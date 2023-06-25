
using WebReactApi.Common;
using WebReactApi.Core.Entities;
using WebReactApi.Core.Uow;

namespace WebReactApi.Service.Todos
{
    public class TodoService : CrudService<Todo>, ITodoService
    {
        public TodoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
