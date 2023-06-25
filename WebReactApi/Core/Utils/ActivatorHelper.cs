using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebReactApi.Core.Utils
{
    internal delegate T ObjectActivator<T>(params object[] args);


    /// <summary>
    /// Making reflection fly and exploring delegates
    /// https://vagifabilov.wordpress.com/2010/04/02/dont-use-activator-createinstance-or-constructorinfo-invoke-use-compiled-lambda-expressions/
    /// http://rogeralsing.com/2008/02/28/linq-expressions-creating-objects/
    /// </summary>
    internal sealed class ActivatorHelper
    {
        private static readonly Lazy<ActivatorHelper> LazyInstance
            = new Lazy<ActivatorHelper>(() => new ActivatorHelper(), true);
        private static object _lockObject = new object();
        private ActivatorHelper()
        {
        }

        public static ActivatorHelper Instance
        {
            get { return LazyInstance.Value; }
        }

        private readonly Dictionary<Type, dynamic> _dict = new Dictionary<Type, dynamic>();

        public ObjectActivator<T> GetActivator<T>()
        {
            var key = typeof(T);
            try
            {
                lock (_lockObject)
                {
                    if (!_dict.ContainsKey(key))
                    {
                        _dict.Add(key, GetCompileConstructor<T>());
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return (ObjectActivator<T>)_dict[key];
        }

        private static Delegate GetCompileConstructor<T>()
        {
            return GetCompileConstructor<T>(typeof(T).GetConstructors().First());
        }

        private static Delegate GetCompileConstructor<T>(ConstructorInfo ctor)
        {
            var paramsInfo = ctor.GetParameters();
            var param = Expression.Parameter(typeof(object[]), "args");
            var argsExp = new Expression[paramsInfo.Length];

            for (var i = 0; i < paramsInfo.Length; i++)
            {
                var index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;

                var paramAccessorExp = Expression.ArrayIndex(param, index);
                var paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                argsExp[i] = paramCastExp;
            }

            var newExp = Expression.New(ctor, argsExp);
            return Expression.Lambda(typeof(ObjectActivator<T>), newExp, param).Compile();
        }
    }
}
