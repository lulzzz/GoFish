using System;
using System.Reflection;

namespace GoFish.Advert
{
    public interface ICommandMediator
    {
        void Send<TResult>(ICommand<TResult> query);
    }

    public class CommandMediator : ICommandMediator
    {
        private IServiceProvider _serviceProvider;

        public CommandMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Send<TResult>(ICommand<TResult> command)
        {
            var handler = GetHandler(command);

            try
            {
                handler
                    .GetType()
                    .GetMethod("Handle")
                    .Invoke(handler, new object[] { command });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is AdvertNotFoundException)
                {
                    throw ex.InnerException;
                }

                throw new InvalidOperationException(ex.InnerException.Message, ex);
            }
            catch
            {
                throw;
            }
        }

        private object GetHandler<TResult>(ICommand<TResult> query)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            return _serviceProvider.GetService(handlerType);
        }
    }
}