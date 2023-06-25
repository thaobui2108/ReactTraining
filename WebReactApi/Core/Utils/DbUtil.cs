using WebReactApi.Core.Uow;
using WebReactApi.Core.Utils;

namespace WebReactApi.Core.Utils
{
    public static class DbUtil
    {
        public static T Repository<T>(IUnitOfWork unitOfWork)
        {
            var activator = ActivatorHelper.Instance.GetActivator<T>();
            return activator(unitOfWork);
        }
        public static T DataService<T>(IUnitOfWork unitOfWork)
        {
            var activator = ActivatorHelper.Instance.GetActivator<T>();
            return activator(unitOfWork);
        }
    }
}
