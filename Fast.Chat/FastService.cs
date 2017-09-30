using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocoding;
using Geocoding.Google;
using WebSocketSharp.Server;
using WebSocketSharp;
using Newtonsoft.Json;

namespace Fast.Chat
{
    public enum ConnectionAction
    {
        OPENED,
        CLOSED
    }

    class FastService : WebSocketBehavior
    {
        public GoogleAddress address { get; set; }
        public string Nickname;

        public delegate void ConnectionOpenOrCloseHandler(ConnectionAction action, FastService s);
        public event ConnectionOpenOrCloseHandler ConnectionOpenedOrClosed = delegate { };

        public delegate void MessageRecievedHandler(dynamic message, FastService s);
        public event MessageRecievedHandler MessageRecieved = delegate { };

        public FastService(FastRoom room, GoogleAddress a)
        {
            this.address = a;
            // some kind of dependency injection trick (I think..) - but it works 
           // room.AddService(this);

            Nickname = EmojiNameGenerator.GetNEmojis(1);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            dynamic message = JsonConvert.DeserializeObject<dynamic>(e.Data);

            MessageRecieved(message, this);
        }

        protected override void OnOpen()
        {
            ConnectionOpenedOrClosed(ConnectionAction.OPENED, this);
        }

        protected override void OnClose(CloseEventArgs e)
        {

            ConnectionOpenedOrClosed(ConnectionAction.CLOSED, this);
        }

        public void SendMessage(string data)
        {
            Send(data);
        }

        public void Disconnect()
        {
            Sessions.CloseSession(this.ID);
        }
    }
}
