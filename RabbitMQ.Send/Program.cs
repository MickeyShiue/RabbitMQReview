using RabbitMQ.Send;


//new NormalSend().Send();
//new WorkerSend().Send();
//new PublishSend().Send();
new RoutingKeySend().Send(args:@args);