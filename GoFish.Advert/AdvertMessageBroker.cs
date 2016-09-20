using System.Text;
using AutoMapper;
using GoFish.Shared.Messaging;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GoFish.Advert
{
    public class AdvertMessageBroker : IMessageBroker<Advert>
    {
        private readonly IMapper _mapper;
        private ILogger<AdvertMessageBroker> _logger;

        public AdvertMessageBroker(ILogger<AdvertMessageBroker> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public void Send(Advert objectToSend)
        {
            var client = new MessagingClient(_logger, "172.17.0.1");
            var dto = _mapper.Map<Advert, AdvertDto>(objectToSend);

            client.SendMessage("AdvertAdded", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto)));
        }
    }
}