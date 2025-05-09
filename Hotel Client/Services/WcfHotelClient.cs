using System.ServiceModel;
using System.ServiceModel.Channels;
using Hotel_Client.OnlineReceptionImplService;

namespace Hotel_Client.Services
{
    public static class WcfHotelClient
    {
        private static readonly EndpointAddress Address = new("http://localhost:8080/Server/OnlineReceptionImplService");

        private static readonly BasicHttpBinding Binding = new()
        {
            MaxBufferSize = int.MaxValue,
            MaxReceivedMessageSize = int.MaxValue,
            ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
            MessageEncoding = WSMessageEncoding.Text,
            AllowCookies = true
        };

        private static readonly ChannelFactory<OnlineReceptionImpl> Factory = new(Binding, Address);

        public static OnlineReceptionImpl Create()
        {
            return Factory.CreateChannel();
        }
    }
}
