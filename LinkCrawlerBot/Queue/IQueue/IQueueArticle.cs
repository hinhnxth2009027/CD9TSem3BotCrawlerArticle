namespace LinkCrawlerBot.Queue.IQueue
{
    public interface IQueueArticle
    {
        public void PushArticle(RabbitMQArticle rabbitMqArticle);
    }
}