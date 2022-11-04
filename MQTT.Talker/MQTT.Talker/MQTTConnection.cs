using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT.Talker
{
    internal class MQTTConnection
    {
        private MqttClient _client = null;
       
        public void Connect(Broker broker, bool setEvents = true)
        {
            _client = CreateClient(broker, true);
            string clientID = Guid.NewGuid().ToString();
            _client.Connect(clientID, null, null, true, 10000);

        }

        public void Disconnect()
        {
            if( _client != null && _client.IsConnected)
            {
                _client.MqttMsgPublishReceived -= MqttMessageReceived;
                _client.Disconnect();
            }
        }

        private MqttClient CreateClient(Broker broker, bool setEvents = true)
        {
            if (_client != null)
            {
                return _client;
            }

            _client = new MqttClient(broker.Server,
                broker.Port,
                false,
                MqttSslProtocols.None,
                (sender, certificate, chain, errors) => true,
                (sender, host, certificates, certificate, issuers) => null);

            _client.MqttMsgPublishReceived += MqttMessageReceived;
            return _client;
        }

        private void _client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Send(string message, string destinationName)
        {
            _client.Publish(destinationName, Encoding.UTF8.GetBytes(message));
        }

        public void Subscribe(string sourceName)
        {
            _client.Subscribe(new[] { sourceName }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public void Unsubscribe(string sourceName)
        {
            _client.Unsubscribe(new[] { sourceName });
        }

        public event EventHandler<MessageInfo> MessageReceived;

        private void MqttMessageReceived(object sender, MqttMsgPublishEventArgs e)
        {
            MessageInfo info = new MessageInfo();
            info.Topic = e.Topic;
            info.Message = Encoding.UTF8.GetString(e.Message);
            if (MessageReceived != null)
            {
                MessageReceived(this, info);
            }

        }

    }
}
