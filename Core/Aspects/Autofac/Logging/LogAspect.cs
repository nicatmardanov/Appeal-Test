using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Constants;
using Core.Utilities.Http;
using Core.Utilities.Interceptors;
using Core.Utilities.Results.Abstract;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private IResult? _result;
        private Microsoft.AspNetCore.Http.HttpContext? HttpContext => HttpContextHelper.HttpContext;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService)!;
        }


        protected override void OnAfter(IInvocation invocation)
        {
            Exception? exception = null;
            try
            {
                _result = ((dynamic?)invocation.ReturnValue)?.Result;
                if (_result?.Success == true)
                    return;
            }
            catch (Exception e)
            {
                exception = e;
            }

            _loggerServiceBase.Info(GetSerializedLogDetail(invocation, exception));
        }

        protected override void OnException(IInvocation invocation, Exception e)
        {
            _loggerServiceBase.Info(GetSerializedLogDetail(invocation, e));
        }

        private LogDetail GetLogDetail(IInvocation invocation, Exception? e = null)
        {
            List<LogParameter> logParameters = GetLogParameters(invocation);

            LogDetail logDetail = new()
            {
                MethodName = GetMethodInformation(invocation.Method),
                ConnectionInfo = JsonConvert.SerializeObject(new LogConnectionInfo(HttpContext?.Connection)),
                HeaderData = GetHeaderData(),
                LogParameters = logParameters
            };

            AddLogDetailText(e, ref logDetail);
            return logDetail;
        }

        private string GetMethodInformation(System.Reflection.MethodInfo method)
        {
            string? methodDeclaringFullName = method.DeclaringType?.FullName;
            return string.IsNullOrEmpty(methodDeclaringFullName) ? method.Name : $"{methodDeclaringFullName}.{method.Name}";
        }

        private string GetSerializedLogDetail(IInvocation invocation, Exception? e = null)
        {
            LogDetail? logDetail = GetLogDetail(invocation, e);
            return Environment.NewLine + Environment.NewLine + JsonConvert.SerializeObject(logDetail);
        }

        private Dictionary<string, string> GetHeaderData()
        {
            Dictionary<string, string> headerData = new();
            AddDataToDictionary(ref headerData, "Id");

            return headerData;
        }

        private List<LogParameter> GetLogParameters(IInvocation invocation)
        {
            List<LogParameter> logParameters = new();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            return logParameters;
        }

        private void AddLogDetailText(Exception? e, ref LogDetail logDetail)
        {
            if (e is null)
            {
                logDetail.Text = _result?.Message;
                logDetail.DetailedText = _result?.DetailedMessage;
                return;
            }

            logDetail.Text = e.Message;
            logDetail.DetailedText = e.StackTrace;
        }

        private void AddDataToDictionary(ref Dictionary<string, string> headerData, string key)
        {
            if (HttpContext?.Request.Headers.TryGetValue(key, out StringValues val) == true)
                headerData.Add(key, val);
        }
    }
}
