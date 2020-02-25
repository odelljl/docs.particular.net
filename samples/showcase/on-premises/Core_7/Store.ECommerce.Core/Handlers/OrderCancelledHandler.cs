﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using Store.Messages.Events;

public class OrderCancelledHandler :
    IHandleMessages<OrderCancelled>
{
    private IHubContext<OrdersHub> ordersHubContext;

    public OrderCancelledHandler(IHubContext<OrdersHub> ordersHubContext)
    {
        this.ordersHubContext = ordersHubContext;
    }

    public async Task Handle(OrderCancelled message, IMessageHandlerContext context)
    {
        await ordersHubContext.Clients.Client(message.ClientId).SendAsync("orderCancelled", 
            new
            {
                message.OrderNumber,
            });
    }
}
