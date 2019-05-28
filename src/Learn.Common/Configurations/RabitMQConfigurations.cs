using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn.Common.Configurations
{
    public class RabitMQConfigurations
    {
        public TimeSpan PublishConfirmTimeout { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string VirtualHost { get; set; }
        public SslOption Ssl { get; set; }
        public TimeSpan RequestTimeout { get; set; }
        public bool AutoCloseConnection { get; set; }
        public bool TopologyRecovery { get; set; }
        public bool AutomaticRecovery { get; set; }
        public bool RouteWithGlobalId { get; set; }
        public TimeSpan GracefulShutdown { get; set; }
        public TimeSpan RecoveryInterval { get; set; }
        public bool PersistentDeliveryMode { get; set; }
    }
}
