using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IHelloCallback))]
    interface IHello
    {
        [OperationContract(IsOneWay = true)]
        void World();
    }

    public interface IHelloCallback
    {
        [OperationContract(IsOneWay = true)]
        void Callback(string msg);
    }

    public class Hello : IHello
    {
        public void World()
        {
            var msg = "123";
            //在这里获取IHelloCallback对象，将其赋值到全局变量或存入公有List、Dictionary里，即可在其它代码里随时调用客户端定义的Callback方法
            var callbackChannel = OperationContext.Current.GetCallbackChannel<IHelloCallback>();
            callbackChannel.Callback(msg);
        }
    }

    public class program
    {
        static void _Main(string[] args)
        {
            Uri baseAddr = new Uri("http://localhost:1000/MyService");
            ServiceHost host = new ServiceHost(typeof(Hello), baseAddr);
            host.AddServiceEndpoint(typeof(IHello), new WSDualHttpBinding(), "HelloService");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(smb);

            host.Open();
        }
    }
}

namespace Client
{
    [ServiceContract(CallbackContract = typeof(IHelloCallback), SessionMode = SessionMode.Required)]
    public interface IHello
    {
        [OperationContract(IsOneWay = true)]
        void World();
    }

    [ServiceContract]
    public interface IHelloCallback
    {
        [OperationContract(IsOneWay = true)]
        void Callback(string msg);
    }

    public class HelloClient : DuplexClientBase<IHello>, IHello
    {
        public HelloClient(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress) { }
        public void World()
        {
            base.Channel.World();
        }
    }

    public class HelloCallback : IHelloCallback
    {
        public void Callback(string msg)
        {
            //dosomething
            //eg:MessageBox.Show(msg);
        }
    }

    public class program
    {
        static void _Main(string[] args)
        {
            HelloCallback callback = new HelloCallback();
            InstanceContext clientContext = new InstanceContext(callback);
            WSDualHttpBinding binding = new WSDualHttpBinding();
            EndpointAddress remoteAddress = new EndpointAddress("http://localhost:1000/MyService/HelloService");
            HelloClient client = new HelloClient(clientContext, binding, remoteAddress);
            client.World();
        }
    }
}
