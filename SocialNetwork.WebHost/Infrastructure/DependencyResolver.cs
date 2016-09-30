using Ninject.Modules;
using SocialNetwork.Logic.Interfaces;
using SocialNetwork.Logic.Services;

namespace SocialNetwork.WebHost.Infrastructure
{
    public class WebHostModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IMessageService>().To<MessageService>();
            Kernel.Bind<ICommentService>().To<CommentService>();
        }
    }
}