using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.WebApp.MVC.Extensions;

namespace NerdStore.WebApp.MVC.Services
{
    public abstract class TextSerializerService : NotificationsBaseSevice
    {
        protected TextSerializerService(INotificationHandler<DomainNotification> notifications, 
                                        IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
        }

        protected StringContent GetContent(object obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool HandlerResponseErrors(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                    throw new CustomHttpRequestException(response.StatusCode);
                case 403:
                    throw new CustomHttpRequestException(response.StatusCode);
                case 404:
                    break;                   
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            //response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult ReturnOk()
        {
            return new ResponseResult();
        }
    }
}