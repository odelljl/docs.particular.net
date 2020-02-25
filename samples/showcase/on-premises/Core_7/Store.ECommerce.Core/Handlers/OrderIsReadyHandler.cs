﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using Store.Messages.Events;

public class OrderIsReadyHandler :
    IHandleMessages<DownloadIsReady>
{
    private IHubContext<OrdersHub> ordersHubContext;

    public OrderIsReadyHandler(IHubContext<OrdersHub> ordersHubContext)
    {
        this.ordersHubContext = ordersHubContext;
    }

    public async Task Handle(DownloadIsReady message, IMessageHandlerContext context)
    {
        await ordersHubContext.Clients.Client(message.ClientId).SendAsync("orderReady",
            new
            {
                message.OrderNumber,
                ProductUrls = message.ProductUrls.Select(pair => new {Id = pair.Key, Url = pair.Value}).ToArray()
            });
    }
}